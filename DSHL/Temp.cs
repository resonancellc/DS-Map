using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DSHL
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
            string path = GetTemporaryFileName();
            File.WriteAllBytes(path, buffer);
            return path;
        }

        /// <summary>
        /// Get the name for a new temporary file.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetTemporaryFileName()
        {
            // First, ensure the temporary directory still exists
            if (!Directory.Exists("temp"))
            {
                // Create
                DirectoryInfo di = Directory.CreateDirectory("temp");
                di.Attributes |= FileAttributes.Hidden;
            }

            // Generate a random file name
            string path = "temp\\temp";
            int id = 0;
            while (File.Exists(path + id + ".bin"))
            {
                id++;
            }
            return path + id + ".bin";
        }
    }
}
