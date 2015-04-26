using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DSHL.Formats.Pokémon.Scripting.Ast;

namespace DSHL.Formats.Pokémon.Scripting
{
    public class Compiler
    {
        private CommandDatabase commands;
        private string tempFile;

        public Compiler(CommandDatabase cdb)
        {
            commands = cdb;
            tempFile = string.Empty;
        }

        public byte[] Compile(TokenReader scripts, TokenReader functions, TokenReader movements)
        {
            // Setup
            Parser sParser = new Parser(scripts, commands);
            Parser fParser = new Parser(functions, commands);
            Parser mParser = new Parser(movements, commands); // todo: change commands

            // Parse all
            // TODO: optimize to save space
            Block[] sResult = sParser.Parse();
            Block[] fResult = fParser.Parse();
            Block[] mResult = mParser.Parse();

            MessageBox.Show(sResult.Length + " scripts\n" + fResult.Length + " functions\n" + mResult.Length + " movements");

            // Write
            Dictionary<string, uint> labels = new Dictionary<string, uint>();
            Dictionary<uint, string> labelsToReplace = new Dictionary<uint, string>();

            string tempFile = Temporary.GetTemporaryFileName();
            using (BinaryWriter bw = new BinaryWriter(File.Create(tempFile)))
            {
                // Write blank header
                // This will get filled in last ;)
                for (int i = 0; i < sResult.Length; i++)
                {
                    bw.Write((uint)0);
                }
                bw.Write((ushort)0xFD13); // An integer could work here, but this saves a bit of space

                // Write parts
                
                #region Scripts
                for (int i = 0; i < sResult.Length; i++)
                {
                    // Get script, set label
                    SBlock script = (SBlock)sResult[i];
                    labels.Add(script.Name, (uint)bw.BaseStream.Position);

                    // Write commands
                    foreach (var cmd in script.Commands)
                    {
                        // cmd
                        bw.Write(cmd.Value);

                        // arguments
                        foreach (var arg in cmd.Arguments)
                        {
                            if (arg is SPointer)
                            {
                                // save for later
                                var ptr = (SPointer)arg;
                                labelsToReplace.Add((uint)bw.BaseStream.Position, ptr.Destination);

                                // write blank pointer
                                bw.Write((uint)0);
                            }
                            else if (arg is SNumber<byte>)
                            {
                                var num = (SNumber<byte>)arg;
                                bw.Write(num.Value);
                            }
                            else if (arg is SNumber<ushort>)
                            {
                                var num = (SNumber<ushort>)arg;
                                bw.Write(num.Value);
                            }
                            else if (arg is SNumber<uint>)
                            {
                                var num = (SNumber<uint>)arg;
                                bw.Write(num.Value);
                            }
                            else throw new Exception("Invalid command!\nHow did it make it past parsing?");
                        }
                    }






                }
                #endregion

                #region Functions
                for (int i = 0; i < fResult.Length; i++)
                {
                    // Get script, set label
                    SBlock func = (SBlock)fResult[i];
                    labels.Add(func.Name, (uint)bw.BaseStream.Position);

                    // Write commands
                    foreach (var cmd in func.Commands)
                    {
                        // cmd
                        bw.Write(cmd.Value);

                        // arguments
                        foreach (var arg in cmd.Arguments)
                        {
                            if (arg is SPointer)
                            {
                                // save for later
                                var ptr = (SPointer)arg;
                                labelsToReplace.Add((uint)bw.BaseStream.Position, ptr.Destination);

                                // write blank pointer
                                bw.Write((uint)0);
                            }
                            else if (arg is SNumber<byte>)
                            {
                                var num = (SNumber<byte>)arg;
                                bw.Write(num.Value);
                            }
                            else if (arg is SNumber<ushort>)
                            {
                                var num = (SNumber<ushort>)arg;
                                bw.Write(num.Value);
                            }
                            else if (arg is SNumber<uint>)
                            {
                                var num = (SNumber<uint>)arg;
                                bw.Write(num.Value);
                            }
                            else throw new Exception("Invalid command!\nHow did it make it past parsing?");
                        }
                    }


                }
                #endregion

                #region Movements
                for (int i = 0; i < mResult.Length; i++)
                {
                    // Get movement, set label
                    MBlock block = (MBlock)mResult[i];
                    labels.Add(block.Name, (uint)bw.BaseStream.Position);

                    // Write commands
                    foreach (var cmd in block.Commands)
                    {
                        // direction
                        bw.Write(cmd.Type);

                        // steps
                        bw.Write(cmd.Steps);
                    }

                }
                #endregion

                // Write actual header
                bw.BaseStream.Seek(0L, SeekOrigin.Begin);
                for (int i = 0; i < sResult.Length; i++)
                {
                    string script = ((SBlock)sResult[i]).Name;

                    uint pos = (uint)bw.BaseStream.Position;
                    bw.Write(labels[script] - pos + 4u);
                }

                // Write pointers
                foreach (uint offset in labelsToReplace.Keys)
                {
                    
                    string label = labelsToReplace[offset];
                    if (labels.ContainsKey(label))
                    {
                        bw.BaseStream.Seek(offset, SeekOrigin.Begin);
                        //uint pos = (uint)bw.BaseStream.Position;
                        bw.Write(labels[label] - offset + 4u);
                    }
                    else
                    {
                        Error(label + " has not been defined!");
                    }
                }
            }
            

            // smoosh
            /*List<Block> allBlocks = new List<Block>();
            allBlocks.AddRange(sResult);
            allBlocks.AddRange(fResult);
            allBlocks.AddRange(mResult);
            
            return allBlocks.ToArray();*/
            return File.ReadAllBytes(tempFile);
        }

