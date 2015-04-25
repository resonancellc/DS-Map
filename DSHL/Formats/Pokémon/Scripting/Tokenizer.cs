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
        private const int EOF = -1;

        public static TokenReader Tokenize(string code)
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
                else if (c == '/') // Command
                {
                    // Try to parse this comment start
                    sr.Read();
                    if (sr.Peek() == EOF)
                    {
                        throw new Exception("Unexpected character '" + c + "'!");
                    }
                    if ((char)sr.Peek() == '/')
                    {
                        sr.Read();
                    }
                    else
                    {
                        throw new Exception("Unexpected character '" + c + "'!");
                    }

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
                else if (c == '_' || char.IsLetter(c))
                {
                    StringBuilder sb = new StringBuilder();
                    do
                    {
                        sb.Append(c);
                        sr.Read();

                        if (sr.Peek() == EOF)
                        {
                            break;
                        }
                        else
                        {
                            c = (char)sr.Peek();
                        }
                    } while (c == '_' || char.IsLetterOrDigit(c));

                    tokens.Add(new Token(sb.ToString()));
                }
                else if (IsHexCharacter(c))
                {
                    StringBuilder sb = new StringBuilder();
                    do
                    {
                        sb.Append(c);
                        sr.Read();

                        if (sr.Peek() == EOF)
                        {
                            break;
                        }
                        else
                        {
                            c = (char)sr.Peek();
                        }
                    } while (IsHexCharacter(c));
                    tokens.Add(new Token(TryTokenizeUInt(sb.ToString())));
                }
                else
                {
                    sr.Read();
                    throw new Exception("Unexpected character '" + c + "'!");
                }
            }

            return new TokenReader(tokens);
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
    }

    public class Token
    {
        // Invalid token
        public static Token Invalid = new Token();

        public object Value;

        public Token()
        {
            Value = null;
        }

        public Token(char c)
        {
            Value = c;
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

    /// <summary>
    /// Provides an easy way to walk through an array of Tokens.
    /// It mimics a System.IO.TextReader, only returning Tokens.
    /// </summary>
    public class TokenReader : IEnumerable<Token>
    {
        private Token[] tokens;
        private int pos;

        /// <summary>
        /// Creates a new TokenReader from an array of tokens.
        /// </summary>
        /// <param name="tokens"></param>
        public TokenReader(Token[] tokens)
        {
            this.tokens = tokens;
            this.pos = 0;
        }

        /// <summary>
        /// Creates a new TokenReader from a list of tokens.
        /// </summary>
        /// <param name="tokens"></param>
        public TokenReader(List<Token> tokens)
        {
            this.tokens = tokens.ToArray();
            this.pos = 0;
        }

        /// <summary>
        /// Read the next token from the array without advancing the position.
        /// </summary>
        /// <returns>The next token.</returns>
        public Token Peek()
        {
            if (pos >= tokens.Length) return Token.Invalid;
            else return tokens[pos];
        }

        /// <summary>
        /// Read the next token from the array and advance the position by one.
        /// </summary>
        /// <returns>The next token.</returns>
        public Token Read()
        {
            if (pos >= tokens.Length) return Token.Invalid;
            else
            {
                Token t = tokens[pos];
                pos += 1;
                return t;
            }
        }

        /// <summary>
        /// Returns whether the next token will be the given character.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns>A boolean.</returns>
        public bool Expect(char c)
        {
            object val = Peek().Value;
            if (val is char)
            {
                return ((char)val == c);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns whether the next token will be the given token.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>A boolean.</returns>
        public bool Expect(Token token)
        {
            // Not the safest?
            return (token.Value == Peek().Value);
        }

        /// <summary>
        /// Returns whether the next token will be a string token.
        /// </summary>
        /// <returns>A boolean.</returns>
        public bool ExpectString()
        {
            return (Peek().Value is string);
        }

        /// <summary>
        /// Returns whether the next token will be a uint token.
        /// </summary>
        /// <returns>A boolean.</returns>
        public bool ExpectUInt()
        {
            return (Peek().Value is uint);
        }

        // So we can use foreach loops with this.
        #region IEnumerable Members

        public IEnumerator<Token> GetEnumerator()
        {
            return tokens.ToList<Token>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return tokens.GetEnumerator();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the position of the TokenReader.
        /// </summary>
        public int Position
        {
            get { return pos; }
            set { pos = value; }
        }

        /// <summary>
        /// Gets the tokens of the TokenReader.
        /// </summary>
        public Token[] Tokens
        {
            get { return tokens; }
        }

        /// <summary>
        /// Gets a value that indicates whether the position is beyond the number of tokens.
        /// </summary>
        public bool EndOfTokens
        {
            get { return (pos >= tokens.Length); }
        }

        #endregion
    }
}
