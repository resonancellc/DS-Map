using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSHL.Formats.Pokémon.Scripting
{
    public class Decompiler
    {
        private CommandDatabase commandDB;
        public List<Script> Scripts;
        public List<Script> Functions;
        private Queue<uint> ScriptOffsets;
        private Queue<uint> FunctionOffsets;

        public Decompiler(CommandDatabase cdb)
        {
            commandDB = cdb;

            Scripts = new List<Script>();
            Functions = new List<Script>();

            ScriptOffsets = new Queue<uint>();
            FunctionOffsets = new Queue<uint>();
        }

        public void Reset()
        {
            Scripts.Clear();
            Functions.Clear();

            ScriptOffsets.Clear();
            FunctionOffsets.Clear();
        }

        public void Decompile(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                BinaryReader br = new BinaryReader(ms);
                Decompile(br);
                br.Dispose();
            }
        }

        public void Decompile(BinaryReader br)
        {
            // Reset
            Reset();

            // Read header
            ScriptOffsets.Clear();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                // Read the script offset, baby~!
                uint offset = br.ReadUInt32();
                if ((offset & 0xFFFF) == 0xFD13) break;
                else ScriptOffsets.Enqueue(offset + (uint)br.BaseStream.Position); // Offsets are relative
            }

            // Read the scripts
            #region Scripts
            int scrL = 1;
            if (ScriptOffsets.Count == 0) return;
            while (ScriptOffsets.Count > 0)
            {
                // Get the offset
                uint scriptOffset = ScriptOffsets.Dequeue();
                if (scriptOffset >= br.BaseStream.Length - 2) continue;

                // Goto
                br.BaseStream.Seek(scriptOffset, SeekOrigin.Begin);
                Script script = new Script("script" + scrL); scrL++;
                script.Offset = scriptOffset;

                // Read commands
                Command cmd = new Command();
                do
                {
                    cmd = DecompileCommand(br);

                    if (cmd.IsJump)
                    {
                        // Get offset of function
                        //uint fOffset = cmd.Arguments.Last() + (uint)br.BaseStream.Position;
                        //cmd.JumpOffset = fOffset;

                        // Check if it's a new jump
                        if (!FunctionOffsets.Contains(cmd.JumpOffset) && !ScriptOffsets.Contains(cmd.JumpOffset))
                        {
                            FunctionOffsets.Enqueue(cmd.JumpOffset);
                        }
                    }

                    script.Commands.Add(cmd);
                }
                while (!cmd.IsEnd);

                // Save
                Scripts.Add(script);
            }
            #endregion

            // Read the functions
            #region Functions
            scrL = 1;
            List<uint> doneFunctions = new List<uint>();
            while (FunctionOffsets.Count > 0)
            {
                // Sort it out, lowest to highest
                FunctionOffsets.OrderByDescending(x => x); // 
                uint functionOffset = FunctionOffsets.Dequeue(); // Get first (smallest) offset
                if (functionOffset >= br.BaseStream.Length - 2) continue;
                else if (doneFunctions.Contains(functionOffset)) continue;

                // Goto
                br.BaseStream.Seek(functionOffset, SeekOrigin.Begin);
                Script func = new Script("func" + scrL); scrL++;
                func.Offset = functionOffset;

                // Read commands
                Command cmd = new Command();
                do
                {
                    cmd = DecompileCommand(br);

                    if (cmd.IsJump)
                    {
                        // Get offset of function
                        //uint fOffset = cmd.Arguments.Last() + (uint)br.BaseStream.Position;
                        //cmd.JumpOffset = fOffset;

                        // Check if it's a new jump
                        if (!FunctionOffsets.Contains(cmd.JumpOffset) && !doneFunctions.Contains(cmd.JumpOffset))
                        {
                            FunctionOffsets.Enqueue(cmd.JumpOffset);
                        }
                    }

                    func.Commands.Add(cmd);
                }
                while (!cmd.IsEnd);

                // Done
                //Functions.Add(func);
                if (Functions.Count == 0) Functions.Add(func);
                else
                {
                    bool added = false;
                    for (int i = 0; i < Functions.Count; i++)
                    {
                        if (Functions[i].Offset > func.Offset)
                        {
                            Functions.Insert(i, func);
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        Functions.Add(func);
                    }
                }

                doneFunctions.Add(functionOffset);
            }
            #endregion

            // TODO: Movements
        }

        private Command DecompileCommand(BinaryReader br)
        {
            // Setup command
            Command cmd = new Command();
            cmd.Offset = (uint)br.BaseStream.Position;
            cmd.Value = br.ReadUInt16();
            cmd.Name = commandDB.GetCommandName(cmd.Value);

            // Read arguments -- interpret command database!
            string[] args = commandDB.GetCommandArguments(cmd.Value);
            if (args != null && args.Length > 0)
            {
                foreach (string arg in args) //
                {
                    switch (arg) // Parse the commands
                    {
                        // Generic Cases
                        case "u8":
                            cmd.Arguments.Add(br.ReadByte());
                            break;
                        case "u16":
                            cmd.Arguments.Add(br.ReadUInt16());
                            break;
                        case "u32":
                            cmd.Arguments.Add(br.ReadUInt32());
                            break;

                        // Special Cases
                        case "-e": // End Function/Script -- nothing to read
                            cmd.IsEnd = true;
                            break;
                        case "-f": // Jump to Func (32)
                            cmd.IsJump = true;
                            {
                                uint offset = (uint)br.BaseStream.Position + br.ReadUInt32();
                                cmd.Arguments.Add(offset);
                                cmd.JumpOffset = offset;
                            }
                            break;
                        case "-m": // Jump to Move (32)
                            cmd.IsMoveJump = true;
                            cmd.Arguments.Add(br.ReadUInt32());
                            break;

                        // Failure
                        default:
                            throw new Exception("Bad argument for command " + cmd.Value.ToString("X4") + "!");
                    }
                }
            }

            return cmd;
        }

        public string ScriptsToString()
        {
            string r = "# There are " + Scripts.Count + " scripts.\n\n";
            foreach (var script in Scripts)
            {
                r += "@ " + script.Name + " { # 0x" + script.Offset.ToString("X") + "\n";
                //r += ScriptToString(script);
                foreach (var cmd in script.Commands)
                {
                    r += "\t" + cmd.Name;
                    string[] args = commandDB.GetCommandArguments(cmd.Value);
                    for (int i = 0; i < cmd.Arguments.Count; i++)
                    {
                        if (args[i] == "-e") break; // Just in case, break

                        r += " ";
                        if (cmd.IsJump && args[i] == "-f") // For when we want a script name
                        {
                            string n = GetScriptNameAtOffset(cmd.Arguments[i]);
                            if (n != string.Empty)
                            {
                                r += n;
                                continue;
                            }

                            n = GetFunctionNameAtOffset(cmd.Arguments[i]);
                            if (n != string.Empty)
                            {
                                r += n;
                                continue;
                            }

                            r += "0x" + cmd.Arguments[i].ToString("X") + " # This should point to a script!";
                            throw new Exception("Could not find a function/script to jump to!");
                            //r += GetScriptNameAtOffset(cmd.Arguments[i]);
                            //r += "UnknownScript/Function!!! // 0x" + cmd.Arguments[i].ToString("X");
                        }
                        else if (cmd.IsMoveJump && args[i] == "-m") // For when we want a movement name
                        {
                            r += "move" + cmd.Arguments[i].ToString("X");
                        }
                        else
                        {
                            r += "0x" + cmd.Arguments[i].ToString("X");
                        }
                    }
                    r += ";\n";
                }
                r += "}\n\n";
            }
            return r;
        }

        public string FunctionsToString()
        {
            string r = "# There are " + Functions.Count + " functions.\n\n";
            foreach (var func in Functions)
            {
                r += "@ " + func.Name + " { # 0x" + func.Offset.ToString("X") + "\n";
                //r += ScriptToString(script);
                foreach (var cmd in func.Commands)
                {
                    r += "\t" + cmd.Name;
                    string[] args = commandDB.GetCommandArguments(cmd.Value);
                    for (int i = 0; i < cmd.Arguments.Count; i++)
                    {
                        if (args[i] == "-e") break; // Just in case, break

                        r += " ";
                        if (cmd.IsJump && args[i] == "-f") // For when we want a script name
                        {
                            string n = GetScriptNameAtOffset(cmd.Arguments[i]);
                            if (n != string.Empty)
                            {
                                r += n;
                                continue;
                            }

                            n = GetFunctionNameAtOffset(cmd.Arguments[i]);
                            if (n != string.Empty)
                            {
                                r += n;
                                continue;
                            }

                            r += "0x" +  cmd.Arguments[i].ToString("X") + " # This should point to a script!";
                            throw new Exception("Could not find a function/script to jump to!");
                            //r += GetScriptNameAtOffset(cmd.Arguments[i]);
                            //r += "UnknownScript/Function!!! // 0x" + cmd.Arguments[i].ToString("X");
                        }
                        else if (cmd.IsMoveJump && args[i] == "-m") // For when we want a movement name
                        {
                            r += "move" + cmd.Arguments[i].ToString("X");
                        }
                        else
                        {
                            r += "0x" + cmd.Arguments[i].ToString("X");
                        }
                    }
                    r += ";\n";
                }
                r += "}\n\n";
            }
            return r;
        }

        public string MovementsToString()
        {
            return "~Not yet~";
        }

        public string GetScriptNameAtOffset(uint offset)
        {
            for (int i = 0; i < Scripts.Count; i++)
            {
                if (Scripts[i].Offset == offset) return Scripts[i].Name;
            }
            return string.Empty;
        }

        public string GetFunctionNameAtOffset(uint offset)
        {
            for (int i = 0; i < Functions.Count; i++)
            {
                if (Functions[i].Offset == offset) return Functions[i].Name;
            }
            return string.Empty;
        }

        public class Script
        {
            public uint Offset;
            public List<Command> Commands;
            public string Name;

            public Script(string name)
            {
                Offset = 0;
                Commands = new List<Command>();
                Name = name;
            }
        }

        public class Command
        {
            public Command()
            {
                Value = 0;
                Name = "";
                Arguments = new List<uint>();

                IsEnd = false;
                IsJump = false;
                IsMoveJump = false;
            }

            public uint Offset;

            public ushort Value;
            public string Name;
            public List<uint> Arguments;

            public bool IsEnd, IsJump, IsMoveJump;
            public uint JumpOffset;
        }
    }
}
