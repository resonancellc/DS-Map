using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
