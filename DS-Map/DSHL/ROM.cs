using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost
{
    using Stream = System.IO.Stream;
    using Reader = System.IO.BinaryReader;
    using Writer = System.IO.BinaryWriter;

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
            stream = System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);

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

        public Header Header { get; } = new Header();
        public Directory Root { get; private set; } = null;

        void Load()
        {
            if (stream == null)
                return;

            using (var br = new Reader(stream, Encoding.Default, true))
            {
                // load ROM header
                LoadHeader(br);

                // load filesystem
                LoadFilesystem(br);
            }
        }

        void LoadHeader(Reader br)
        {
            Header.Title = br.ReadString(12);
            Header.Code = br.ReadString(4);
            Header.Maker = br.ReadString(2);
            Header.DeviceCode = br.ReadByte();                          // bit1 = DSi
            Header.EncryptionSeed = br.ReadByte();
            Header.FileLength = br.ReadByte();
            br.BaseStream.Seek(9L, System.IO.SeekOrigin.Current);       // 7 reserved, 2 unknown (used by DSi games)
            Header.Version = br.ReadByte();
            Header.InternalFlags = br.ReadByte();
            Header.Arm9Offset = br.ReadUInt32();
            Header.Arm9EntryAddress = br.ReadUInt32();
            Header.Arm9LoadAddress = br.ReadUInt32();
            Header.Arm9Length = br.ReadUInt32();
            Header.Arm7Offset = br.ReadUInt32();
            Header.Arm7EntryAddress = br.ReadUInt32();
            Header.Arm7LoadAddress = br.ReadUInt32();
            Header.Arm7Length = br.ReadUInt32();
            Header.FNTOffset = br.ReadUInt32();                         // !!
            Header.FNTLength = br.ReadUInt32();                         // !!
            Header.FATOffset = br.ReadUInt32();                         // !!
            Header.FATLength = br.ReadUInt32();                         // !!
            Header.Arm9OverlayOffset = br.ReadUInt32();
            Header.Arm9OverlaySize = br.ReadUInt32();
            Header.Arm7OverlayOffset = br.ReadUInt32();
            Header.Arm7OverlaySize = br.ReadUInt32();
            Header.NormalRegisterSettings = br.ReadUInt32();
            Header.SecureRegisterSettings = br.ReadUInt32();
            Header.BannerOffset = br.ReadUInt32();                      // !!
            Header.SecureAreaCRC = br.ReadUInt16();
            Header.SecureTransferTimeout = br.ReadUInt16();
            Header.Arm9AutoLoad = br.ReadUInt32();
            Header.Arm7AutoLoad = br.ReadUInt32();
            Header.SecureDisable = br.ReadUInt64();
            Header.ROMLength = br.ReadUInt32();
            Header.HeaderLength = br.ReadUInt32();
            br.BaseStream.Seek(56L, System.IO.SeekOrigin.Current);
            Header.NintendoLogo = br.ReadBytes(156);
            Header.NintendoLogoCRC = br.ReadUInt16();
            Header.HeaderCRC = br.ReadUInt16();

            // note: DSi extended this format but we don't care
        }

        void LoadFilesystem(Reader br)
        {
            // http://problemkaputt.de/gbatek.htm#dscartridgenitroromandnitroarcfilesystems

            // files:       0000 - EFFF
            // directories: F000 - FFFF
            Root = LoadDirectory(br, "root", 0);
        }

        void ppp(Directory d, int i)
        {
            for (int j = 0; j < i; j++)
                Console.Write(" ");
            Console.WriteLine(d.Name);

            foreach (var dir in d.Directories)
                ppp(dir, i + 1);

            foreach (var f in d.Files)
            {
                for (int j = 0; j < i + 1; j++)
                    Console.Write(" ");
                Console.WriteLine(f.Name);
            }
        }

        Directory LoadDirectory(Reader br, string name, int id)
        {
            br.BaseStream.Position = Header.FNTOffset + 8 * id;

            // directory main-table
            var subTableOffset = br.ReadUInt32();
            var fileID = br.ReadUInt16();
            var parentID = br.ReadUInt16();

            var dir = new Directory()
            {
                Name = name,
                ID = id,
            };

            br.BaseStream.Position = Header.FNTOffset + subTableOffset;
            for (;;)
            {
                var entryData = br.ReadByte();

                var nameLength = entryData & 0x7F;
                if (nameLength == 0)
                    break;

                var entryName = br.ReadString(nameLength);
                if (entryData > 0x80)
                {
                    var subTableID = br.ReadUInt16() & 0xFFF;
                    var i = br.BaseStream.Position;
                    {
                        // load directory
                        dir.Directories.Add(LoadDirectory(br, entryName, subTableID));
                    }
                    br.BaseStream.Position = i;
                }
                else
                {
                    var i = br.BaseStream.Position;
                    dir.Files.Add(LoadFile(br, entryName, fileID++));
                    br.BaseStream.Position = i;
                }
                // note: 0x80 is reserved so this logic is not ideal
            }

            return dir;
        }

        File LoadFile(Reader br, string name, int id)
        {
            var f = new File()
            {
                Name = name,
                ID = id
            };

            // read FAT
            br.BaseStream.Position = Header.FATOffset + 8 * id;

            var start = br.ReadInt32();
            var end = br.ReadInt32();

            f.Offset = start;
            f.Length = end - start;

            return f;
        }

        void SaveFilesystem(Writer w)
        {
            // idea here is packing all the files in nicely
            // root directory, we need a count of all directories...

        }

        public class Directory
        {
            public string Name { get; set; }
            public int ID { get; set; }

            public List<Directory> Directories { get; } = new List<Directory>();
            public List<File> Files { get; } = new List<File>();
        }

        public class File
        {
            public string Name { get; set; }
            public int ID { get; set; }

            public int Offset { get; set; }
            public int Length { get; set; }
        }
    }

    public class Header
    {
        // It follows this format exactly
        public string Title, Code, Maker;
        public byte DeviceCode, EncryptionSeed;
        public byte FileLength;
        // 9 bytes reserved (00)
        public byte Version, InternalFlags;
        public uint Arm9Offset, Arm9EntryAddress, Arm9LoadAddress, Arm9Length;
        public uint Arm7Offset, Arm7EntryAddress, Arm7LoadAddress, Arm7Length;
        public uint FNTOffset, FNTLength;
        public uint FATOffset, FATLength;
        public uint Arm9OverlayOffset, Arm9OverlaySize;
        public uint Arm7OverlayOffset, Arm7OverlaySize;
        public uint NormalRegisterSettings, SecureRegisterSettings;
        public uint BannerOffset;
        public ushort SecureAreaCRC, SecureTransferTimeout;
        public uint Arm9AutoLoad, Arm7AutoLoad;
        public ulong SecureDisable;
        public uint ROMLength;
        public uint HeaderLength;
        // 56 bytes reserved (00)
        public byte[] NintendoLogo; // 156 bytes
        public ushort NintendoLogoCRC, HeaderCRC;
        //public byte[] DebuggerReserved, ConfigurationSettings;
        // 160 bytes reserved (00)
    }
}
