using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DSMap.NDS;

namespace DSMap.Formats
{
    public class Map
    {
        public Map(byte[] file)
        {

        }

        // TODO: Map files

        public static string[] LoadMapNames(NARC narc)
        {
            string[] names = new string[narc.FileCount];
            for (int i = 0; i < names.Length; i++)
            {
                using (MemoryStream ms = narc.GetFileMemoryStream(i))
                {
                    // Read part of header
                    uint moveSize = ReadUInt32(ms);
                    uint objSize = ReadUInt32(ms);

                    // Skip to the location of the model's name
                    ms.Seek(moveSize + objSize + 60, SeekOrigin.Current);

                    // Read
                    byte[] buffer = new byte[16];
                    ms.Read(buffer, 0, 16);

                    names[i] = "";
                    foreach (byte b in buffer)
                    {
                        if (b == 0) break;
                        else names[i] += (char)b;
                    }
                }
            }
            return names;
        }

        private static uint ReadUInt32(MemoryStream ms)
        {
            byte[] buffer = new byte[4];
            ms.Read(buffer, 0, 4);
            return (uint)((buffer[3] << 24) | (buffer[2] << 16)
                | (buffer[1] << 8) | buffer[0]);
        }
    }
}
