using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSHL.Formats.Pokémon.Scripting
{
    public class Decompiler
    {
        private CommandDataBase commandDB;
        public List<Script> Scripts;
        public List<Script> Functions;
        private Queue<uint> ScriptOffsets;
        private Queue<uint> FunctionOffsets;

        public Decompiler(CommandDataBase commandDatabase)
        {
            commandDB = commandDatabase;

            Scripts = new List<Script>();
            Functions = new List<Script>();

            ScriptOffsets = new Queue<uint>();
            FunctionOffsets = new Queue<uint>();
        }

        public void Decompile(BinaryReader br)
        {
            // Setup
            Scripts.Clear();
            Functions.Clear();

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
            if (ScriptOffsets.Count == 0) return;
            while (ScriptOffsets.Count > 0)
            {
                // Get the offset
                uint scriptOffset = ScriptOffsets.Dequeue();
                if (scriptOffset >= br.BaseStream.Length - 2) continue;

                // Goto
                br.BaseStream.Seek(scriptOffset, SeekOrigin.Begin);
                Script script = new Script();
                script.Offset = scriptOffset;

                // Read commands
                Command cmd = new Command();
                do
                {
                    cmd = DecompileCommand(br);

                    if (cmd.IsJump)
                    {
                        // Get offset of function
                        uint fOffset = cmd.Arguments.Last() + (uint)br.BaseStream.Position;
                        cmd.JumpOffset = fOffset;

                        // Check if it's a new jump
                        if (!FunctionOffsets.Contains(fOffset) && !ScriptOffsets.Contains(fOffset))
                        {
                            FunctionOffsets.Enqueue(fOffset);
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
                Script func = new Script();
                func.Offset = functionOffset;

                // Read commands
                Command cmd = new Command();
                do
                {
                    cmd = DecompileCommand(br);

                    if (cmd.IsJump)
                    {
                        // Get offset of function
                        uint fOffset = cmd.Arguments.Last() + (uint)br.BaseStream.Position;
                        cmd.JumpOffset = fOffset;

                        // Check if it's a new jump
                        if (!FunctionOffsets.Contains(fOffset) && !doneFunctions.Contains(fOffset))
                        {
                            FunctionOffsets.Enqueue(fOffset);
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
                            cmd.Arguments.Add(br.ReadUInt32());
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

        public class Script
        {
            public uint Offset;
            public List<Command> Commands;

            public Script()
            {
                Offset = 0;
                Commands = new List<Command>();
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
