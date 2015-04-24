using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DSHL.Formats.Pokémon.Scripting
{
    public class Decompiler
    {
        private CommandDatabase commandDB;
        public List<Script> Scripts;
        public List<Script> Functions;
        public List<Movement> Movements;
        private Queue<uint> ScriptOffsets;
        private Queue<uint> FunctionOffsets;
        private Queue<uint> MovementOffsets;

        public Decompiler(CommandDatabase cdb)
        {
            commandDB = cdb;

            Scripts = new List<Script>();
            Functions = new List<Script>();
            Movements = new List<Movement>();

            ScriptOffsets = new Queue<uint>();
            FunctionOffsets = new Queue<uint>();
            MovementOffsets = new Queue<uint>();
        }

        public void Reset()
        {
            Scripts.Clear();
            Functions.Clear();
            Movements.Clear();

            ScriptOffsets.Clear();
            FunctionOffsets.Clear();
            MovementOffsets.Clear();
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
                    else if (cmd.IsMoveJump)
                    {
                        if (!MovementOffsets.Contains(cmd.JumpOffset))
                        {
                            MovementOffsets.Enqueue(cmd.JumpOffset);
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
            //FunctionOffsets.OrderByDescending(x => x);
            while (FunctionOffsets.Count > 0)
            {
                // Sort it out, lowest to highest
                //FunctionOffsets.OrderByDescending(x => x); // 
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
                    else if (cmd.IsMoveJump)
                    {
                        if (!MovementOffsets.Contains(cmd.JumpOffset))
                        {
                            MovementOffsets.Enqueue(cmd.JumpOffset);
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

            // Read the movements
            #region Movements
            scrL = 1;
            //MovementOffsets.OrderByDescending(x => x);
            while (MovementOffsets.Count > 0)
            {
                uint offset = MovementOffsets.Dequeue();
                if (offset >= br.BaseStream.Length - 4) continue; // TODO: Error
                //MessageBox.Show("Movement offset: " + offset.ToString("X"));

                // Check start of movement
                /*if (br.ReadUInt32() != 0x000000FE)
                {
                    throw new Exception("Invalid movement!");
                }*/

                // Read movements
                // This format is a bit more obscure
                // I had to guess a bit here
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
                Movement mov = new Movement("mov" + scrL); scrL++;
                mov.Offset = offset;

                while (br.BaseStream.Position < br.BaseStream.Length - 4)
                {
                    Move m = new Move();
                    m.Type = br.ReadUInt16();
                    m.Steps = br.ReadUInt16();

                    if (m.Type == 0xFE) // End of movement?
                    {
                        break;
                    }
                    else
                    {
                        mov.Commands.Add(m);
                    }
                }

                Movements.Add(mov);
            }

            #endregion
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
                            {
                                uint offset = (uint)br.BaseStream.Position + br.ReadUInt32() + 4;
                                //cmd.Arguments.Add(br.ReadUInt32());
                                cmd.Arguments.Add(offset);
                                cmd.JumpOffset = offset;
                            }
                            break;

                        // Failure
                        default:
                            throw new Exception("Bad argument for command " + cmd.Value.ToString("X4") + "!");
                    }
                }
            }

            return cmd;
        }

        /*private Move DecompileMove(BinaryReader br)
        {
            // This is actually quite simple.
            // It doesn't even need a function.
            Move m = new Move();
            m.Type = br.ReadUInt16();
            m.Steps = br.ReadUInt16();
            return m;
        }*/

        public string ScriptsToString()
        {
            string r = "# There are " + Scripts.Count + " scripts.\n\n";
            foreach (var script in Scripts)
            {
                r += "# 0x" + script.Offset.ToString("X") + "\n";
                r += "@ " + script.Name + " {\n";
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
                            //r += "move" + cmd.Arguments[i].ToString("X");
                            string n = GetMovementNameAtOffset(cmd.Arguments[i]);
                            if (n != string.Empty)
                            {
                                r += n;
                            }
                            else
                            {
                                r += "0x" + cmd.Arguments[i].ToString("X") + " # This should point to a movement!";
                                throw new Exception("Could not find a movement to call!");
                            }
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
                r += "# 0x" + func.Offset.ToString("X") + "\n";
                r += "@ " + func.Name + " {\n";
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
                            //r += "move" + cmd.Arguments[i].ToString("X");
                            string n = GetMovementNameAtOffset(cmd.Arguments[i]);
                            if (n != string.Empty)
                            {
                                r += n;
                            }
                            else
                            {
                                r += "0x" + cmd.Arguments[i].ToString("X") + " # This should point to a movement!";
                                throw new Exception("Could not find a movement to call!");
                            }
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
            string r = "# There are " + Movements.Count + " movements.\n\n";
            foreach (var moves in Movements)
            {
                r += "# 0x" + moves.Offset.ToString("X") + "\n";
                r += "$ " + moves.Name + " [\n";
                foreach (var move in moves.Commands)
                {
                    r += "\t";
                    // MOVE NAME
                    r += "0x" + move.Type.ToString("X4");
                    r += " ";
                    r += "0x" + move.Steps.ToString("X4");
                    r += ";\n";
                }
                r += "]\n\n";
            }
            return r;
            //return "~Not yet~";
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

        public string GetMovementNameAtOffset(uint offset)
        {
            for (int i = 0; i < Movements.Count; i++)
            {
                if (Movements[i].Offset == offset) return Movements[i].Name;
            }
            return string.Empty;
        }

        #region Parts

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

        public class Movement
        {
            public string Name;
            public uint Offset;
            public List<Move> Commands;

            public Movement(string name)
            {
                Name = name;
                Offset = 0;
                Commands = new List<Move>();
            }
        }

        public class Move
        {
            public ushort Type;
            public ushort Steps;
        }

        #endregion
    }
}
