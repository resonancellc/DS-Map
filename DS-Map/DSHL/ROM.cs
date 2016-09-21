using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Lost
{
    public static class ROM
    {
        public static void Extract(string filename, string directory)
        {
            // things to extract:
            // filesystem
            // arm7/9
            // arm overlay
            // header
            // banner
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
            Directory.CreateDirectory(directory);

            // TODO: this would be nice I guess
        }

        public static Header LoadHeader(string filename)
        {
            using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var br = new BinaryReader(fs))
            {
                var header = new Header();
                br.BaseStream.Position = 0L;

                header.Title = br.ReadString(12);
                header.Code = br.ReadString(4);
                header.Maker = br.ReadString(2);
                header.DeviceCode = br.ReadByte();                          // bit1 = DSi
                header.EncryptionSeed = br.ReadByte();
                header.FileLength = br.ReadByte();
                br.BaseStream.Seek(9L, SeekOrigin.Current);                 // 7 reserved, 2 unknown (used by DSi games)
                header.Version = br.ReadByte();
                header.InternalFlags = br.ReadByte();
                header.Arm9Offset = br.ReadUInt32();
                header.Arm9EntryAddress = br.ReadUInt32();
                header.Arm9LoadAddress = br.ReadUInt32();
                header.Arm9Length = br.ReadUInt32();
                header.Arm7Offset = br.ReadUInt32();
                header.Arm7EntryAddress = br.ReadUInt32();
                header.Arm7LoadAddress = br.ReadUInt32();
                header.Arm7Length = br.ReadUInt32();
                header.FNTOffset = br.ReadUInt32();                         // !!
                header.FNTLength = br.ReadUInt32();                         // !!
                header.FATOffset = br.ReadUInt32();                         // !!
                header.FATLength = br.ReadUInt32();                         // !!
                header.Arm9OverlayOffset = br.ReadUInt32();
                header.Arm9OverlaySize = br.ReadUInt32();
                header.Arm7OverlayOffset = br.ReadUInt32();
                header.Arm7OverlaySize = br.ReadUInt32();
                header.NormalRegisterSettings = br.ReadUInt32();
                header.SecureRegisterSettings = br.ReadUInt32();
                header.BannerOffset = br.ReadUInt32();                      // !!
                header.SecureAreaCRC = br.ReadUInt16();
                header.SecureTransferTimeout = br.ReadUInt16();
                header.Arm9AutoLoad = br.ReadUInt32();
                header.Arm7AutoLoad = br.ReadUInt32();
                header.SecureDisable = br.ReadUInt64();
                header.ROMLength = br.ReadUInt32();
                header.HeaderLength = br.ReadInt32();
                br.BaseStream.Seek(56L, SeekOrigin.Current);
                header.NintendoLogo = br.ReadBytes(156);
                header.NintendoLogoCRC = br.ReadUInt16();
                header.HeaderCRC = br.ReadUInt16();

                // note: DSi extended this format but we don't care
                return header;
            }
        }

        public static Banner LoadBanner(string filename)
        {
            using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var br = new BinaryReader(fs))
            {
                var banner = new Banner();

                // read banner data
                banner.Version = br.ReadUInt16();
                banner.CRC16 = br.ReadUInt16();
                br.BaseStream.Seek(6 + 0x16L, SeekOrigin.Current); // extra CRC's + reserved
                var iconData = br.ReadBytes(0x200);
                var iconPalette = br.ReadColors(16);
                banner.JapaneseTitle = Encoding.Unicode.GetString(br.ReadBytes(0x100));
                banner.EnglishTitle = Encoding.Unicode.GetString(br.ReadBytes(0x100));
                banner.FrenchTitle = Encoding.Unicode.GetString(br.ReadBytes(0x100));
                banner.GermanTitle = Encoding.Unicode.GetString(br.ReadBytes(0x100));
                banner.ItalianTitle = Encoding.Unicode.GetString(br.ReadBytes(0x100));
                banner.SpanishTitle = Encoding.Unicode.GetString(br.ReadBytes(0x100));

                // draw icon
                banner.Icon = new Bitmap(32, 32);

                int i = 0;
                for (int tileY = 0; tileY < 4; tileY++)
                {
                    for (int tileX = 0; tileX < 4; tileX++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x += 2)
                            {
                                var left = iconData[i] & 0xF;
                                var right = (iconData[i++] & 0xF0) >> 4;

                                banner.Icon.SetPixel(x + tileX * 8, y + tileY * 8, iconPalette[left]);
                                banner.Icon.SetPixel(x + 1 + tileX * 8, y + tileY * 8, iconPalette[right]);
                            }
                        }
                    }
                }

                banner.Icon.MakeTransparent(iconPalette[0]);

                return banner;
            }
        }
    }


    /*
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

        Directory LoadDirectory(Reader br, string name, int id)
        {
            br.BaseStream.Position = Header.FNTOffset + 8 * id;

            // directory main-table
            var subTableOffset = br.ReadUInt32();
            var fileID = br.ReadUInt16();
            var parentID = br.ReadUInt16();

            var dir = new Directory(name, id);

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
                ID = id,
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

        /// <summary>
        /// Returns the <see cref="File"/> at the given path within the <see cref="ROM"/>.
        /// </summary>
        /// <param name="path">The path of the file to return.</param>
        /// <returns>A <see cref="File"/> if it exists or <c>null</c> otherwise.</returns>
        public File GetFile(string path)
        {
            var parts = path.Split('\\');
            if (parts.Length == 0)
                return null;

            var f = 0;
            if (parts[0] == "" || parts[0] == "root")
                f++;
            Console.WriteLine("root");

            var currentDirectory = Root;
            for (; f < parts.Length - 1; f++)
            {
                for (int j = 0; j < f; j++)
                    Console.Write(" ");
                Console.WriteLine(parts[f]);

                currentDirectory = currentDirectory.GetDirectory(parts[f]);
                if (currentDirectory == null)
                    return null;
            }

            return currentDirectory.GetFile(parts[parts.Length - 1]);
        }

        /// <summary>
        /// Gets the <see cref="Directory"/> at the given path within the <see cref="ROM"/>.
        /// </summary>
        /// <param name="path">The path of the directory to return.</param>
        /// <returns>A <see cref="Directory"/> if it exists or <c>null</c> otherwise. </returns>
        public Directory GetDirectory(string path)
        {
            var parts = path.Split('\\');
            if (parts.Length == 0)
                return null;

            var f = 0;
            if (parts[0] == "" || parts[0] == "root")
                f++;

            var currentDirectory = Root;
            for (; f < parts.Length - 1; f++)
            {
                currentDirectory = currentDirectory.GetDirectory(parts[f]);
                if (currentDirectory == null)
                    return null;
            }

            return currentDirectory.GetDirectory(parts[parts.Length - 1]);
        }

        public class Directory
        {
            public Directory(string name, int id)
            {
                Name = name;
                ID = id;
            }

            public string Name { get; set; }
            public int ID { get; set; }

            public List<Directory> Directories { get; } = new List<Directory>();
            public List<File> Files { get; } = new List<File>();

            public Directory GetDirectory(string name)
            {
                for (int i = 0; i < Directories.Count; i++)
                {
                    if (Directories[i].Name == name)
                        return Directories[i];
                }
                return null;
            }

            public File GetFile(string name)
            {
                for (int i = 0; i < Files.Count; i++)
                {
                    if (Files[i].Name == name)
                        return Files[i];
                }
                return null;
            }
        }

        public class File
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public int Offset { get; set; }
            public int Length { get; set; }
        }
    }
    */

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
        public int HeaderLength;
        // 56 bytes reserved (00)
        public byte[] NintendoLogo; // 156 bytes
        public ushort NintendoLogoCRC, HeaderCRC;
        //public byte[] DebuggerReserved, ConfigurationSettings;
        // 160 bytes reserved (00)
    }

    public struct Banner
    {
        public ushort Version;
        public ushort CRC16;
        public string JapaneseTitle;
        public string EnglishTitle;
        public string FrenchTitle;
        public string GermanTitle;
        public string ItalianTitle;
        public string SpanishTitle;

        public Bitmap Icon;
    }
}
