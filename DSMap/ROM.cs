using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DSMap
{
    public class ROM
    {
        private string _file;
        private string _directory;
        private rHeader _header;
        private rBanner _banner;

        public ROM()
        {
            _file = string.Empty;
            _directory = string.Empty;
            _header = new rHeader();
            _banner = new rBanner();
        }

        ~ROM()
        {
            // A bit of cleanup
            if (_banner.Image != null) _banner.Image.Dispose();
        }

        /// <summary>
        /// Gets whether or not the ROM has been loaded and extracted.
        /// </summary>
        /// <returns></returns>
        public bool IsLoaded()
        {
            // See if the files have even been set
            if (_file == string.Empty || _directory == string.Empty) return false;

            // Check if the files exist
            if (!File.Exists(_file) || !Directory.Exists(_directory)) return false;

            // Success
            return true;
        }

        /// <summary>
        /// Begin a process to build the ROM using ndstool.exe and wait for it to finish.
        /// </summary>
        public void Build()
        {
            // Only if it is loaded do we allow a build
            if (IsLoaded())
            {
                // Construct process
                string dataDirectory = Path.Combine(_directory, "root");
                string overlayDirectory = Path.Combine(_directory, "overlay");

                string args = "-c \"" + _file + "\"";
                args += " -9 \"" + Path.Combine(_directory, "arm9.bin") + "\"";
                args += " -7 \"" + Path.Combine(_directory, "arm7.bin") + "\"";
                args += " -y9 \"" + Path.Combine(_directory, "y9.bin") + "\"";
                args += " -y7 \"" + Path.Combine(_directory, "y7.bin") + "\"";
                args += " -d \"" + dataDirectory + "\"";
                args += " -y \"" + overlayDirectory + "\"";
                args += " -t \"" + Path.Combine(_directory, "banner.bin") + "\"";
                args += " -h \"" + Path.Combine(_directory, "header.bin") + "\"";

                Process p = new Process();
                p.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, "ndstool.exe");
                p.StartInfo.Arguments = args;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            }
        }

        /// <summary>
        /// Begin a process to extract the ROM using ndstool.exe and wait for it to finish.
        /// </summary>
        public void Extract()
        {
            if (_file != string.Empty && File.Exists(_file))
            {
                string dataDirectory = Path.Combine(_directory, "root");
                string overlayDirectory = Path.Combine(_directory, "overlay");

                Directory.CreateDirectory(_directory);
                Directory.CreateDirectory(dataDirectory);
                Directory.CreateDirectory(overlayDirectory);

                string args = "-x \"" + _file + "\"";
                args += " -9 \"" + Path.Combine(_directory, "arm9.bin") + "\"";
                args += " -7 \"" + Path.Combine(_directory, "arm7.bin") + "\"";
                args += " -y9 \"" + Path.Combine(_directory, "y9.bin") + "\"";
                args += " -y7 \"" + Path.Combine(_directory, "y7.bin") + "\"";
                args += " -d \"" + dataDirectory + "\"";
                args += " -y \"" + overlayDirectory + "\"";
                args += " -t \"" + Path.Combine(_directory, "banner.bin") + "\"";
                args += " -h \"" + Path.Combine(_directory, "header.bin") + "\"";

                Process p = new Process();
                p.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, "ndstool.exe");
                p.StartInfo.Arguments = args;
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                p.WaitForExit();
            }
        }

        /// <summary>
        /// Load and extract the ROM.
        /// </summary>
        /// <param name="file">The path of the ROM.</param>
        public void Load(string file)
        {
            // Yeah...
            if (!File.Exists(file)) throw new FileNotFoundException();

            // Set up the files
            _file = file;
            _directory = Path.Combine(Path.GetDirectoryName(_file), Path.GetFileNameWithoutExtension(_file));

            // For now, we won't bother asking about it.
            // Just do the extraction if it hasn't been done yet
            if (!Directory.Exists(_directory))
            {
                // Yeah
                Extract();
            }
            else
            {
                MessageBox.Show("This ROM has already been extracted!\nIt will be loaded from there.", "Hooray!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Now, load the relevant information
            using (BinaryReader br = new BinaryReader(File.OpenRead(Path.Combine(_directory, "header.bin"))))
            {
                // Header
                _header.Title = Encoding.UTF8.GetString(br.ReadBytes(12));
                _header.Code = Encoding.UTF8.GetString(br.ReadBytes(4));
                _header.Maker = Encoding.UTF8.GetString(br.ReadBytes(2));

                _header.DeviceCode = br.ReadByte();
                _header.EncryptionSeed = br.ReadByte();
                _header.FileLength = br.ReadByte();
                br.BaseStream.Seek(9L, SeekOrigin.Current);

                _header.Version = br.ReadByte();
                _header.InternalFlags = br.ReadByte();

                _header.Arm9Offset = br.ReadUInt32();
                _header.Arm9Entry = br.ReadUInt32();
                _header.Arm9Load = br.ReadUInt32();
                _header.Arm9Length = br.ReadUInt32();

                _header.Arm7Offset = br.ReadUInt32();
                _header.Arm7Entry = br.ReadUInt32();
                _header.Arm7Load = br.ReadUInt32();
                _header.Arm7Length = br.ReadUInt32();

                _header.FNTOffset = br.ReadUInt32();
                _header.FNTSize = br.ReadUInt32();
                _header.FATOffset = br.ReadUInt32();
                _header.FATSize = br.ReadUInt32();

                _header.Arm9OverlayOffset = br.ReadUInt32();
                _header.Arm9OverlaySize = br.ReadUInt32();
                _header.Arm7OverlayOffset = br.ReadUInt32();
                _header.Arm7OverlaySize = br.ReadUInt32();

                _header.NormalRegisterSettings = br.ReadUInt32();
                _header.SecureRegisterSettings = br.ReadUInt32();

                _header.BannerOffset = br.ReadUInt32();

                _header.SecureAreaCRC = br.ReadUInt16();
                _header.SecureTransferTimeout = br.ReadUInt16();

                _header.Arm9AutoLoad = br.ReadUInt32();
                _header.Arm7AutoLoad = br.ReadUInt32();

                _header.SecureDisable = br.ReadUInt64();

                _header.ROMLength = br.ReadUInt32();
                _header.HeaderLength = br.ReadUInt32();
                br.BaseStream.Seek(56L, SeekOrigin.Current);

                _header.NintendoLogo = br.ReadBytes(156);
                _header.NintendoLogoCRC = br.ReadUInt16();
                _header.HeaderCRC = br.ReadUInt16();
            }

            // Load the banner
            using (BinaryReader br = new BinaryReader(File.OpenRead(Path.Combine(_directory, "banner.bin"))))
            {
                // Load its values
                _banner.Version = br.ReadUInt16();
                _banner.CRC16 = br.ReadUInt16();
                br.BaseStream.Seek(28L, SeekOrigin.Current);
                _banner.TileData = br.ReadBytes(512);
                _banner.Palette = br.ReadBytes(32);
                _banner.JapaneseTitle = Encoding.Unicode.GetString(br.ReadBytes(256));
                _banner.EnglishTitle = Encoding.Unicode.GetString(br.ReadBytes(256));
                _banner.FrenchTitle = Encoding.Unicode.GetString(br.ReadBytes(256));
                _banner.GermanTitle = Encoding.Unicode.GetString(br.ReadBytes(256));
                _banner.ItalianTitle = Encoding.Unicode.GetString(br.ReadBytes(256));
                _banner.SpanishTitle = Encoding.Unicode.GetString(br.ReadBytes(256));

                _banner.Image = new Bitmap(32, 32);

                // Now, draw it
                // Break up the tile data into nybbles
                byte[] tiles = new byte[_banner.TileData.Length * 2];
                for (int i = 0; i < _banner.TileData.Length; i++)
                {
                    tiles[i * 2] = (byte)(_banner.TileData[i] & 0xF);
                    tiles[i * 2 + 1] = (byte)((_banner.TileData[i] & 0xF0) >> 4);
                }

                // Convert the palette
                Color[] palette = new Color[_banner.Palette.Length / 2];
                for (int i = 0; i < palette.Length; i++)
                {
                    // Get ushort
                    ushort bgr = BitConverter.ToUInt16(_banner.Palette, i * 2);
                    int r, g, b;

                    // Get components
                    r = (bgr & 0x001F) * 0x08;
                    g = ((bgr & 0x03E0) >> 5) * 0x08;
                    b = ((bgr & 0x7C00) >> 10) * 0x08;

                    // Save
                    palette[i] = Color.FromArgb(r, g, b);
                }

                // Draw
                int k = 0;
                for (int hi = 0; hi < 4; hi++)
                {
                    for (int wi = 0; wi < 4; wi++)
                    {
                        for (int h = 0; h < 8; h++)
                        {
                            for (int w = 0; w < 8; w++)
                            {
                                _banner.Image.SetPixel(w + wi * 8, h + hi * 8, palette[tiles[k]]);
                                k++;
                            }
                        }
                    }
                }
            }
        }

        // File stuffs
        /// <summary>
        /// Gets the absolute path for a file path relative to the ROM.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetFullFilePath(string filePath)
        {
            return Path.Combine(_directory, filePath);
        }

        /// <summary>
        /// Opens a file in the extracted ROM for reading.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public FileStream FileOpenRead(string file)
        {
            string path = GetFullFilePath(file);
            if (!File.Exists(path)) throw new FileNotFoundException();

            return File.OpenRead(path);
        }

        /// <summary>
        /// Opens a file in the extracted ROM for writing.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public FileStream FileOpenWrite(string file)
        {
            string path = GetFullFilePath(file);
            if (!File.Exists(path)) throw new FileNotFoundException();
            // For now, check if it exists
            // In the future, I may comment this out to allow for file creation

            return File.OpenWrite(path);
        }

        /// <summary>
        /// Opens a NARC file in the extracted ROM.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public NDS.NARC FileOpenNARC(string file)
        {
            string path = GetFullFilePath(file);
            if (!File.Exists(path)) throw new FileNotFoundException();

            return new NDS.NARC(path);
        }

        #region Properties

        /// <summary>
        /// The path of the ROM source file.
        /// </summary>
        public string FilePath
        {
            get { return _file; }
        }

        /// <summary>
        /// The path to the root directory of the ROM.
        /// </summary>
        public string DirectoryPath
        {
            get { return _directory; }
        }

        /// <summary>
        /// The header of the ROM as read from header.bin.
        /// </summary>
        public rHeader Header
        {
            get { return _header; }
        }

        /// <summary>
        /// The banner of the ROM as read from banner.bin.
        /// </summary>
        public rBanner Banner
        {
            get { return _banner; }
        }

        #endregion

        /// <summary>
        /// Represents the header of an NDS ROM.
        /// </summary>
        public struct rHeader
        {
            // It follows this format exactly
            public string Title, Code, Maker;
            public byte DeviceCode, EncryptionSeed;
            public byte FileLength;
            // 9 bytes reserved (00)
            public byte Version, InternalFlags;
            public uint Arm9Offset, Arm9Entry, Arm9Load, Arm9Length;
            public uint Arm7Offset, Arm7Entry, Arm7Load, Arm7Length;
            public uint FNTOffset, FNTSize;
            public uint FATOffset, FATSize;
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

        /// <summary>
        /// Represents the banner of an NDS ROM.
        /// </summary>
        public struct rBanner
        {
            public ushort Version, CRC16;
            // 28 bytes reserved
            public byte[] TileData, Palette;
            public string JapaneseTitle, EnglishTitle, FrenchTitle;
            public string GermanTitle, ItalianTitle, SpanishTitle;

            public Bitmap Image;
        }
    }
}
