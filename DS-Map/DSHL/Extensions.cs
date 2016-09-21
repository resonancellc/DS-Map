using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Lost
{
    public static class Extensions
    {
        // for some odd reason Encoding.GetString was not working as desired
        // by returning \0 characters in the resulting string
        public static string ReadString(this BinaryReader br, int length)
        {
            var buffer = br.ReadBytes(length);
            var sb = new StringBuilder();

            foreach (var b in buffer)
            {
                if (b == 0)
                    break;

                sb.Append((char)b);
            }

            return sb.ToString();
        }

        public static Color ReadColor(this BinaryReader br)
        {
            var c = br.ReadUInt16();
            var r = (c & 0x1F) << 3;
            var g = ((c >> 5) & 0x1F) << 3;
            var b = ((c >> 10) & 0x1F) << 3;
            return Color.FromArgb(r, g, b);
        }

        public static Color[] ReadColors(this BinaryReader br, int count)
        {
            var c = new Color[count];
            for (int i = 0; i < count; i++)
                c[i] = br.ReadColor();
            return c;
        }

        public static void WriteInt32(this Stream stream, int i)
        {
            stream.Write(BitConverter.GetBytes(i), 0, 4);
        }
    }
}