        private void Error(string msg)
        {
            throw new Exception(msg);
        }
    }

    public class Parser
    {
        private CommandDatabase commands;
        private TokenReader tokens;

        public Parser(TokenReader tokens, CommandDatabase cdb)
        {
            this.commands = cdb;
            this.tokens = tokens;
        }

        public Block[] Parse()
        {
            tokens.Position = 0;

            List<Block> blocks = new List<Block>();
            while (!tokens.EndOfTokens)
            {
                //throw new Exception("First token: '" + tokens.Peek().Value.ToString() + "'");

                if (tokens.Expect('@'))
                {
                    blocks.Add(ParseScript());
                }
                else if (tokens.Expect('$'))
                {
                    blocks.Add(ParseMovement());
                }
                else
                {
                    throw new Exception(tokens.Position + ": Unable to parse token '" + tokens.Read().ToString() + "'!");
                }
            }

            return blocks.ToArray();
        }

        private SBlock ParseScript()
        {
            // Check for @ again and eat it
            if (!tokens.Expect('@'))
            {
                throw new Exception("Expected '@' to start a script!");
            }
            else tokens.Read();

            // Get name
            if (!tokens.ExpectString())
            {
                throw new Exception("Expctecd script name!");
            }
            string name = tokens.Read().Value.ToString();

            // Check for {
            if (!tokens.Expect('{'))
            {
                throw new Exception("Expected '{' next!");
            }
            else tokens.Read();

            // Parse block
            SBlock block = new SBlock(name);
            while (!tokens.Expect('}'))
            {
                // Check for EOF
                if (tokens.EndOfTokens)
                {
                    throw new Exception("Unterminated script block!");
                }

                // Parse a line -- we can eat blank lines too.
                if (tokens.Expect(';')) { tokens.Read(); }
                else
                {
                    block.Commands.Add(ParseSCommand());
                }

                // continue
            }
            tokens.Read(); // Eat }

            return block;
        }

        private SCommand ParseSCommand()
        {
            // cmd
            ushort cmd = 0;
            if (tokens.ExpectString())
            {
                string cmdN = (string)tokens.Read().Value;
                if (!commands.IsCommand(cmdN))
                {
                    throw new Exception(cmdN + " is not a command!");
                }
                else cmd = commands.GetCommandValue(cmdN);
            }
            /*else if (tokens.ExpectUInt())
            {
                cmd = (ushort)(uint)tokens.Read().Value;
                if (commands.IsCommand(cmd))
                {
                    throw new Exception(cmd.ToString("X4") + " is not a command!");
                }
            }*/
            else
            {
                throw new Exception("Expected command!");
            }
            SCommand result = new SCommand(cmd);

            // Arguments
            string[] args = commands.GetCommandArguments(cmd);
            if (args != null)
            {
                // A null could be given if there are no args.
                foreach (string arg in args)
                {
                    if (arg != "-e") // -e is not a parameter, but a note to the decompiler.
                    {
                        result.Arguments.Add(ParseSArgument(arg));
                    }
                }
            }

            // End of line ;
            if (tokens.Expect(';'))
            {
                tokens.Read();
            }
            else
            {
                throw new Exception("Expected ';' to end line!");
            }

            return result;
        }

