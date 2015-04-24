using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DSHL.Formats.Pokémon.Scripting
{
    public class Tokenizer
    {
        public const int EOF = -1;

        public static Token[] Tokenize(string code)
        {
            List<Token> tokens = new List<Token>();

            StringReader sr = new StringReader(code);
            while (sr.Peek() != EOF)
            {
                char c = (char)sr.Peek();

                if (char.IsWhiteSpace(c))
                {
                    sr.Read();
                }
                else if (c == '#') // Command
                {
                    // Just consume the rest of this line
                    while (sr.Peek() != EOF && sr.Peek() != '\n')
                    {
                        sr.Read();
                    }
                }
                else if (c == ';' || c == '@' || c == '$') // New commnad/script start
                {
                    sr.Read();
                    tokens.Add(new Token(c));
                }
                else if (c == '{' || c == '}') // Block Open/Close
                {
                    sr.Read(); // Control characters
                    tokens.Add(new Token(c));
                }
                else if (c == '[' || c == ']')
                {
                    sr.Read(); // Control characters
                    tokens.Add(new Token(c));
                }
                else if (char.IsLetter(c))
                {
                    StringBuilder sb = new StringBuilder();
                    while (char.IsLetterOrDigit((char)sr.Peek()))
                    {
                        sb.Append((char)sr.Read());
                    }
                    tokens.Add(new Token(sb.ToString()));
                }
                else if (IsHexCharacter(c))
                {
                    StringBuilder sb = new StringBuilder();
                    while (IsHexCharacter((char)sr.Peek()))
                    {
                        sb.Append((char)sr.Read());
                    }
                    tokens.Add(new Token(TryTokenizeUInt(sb.ToString())));
                }
                else
                {
                    throw new Exception("Unexpected character '" + c + "'!");
                }
            }

            return tokens.ToArray();
        }

        private static uint TryTokenizeUInt(string s)
        {
            //MessageBox.Show("'" + s + "'");   
            if (s.StartsWith("0x"))
            {
                //return Convert.ToUInt32(s, 16);
                s = s.Substring(2);
            }

            uint u;
            if (uint.TryParse(s, System.Globalization.NumberStyles.HexNumber, null, out u)) { return u; }
            else if (uint.TryParse(s, out u)) { return u; }
            else throw new Exception("Invalid number format '" + s + "'!");
        }

        private static bool IsHexCharacter(char c)
        {
            if (c == 'x') return true;
            else if (char.IsDigit(c)) return true;
            else if (c >= 'A' && c <= 'F') return true;
            else if (c >= 'a' && c <= 'a') return true;
            else return false;
        }

        public class Token
        {
            public object Value;

            public Token()
            {
                Value = null;
            }

            public Token(char c)
            {
                Value = c.ToString();
            }

            public Token(string s)
            {
                Value = s;
            }

            public Token(uint i)
            {
                Value = i;
            }

            public override string ToString()
            {
                if (Value != null)
                    return Value.ToString();
                else
                    return "-";
            }
        }
    }

    public class Compiler
    {
    }
}
