using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost
{
    /// <summary>
    /// Represents an NDS ROM filesystem.
    /// </summary>
    public class ROM : IDisposable
    {
        Stream stream;
        bool disposed = false;

        public ROM(string filename)
        {
            // open the ROM file, do not let other sources edit it
            stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

            Load();
        }

        ~ROM()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                stream?.Dispose();
                disposed = true;
            }
        }

        void Load()
        {
            if (stream == null)
                return;


        }
    }
}