        private SArgument ParseSArgument(string arg)
        {
            // Try to parse argument
            SArgument result;
            if (arg == "-f" || arg == "-m")
            {
                if (tokens.ExpectString())
                {
                    result = new SPointer((string)tokens.Read().Value);
                }
                else
                {
                    throw new Exception("Expected script/fuction/movement name!");
                }
            }
            else if (arg == "u8")
            {
                if (tokens.ExpectUInt())
                {
                    result = new SNumber<byte>((byte)(uint)tokens.Read().Value);
                }
                else throw new Exception("Expected byte!");
            }
            else if (arg == "u16")
            {
                if (tokens.ExpectUInt())
                {
                    result = new SNumber<ushort>((ushort)(uint)tokens.Read().Value);
                }
                else throw new Exception("Expected ushort!");
            }
            else if (arg == "u32")
            {
                if (tokens.ExpectUInt())
                {
                    result = new SNumber<uint>((uint)tokens.Read().Value);
                }
                else throw new Exception("Expected ushort!");
            }
            else
            {
                throw new Exception("Invalid argument '" + arg + "'!");
            }

            return result;
        }

        private MBlock ParseMovement()
        {
            // Check for @ again and eat it
            if (!tokens.Expect('$'))
            {
                throw new Exception("Expected '$' to start a movement!");
            }
            else tokens.Read();

            // Get name
            if (!tokens.ExpectString())
            {
                throw new Exception("Expected movement name!");
            }
            string name = tokens.Read().Value.ToString();

            // Check for {
            if (!tokens.Expect('['))
            {
                throw new Exception("Expected '[' next!");
            }
            else tokens.Read();

            // Parse block
            MBlock block = new MBlock(name);
            while (!tokens.Expect(']'))
            {
                // Check for EOF
                if (tokens.EndOfTokens)
                {
                    throw new Exception("Unterminated movement block!");
                }

                // Parse a line -- we can eat blank lines too.
                if (tokens.Expect(';')) { tokens.Read(); }
                else
                {
                    block.Commands.Add(ParseMCommand());
                }

                // continue
            }
            tokens.Read(); // Eat }

            return block;
        }

        private MCommand ParseMCommand()
        {
            // cmd
            ushort cmd = 0;
            /*if (tokens.ExpectString()) // TODO -- there isn't a database for this yet
            {
                string cmdN = (string)tokens.Read().Value;
                if (!commands.IsCommand(cmdN))
                {
                    throw new Exception(cmdN + " is not a command!");
                }
                else cmd = commands.GetCommandValue(cmdN);
            }*/
            if (tokens.ExpectUInt())
            {
                uint temp = (uint)tokens.Read().Value;
                if (temp > ushort.MaxValue)
                {
                    throw new Exception("0x" + temp.ToString("X4") + " is not a valid movement!");
                }
                cmd = (ushort)temp;

                /*cmd = (ushort)(uint)tokens.Read().Value;
                if (commands.IsCommand(cmd))
                {
                    throw new Exception(cmd.ToString("X4") + " is not a command!");
                }*/
            }
            else
            {
                throw new Exception("Expected command!");
            }

            // steps
            ushort steps = 0;
            if (tokens.ExpectUInt())
            {
                uint temp = (uint)tokens.Read().Value;
                if (temp > ushort.MaxValue)
                {
                    throw new Exception("0x" + temp.ToString("X4") + " is not a valid step!");
                }
                steps = (ushort)temp;
            }
            else
            {
                throw new Exception("Expected number of steps!");
            }

            // End of line ;
            if (tokens.Expect(';'))
            {
                tokens.Read();
            }
            else
            {
                throw new Exception("Expected ';' to end line!");
            }

            return new MCommand(cmd, steps);
        }
    }

    /*
    /// <summary>
    /// A class to help with building the raw output of a script...
    /// </summary>
    public class ScriptBuilder
    {
        List<byte> buffer;

        public ScriptBuilder()
        {
            buffer = new List<byte>();
        }

        public byte[] ToArray()
        {
            return buffer.ToArray();
        }

        public void Clear()
        {
            buffer.Clear();
        }

        public void Append(byte val)
        {
            buffer.Add(val);
        }

        public void Append(ushort val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            buffer.AddRange(bytes);
        }

        public void Append(uint val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            buffer.AddRange(bytes);
        }
    }
    */
}
