using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSMap.Formats
{
    /// <summary>
    /// Repeasents a collection of encrypted strings, as found in the NDS Pokémon games.
    /// </summary>
    public class PkmnText// : ICollection
    {
        internal struct Message
        {
            //public int Id;
            public int StartOffset;
            public string Text;
            public int Key2;
            public int RealKey;
            public int Size;
        }

        // TODO: make this a list
        private Message[] messages;
        private ushort originalKey;

        public PkmnText(MemoryStream ms, bool trimEnding = false)
        {
            using (BinaryReader br = new BinaryReader(ms))
            {
                short count = br.ReadInt16();
                messages = new Message[count];

                originalKey = br.ReadUInt16();
                int key = (originalKey * 0x2FD) & ushort.MaxValue;

                //ArrayList originalOffsets = new ArrayList();
                //List<int> originalSizes = new List<int>();

                // Decode message headers
                for (int i = 0; i < count; i++)
                {
                    Message msg = new Message();
                    msg.Key2 = key * (i + 1) & ushort.MaxValue;
                    msg.RealKey = msg.Key2 | msg.Key2 << 16;

                    int start = br.ReadInt32();
                    msg.StartOffset = start ^ msg.RealKey;
                    //originalOffsets.Add(start);

                    int size = br.ReadInt32();
                    msg.Size = size ^ msg.RealKey;
                    //originalSizes.Add(size);

                    //messages.Add(msg);
                    messages[i] = msg;
                }

                // Decode the messages
                for (int i = 0; i < count; i++)
                {
                    //MessageBox.Show("Getting message " + i + "...");
                    Message msg = messages[i];

                    key = 0x91BD3 * (i + 1) & ushort.MaxValue;
                    int len = 0;

                    bool isVar = false;
                    StringBuilder sb = new StringBuilder();
                    for (int n = 0; n < msg.Size; n++)
                    {
                        ushort u = (ushort)(br.ReadUInt16() ^ key);
                        if (u == 0xFFFE)
                        {
                            // Variable
                            isVar = true;
                            sb.Append("[var ");
                            len++;
                        }
                        else
                        {
                            string chr = string.Empty;
                            if (isVar)
                            {
                                chr = GetVarCharacter(u);
                                len++;
                            }
                            else
                            {
                                chr = GetCharacter(u);
                            }
                            sb.Append(chr);
                        }

                        if (len == 4)
                        {
                            sb.Append("]");
                            isVar = false;
                            len = 0;
                        }

                        key = key + 0x493D & ushort.MaxValue;

                    }
                    msg.Text = sb.ToString();

                    if (trimEnding) msg.Text = msg.Text.Replace("\\0", ""); // ~~~

                    messages[i] = msg;
                }
            }
        }

        public byte[] Save()
        {
            // Saving is disabled for now.
            return null;// new byte[] { 0 };
        }

        #region Get Characters

        private string GetVarCharacter(ushort u)
        {
            switch (u)
            {
                // Parameters
                case 0:
                    return ".0 ";
                case 1:
                    return ".1 ";
                // Names
                case 0x100:
                    return "poke: ";
                case 0x101:
                    return "poke2: ";
                case 0x103:
                    return "name: ";
                case 0x104:
                    return "place: ";
                case 0x106:
                    return "move: ";
                case 0x107:
                    return "nat: ";
                case 0x108:
                    return "item: ";
                case 0x10A:
                    return "poffin item: ";
                case 0x10E:
                    return "trainer: ";
                case 0x118:
                    return "key item: ";
                case 0x11F:
                    return "acc.: ";
                case 0x132:
                    return "num: ";
                case 0x133:
                    return "level: ";
                case 0x134:
                    return "num2: ";
                case 0x135:
                    return "num3: ";
                case 0x137:
                    return "money: ";
                case 0x203:
                    return "color: ";
            }
            return "?";
        }

        private string GetCharacter(ushort u)
        {
            switch (u)
            {
                case 0x0:
                    return ".0 ";
                case 0x1:
                    return ".1 ";
                case 0xA2:
                    return "０";
                case 0xA3:
                    return "１";
                case 0xA4:
                    return "２";
                case 0xA5:
                    return "３";
                case 0xA6:
                    return "４";
                case 0xA7:
                    return "５";
                case 0xA8:
                    return "６";
                case 0xA9:
                    return "７";
                case 0xAA:
                    return "８";
                case 0xAB:
                    return "９";
                case 0xAC:
                    return "Ａ";
                case 0xAD:
                    return "Ｂ";
                case 0xAE:
                    return "Ｃ";
                case 0xAF:
                    return "Ｄ";
                case 0xB0:
                    return "Ｅ";
                case 0xB1:
                    return "Ｆ";
                case 0xB2:
                    return "Ｇ";
                case 0xB3:
                    return "Ｈ";
                case 0xB4:
                    return "Ｉ";
                case 0xB5:
                    return "Ｊ";
                case 0xB6:
                    return "Ｋ";
                case 0xB7:
                    return "Ｌ";
                case 0xB8:
                    return "Ｍ";
                case 0xB9:
                    return "Ｎ";
                case 0xBA:
                    return "Ｏ";
                case 0xBB:
                    return "Ｐ";
                case 0xBC:
                    return "Ｑ";
                case 0xBD:
                    return "Ｒ";
                case 0xBE:
                    return "Ｓ";
                case 0xBF:
                    return "Ｔ";
                case 0xC0:
                    return "Ｕ";
                case 0xC1:
                    return "Ｖ";
                case 0xC2:
                    return "Ｗ";
                case 0xC3:
                    return "Ｘ";
                case 0xC4:
                    return "Ｙ";
                case 0xC5:
                    return "Ｚ";
                case 0xC6:
                    return "ａ";
                case 0xC7:
                    return "ｂ";
                case 0xC8:
                    return "c";
                case 0xC9:
                    return "d";
                case 0xCA:
                    return "e";
                case 0xCB:
                    return "f"; ;
                case 0xCC:
                    return "g";
                case 0xCD:
                    return "h";
                case 0xCE:
                    return "i";
                case 0xCF:
                    return "j";
                case 0xD0:
                    return "k";
                case 0xD1:
                    return "l";
                case 0xD2:
                    return "m";
                case 0xD3:
                    return "n";
                case 0xD4:
                    return "o";
                case 0xD5:
                    return "p";
                case 0xD6:
                    return "q";
                case 0xD7:
                    return "r";
                case 0xD8:
                    return "s";
                case 0xD9:
                    return "t";
                case 0xDA:
                    return "u";
                case 0xDB:
                    return "v";
                case 0xDC:
                    return "w";
                case 0xDD:
                    return "x";
                case 0xDE:
                    return "y";
                case 0xDF:
                    return "z";
                case 0x121:
                    return "0";
                case 0x122:
                    return "1";
                case 0x123:
                    return "2";
                case 0x124:
                    return "3";
                case 0x125:
                    return "4";
                case 0x126:
                    return "5";
                case 0x127:
                    return "6";
                case 0x128:
                    return "7";
                case 0x129:
                    return "8";
                case 0x12A:
                    return "9";
                case 0x12B:
                    return "A"; ;
                case 0x12C:
                    return "B";
                case 0x12D:
                    return "C"; ;
                case 0x12E:
                    return "D";
                case 0x12F:
                    return "E";
                case 0x130:
                    return "F";
                case 0x131:
                    return "G";
                case 0x132:
                    return "H";
                case 0x133:
                    return "I";
                case 0x134:
                    return "J";
                case 0x135:
                    return "K";
                case 0x136:
                    return "L";
                case 0x137:
                    return "M";
                case 0x138:
                    return "N";
                case 0x139:
                    return "O";
                case 0x13A:
                    return "P";
                case 0x13B:
                    return "Q";
                case 0x13C:
                    return "R";
                case 0x13D:
                    return "S";
                case 0x13E:
                    return "T";
                case 0x13F:
                    return "U";
                case 0x140:
                    return "V";
                case 0x141:
                    return "W";
                case 0x142:
                    return "X";
                case 0x143:
                    return "Y";
                case 0x144:
                    return "Z";
                case 0x145:
                    return "a";
                case 0x146:
                    return "b";
                case 0x147:
                    return "c";
                case 0x148:
                    return "d";
                case 0x149:
                    return "e";
                case 0x14A:
                    return "f";
                case 0x14B:
                    return "g";
                case 0x14C:
                    return "h";
                case 0x14D:
                    return "i";
                case 0x14E:
                    return "j";
                case 0x14F:
                    return "k";
                case 0x150:
                    return "l";
                case 0x151:
                    return "m";
                case 0x152:
                    return "n";
                case 0x153:
                    return "o";
                case 0x154:
                    return "p";
                case 0x155:
                    return "q";
                case 0x156:
                    return "r";
                case 0x157:
                    return "s";
                case 0x158:
                    return "t";
                case 0x159:
                    return "u";
                case 0x15A:
                    return "v";
                case 0x15B:
                    return "w";
                case 0x15C:
                    return "x";
                case 0x15D:
                    return "y";
                case 0x15E:
                    return "z";
                case 0x15F:
                    return "À";
                case 0x160:
                    return "Á";
                case 0x161:
                    return "Â";
                case 0x162:
                    return "\x0162";
                case 0x163:
                    return "Ä";
                case 0x164:
                    return "\x0164";
                case 0x165:
                    return "\x0165";
                case 0x166:
                    return "Ç";
                case 0x167:
                    return "È";
                case 0x168:
                    return "É";
                case 0x169:
                    return "Ê";
                case 0x16A:
                    return "Ë";
                case 0x16B:
                    return "Ì";
                case 0x16C:
                    return "Í";
                case 0x16D:
                    return "Î";
                case 0x16E:
                    return "Ï";
                case 0x16F:
                    return "\x016F";
                case 0x170:
                    return "Ñ";
                case 0x171:
                    return "Ò";
                case 0x172:
                    return "Ó";
                case 0x173:
                    return "Ô";
                case 0x174:
                    return "\x0174";
                case 0x175:
                    return "Ö";
                case 0x176:
                    return "×";
                case 0x177:
                    return "\x0177";
                case 0x178:
                    return "Ù";
                case 0x179:
                    return "Ú";
                case 0x17A:
                    return "Û";
                case 0x17B:
                    return "Ü";
                case 0x17C:
                    return "\x017C";
                case 0x17D:
                    return "\x017D";
                case 0x17E:
                    return "ß";
                case 0x17F:
                    return "à";
                case 0x180:
                    return "á";
                case 0x181:
                    return "â";
                case 0x182:
                    return "\x0182";
                case 0x183:
                    return "ä";
                case 0x184:
                    return "\x0184";
                case 0x185:
                    return "\x0185";
                case 0x186:
                    return "ç";
                case 0x187:
                    return "è";
                case 0x188:
                    return "é";
                case 0x189:
                    return "ê";
                case 0x18A:
                    return "ë";
                case 0x18B:
                    return "ì";
                case 0x18C:
                    return "í";
                case 0x18D:
                    return "î";
                case 0x18E:
                    return "ï";
                case 0x18F:
                    return "\x018F";
                case 0x190:
                    return "ñ";
                case 0x191:
                    return "ò";
                case 0x192:
                    return "ó";
                case 0x193:
                    return "ô";
                case 0x194:
                    return "\x0194";
                case 0x195:
                    return "ö";
                case 0x196:
                    return "÷";
                case 0x197:
                    return "\x0197";
                case 0x198:
                    return "ù";
                case 0x199:
                    return "ú";
                case 0x19A:
                    return "û";
                case 0x19B:
                    return "ü";
                case 0x19C:
                    return "\x019C";
                case 0x19D:
                    return "\x019D";
                case 0x19E:
                    return "\x019E";
                case 0x19F:
                    return "Œ";
                case 0x1A0:
                    return "œ";
                case 0x1A1:
                    return "\x01A1";
                case 0x1A2:
                    return "\x01A2";
                case 0x1A3:
                    return "ª";
                case 0x1A4:
                    return "º";
                case 0x1A5:
                    return "ᵉʳ";
                case 0x1A6:
                    return "ʳᵉ";
                case 0x1A7:
                    return "ʳ";
                case 0x1A8:
                    return "¥";
                case 0x1A9:
                    return "¡";
                case 0x1AA:
                    return "¿";
                case 0x1AB:
                    return "!";
                case 0x1AC:
                    return "?";
                case 0x1AD:
                    return ",";
                case 0x1AE:
                    return ".";
                case 0x1AF:
                    return "...";
                case 0x1B0:
                    return "·";
                case 0x1B1:
                    return "/";
                case 0x1B2:
                    return "‘";
                case 0x1B3:
                    return "'";
                case 0x1B4:
                    return "“";
                case 0x1B5:
                    return "”";
                case 0x1B6:
                    return "„";
                case 0x1B7:
                    return "«";
                case 0x1B8:
                    return "»";
                case 0x1B9:
                    return "(";
                case 0x1BA:
                    return ")";
                case 0x1BB:
                    return "♂";
                case 0x1BC:
                    return "♀";
                case 0x1BD:
                    return "+";
                case 0x1BE:
                    return "-";
                case 0x1BF:
                    return "*";
                case 0x1C0:
                    return "#";
                case 0x1C1:
                    return "=";
                case 0x1C2:
                    return "\and";
                case 0x1C3:
                    return "~";
                case 0x1C4:
                    return ":";
                case 0x1C5:
                    return ";";
                case 0x1C6:
                    return "„";
                case 0x1C7:
                    return "«";
                case 0x1C8:
                    return "»";
                case 0x1C9:
                    return "(";
                case 0x1CA:
                    return ")";
                case 0x1CB:
                    return "♂";
                case 0x1CC:
                    return "♀";
                case 0x1CD:
                    return "+";
                case 0x1CE:
                    return "-";
                case 0x1CF:
                    return "*";
                case 0x1D2:
                    return "%";
                case 0x1DE:
                    return " ";
                case 0xE000:
                    return "\\n";
                case 0x25BC:
                    return "\\r";
                case 0x25BD:
                    return "\\v";
                case 0xFFFE:
                    return "[]";
                case 0xFFFF:
                    return "\\0";

            }
            return "\\x" + u.ToString("X4");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets one of the strings in this PkmnText.
        /// </summary>
        /// <param name="index">The index of the string.</param>
        /// <returns>A string.</returns>
        public string this[int index]
        {
            get { return messages[index].Text; }
            set { messages[index].Text = value; }
        }

        /// <summary>
        /// Gets the number of strings in this PkmnText.
        /// </summary>
        public int Count
        {
            get { return messages.Length; }
        }

        /// <summary>
        /// Gets the encryption key used by this PkmnText.
        /// </summary>
        public ushort Key
        {
            get { return originalKey; }
        }

        #endregion
    }
}
