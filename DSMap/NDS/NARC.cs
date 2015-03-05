using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSMap.NDS
{
    public class NARC
    {
        // Magic stamps
        private const uint NARCMAGIC = 0x4352414E;
        private const uint FATMAGIC = 0x46415442;
        private const uint FNTMAGIC = 0x464E5442;
        private const uint FIMGMAGIC = 0x46494D47;

        // The data
        private string _path;
        private byte[][] _files;

        public NARC(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException();

            _path = file;
            Load(); // Just to make it simpler
        }

        private void Load()
        {
            // Load the individual files from the NARC into memory
            // I would rather use an external version, but it takes too long to extract
            using (BinaryReader br = new BinaryReader(File.OpenRead(_path)))
            {
                uint[] fileOffsets; int[] fileSizes;

                // Read header
                if (br.ReadUInt32() != NARCMAGIC)
                    throw new Exception("Bad NARC header!");
                ushort endian = br.ReadUInt16(); // fe ff - little endian
                ushort format = br.ReadUInt16(); // 00 10 - narc format
                uint narcSize = br.ReadUInt32();
                if (br.ReadUInt16() != 0x10) // header size
                    throw new Exception("Bad NARC header!");
                if (br.ReadUInt16() != 0x3) // subsection count
                    throw new Exception("Bad NARC header! Can only read NARCs with 3 subsections!");
                // This probably not the safest, but whatever

                // Read FAT
                if (br.ReadUInt32() != FATMAGIC)
                    throw new Exception("Bad FAT header!");
                uint fatSize = br.ReadUInt32();

                uint fileCount = br.ReadUInt32();
                fileOffsets = new uint[fileCount];
                fileSizes = new int[fileCount];
                for (int f = 0; f < fileCount; f++)
                {
                    uint start = br.ReadUInt32();
                    uint end = br.ReadUInt32();

                    fileOffsets[f] = start;
                    fileSizes[f] = (int)(end - start);
                }

                // Read FNT
                if (br.ReadUInt32() != FNTMAGIC)
                    throw new Exception("Bad FNT header!");
                uint fntSize = br.ReadUInt32();
                // I won't deal with named NARCs, so I skip this entire section.
                br.BaseStream.Seek(fntSize - 8, SeekOrigin.Current);

                // Read FIMG
                if (br.ReadUInt32() != FIMGMAGIC)
                    throw new Exception("Bad FIMG header!");
                br.BaseStream.Seek(4L, SeekOrigin.Current);
                uint fileDataOffset = (uint)br.BaseStream.Position;

                // Read each file into memory
                _files = new byte[fileCount][];
                for (int f = 0; f < fileCount; f++)
                {
                    br.BaseStream.Seek(fileDataOffset + fileOffsets[f], SeekOrigin.Begin);
                    _files[f] = br.ReadBytes(fileSizes[f]);
                }
            }
        }

        public void Save()
        {
            // Overwrite an existing NARC with our own
            using (BinaryWriter bw = new BinaryWriter(File.Create(_path)))
            {
                // Write header
                bw.Write(NARCMAGIC);
                bw.Write((byte)0xFE);
                bw.Write((byte)0xFF);
                bw.Write((byte)0x0);
                bw.Write((byte)0x1);
                bw.Write((uint)0x0); // file size, to be changed later
                bw.Write((ushort)0x10);
                bw.Write((ushort)0x3);

                // Write FAT
                bw.Write(FATMAGIC);
                bw.Write((uint)(12 + (_files.Length * 8))); // length: header + files * 8
                bw.Write((uint)_files.Length);

                uint currentOffset = 0; // will also be the size of the img section
                //uint imgSize = 0;
                for (int i = 0; i < _files.Length; i++)
                {
                    uint size = (uint)(_files[i].Length % 4 == 0 ? _files[i].Length : (_files[i].Length + (4 - (_files[i].Length % 4))));
                    //uint size = (uint)(files [i].Length + (4 - (files [i].Length % 4)));

                    bw.Write(currentOffset);
                    bw.Write((currentOffset + size));

                    currentOffset += size;
                    //imgSize += size;
                }

                // Write FNT
                // All files shall remain nameless
                // Therefore, write a blank FNT
                bw.Write(FNTMAGIC);
                bw.Write((uint)0x10);
                bw.Write((uint)0x4);
                bw.Write((ushort)0x0);
                bw.Write((ushort)0x1);

                // Write FIMG
                bw.Write(FIMGMAGIC);
                bw.Write(currentOffset); // Set during FAT part
                for (int i = 0; i < _files.Length; i++)
                {
                    bw.Write(_files[i]);

                    if (_files[i].Length % 4 != 0)
                    {
                        for (int x = 0; x < (4 - (_files[i].Length % 4)); x++)
                        {
                            bw.Write((byte)0xFF);
                        }
                    }

                    _files[i] = null;
                }

                // Write file size
                bw.BaseStream.Seek(8L, SeekOrigin.Begin);
                bw.Write((uint)bw.BaseStream.Length);
            }
        }

        #region Properties

        public byte[][] Files
        {
            get { return _files; }
        }

        public int FileCount
        {
            get { return _files.Length; }
        }

        #endregion

        public byte[] GetFile(int id)
        {
            if (id >= _files.Length) throw new IndexOutOfRangeException();

            return _files[id];
        }

        // Useful for BinaryReaders
        // We can't use this for replacement, because MemoryStreams cannot change size
        public MemoryStream GetFileMemoryStream(int id)
        {
            byte[] buffer = GetFile(id);
            return new MemoryStream(buffer);
        }

        public void ReplaceFile(int id, byte[] data)
        {
            if (id >= _files.Length) throw new IndexOutOfRangeException();

            _files[id] = data;
        }

        public void ReplaceFile(int id, string file)
        {
            if (id >= _files.Length) throw new IndexOutOfRangeException();

            _files[id] = File.ReadAllBytes(file);
        }

        public string GetFileInTempFile(int id)
        {
            if (id >= _files.Length) throw new IndexOutOfRangeException();

            return Temporary.CreateTemporaryFile(_files[id]);
        }
    }
}
