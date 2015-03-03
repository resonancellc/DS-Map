using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Security.AccessControl;

namespace DSMap
{
    /// <summary>
    /// Temporary file system for data I/O.
    /// </summary>
    public static class Temporary
    {
        public static void Create()
        {
            // Clean up existing
            Dispose();

            // And create
            DirectoryInfo di = Directory.CreateDirectory("temp");
            di.Attributes |= FileAttributes.Hidden;
        }

        public static void Dispose()
        {
            if (Directory.Exists("temp"))
            {
                Directory.Delete("temp", true);
            }
        }

        /// <summary>
        /// Create a temporary file for a buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string CreateTemporaryFile(byte[] buffer)
        {
            string path = "temp\\temp";
            int id = 0;
            while (File.Exists(path + id + ".bin"))
            {
                id++;
            }

            File.WriteAllBytes(path + id + ".bin", buffer);
            return path + id + ".bin";
        }
    }
}
