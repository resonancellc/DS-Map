using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Block[] Compile(TokenReader scripts, TokenReader functions, TokenReader movements)
        {
            Parser sParser = new Parser(scripts, commands);

            Block[] sResult = sParser.Parse();

            return sResult;
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
                    ParseMovement();
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

        private void ParseMovement()
        {
            throw new NotImplementedException();
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
