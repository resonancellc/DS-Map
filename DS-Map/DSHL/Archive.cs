using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lost
{
    public class Archive
    {
        const uint NARC_MAGIC = 0x4352414Eu; // Nitro ARChive
        const uint FATB_MAGIC = 0x46415442u; // File Allocation TaBle
        const uint FNTB_MAGIC = 0x464E5442u; // File Name TaBle
        const uint FIMG_MAGIC = 0x46494D47u; // File IMaGe

        const uint NARC_FORMAT = 0x0100FFFEu; // Version 1, Little-Endian
        const ushort NARC_SIZE = 16;
        const ushort NARC_SECTIONS = 3;

        string path;
        int allocationTableOffset;
        int imageStartOffset;

        int[] fileOffsets, fileSizes;
        int fileCount;

        /// <summary>
        /// Create an Archive from a given NARC file.
        /// </summary>
        /// <param name="file">The NARC file to load.</param>
        public Archive(string file)
        {
            path = file;

            using (var fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var br = new BinaryReader(fs))
            {
                // NARC
                if (br.ReadUInt32() != NARC_MAGIC) throw new Exception("Invalid NARC header!");
                if (br.ReadUInt32() != NARC_FORMAT) throw new Exception("Invalid NARC format!");
                if (br.ReadUInt32() != br.BaseStream.Length) throw new Exception("Invalid NARC size!");
                if (br.ReadUInt16() != NARC_SIZE) throw new Exception("Invalid NARC header size! Expected 16.");
                if (br.ReadUInt16() != NARC_SECTIONS) throw new Exception("Invalid number of NARC subsections! Expected 3.");

                // FAT
                if (br.ReadUInt32() != FATB_MAGIC) throw new Exception("Expected FATB header!");
                br.BaseStream.Seek(4L, SeekOrigin.Current);

                fileCount = br.ReadInt32();
                fileOffsets = new int[fileCount];
                fileSizes = new int[fileCount];

                allocationTableOffset = (int)br.BaseStream.Position;
                for (int i = 0; i < fileCount; i++)
                {
                    var fileStart = br.ReadInt32();
                    var fileEnd = br.ReadInt32();

                    if (fileEnd - fileStart < 0) throw new Exception($"File {i} has an invalid size!");

                    fileOffsets[i] = fileStart;
                    fileSizes[i] = fileEnd - fileStart;
                }

                // FNT
                // Always empty as far as I know
                if (br.ReadUInt32() != FNTB_MAGIC) throw new Exception("Expected FNTB header!");
                var fntbSize = br.ReadUInt32();

                br.BaseStream.Seek(fntbSize - 8u, SeekOrigin.Current);

                // FIMG
                if (br.ReadUInt32() != FIMG_MAGIC) throw new Exception("Expected FIMG header!");

                imageStartOffset = (int)br.BaseStream.Position + 4;

                /*
                var firstFileOffset = br.BaseStream.Position + 4L;
                for (int i = 0; i < fileCount; i++)
                {
                    br.BaseStream.Seek(firstFileOffset + fileOffsets[i], SeekOrigin.Begin);
                    files.Add(br.ReadBytes(fileSizes[i]));
                }
                */
            }
        }

        /// <summary>
        /// Create a new Archive with the contents of a directory.
        /// </summary>
        /// <param name="file">The file to write to.</param>
        /// <param name="directory">The directory to take files from.</param>
        public static void Create(string file, string directory)
        {
            // Get all files to add
            //var directoryInfo = new DirectoryInfo(directory);
            var files = new List<byte[]>();
            foreach (var filepath in Directory.EnumerateFiles(directory))
            {
                files.Add(File.ReadAllBytes(filepath));
            }

            // Create new archive
            using (var fs = File.Create(file))
            using (var bw = new BinaryWriter(fs))
            {
                // NARC
                bw.Write(NARC_MAGIC);
                bw.Write(NARC_FORMAT);
                bw.Write(0u);
                bw.Write(NARC_SIZE);
                bw.Write(NARC_SECTIONS);

                // FATB
                bw.Write(FATB_MAGIC);
                bw.Write((12 + (files.Count * 8)));
                bw.Write(files.Count);

                var fileOffset = 0;
                for (int i = 0; i < files.Count; i++)
                {
                    var length = files[i].Length;
                    if (length % 4 != 0) length += (4 - length % 4);

                    bw.Write(fileOffset);
                    bw.Write(fileOffset + length);

                    fileOffset += length;
                }

                // FNTB
                bw.Write(FNTB_MAGIC);
                bw.Write(16u);
                bw.Write(4u);
                bw.Write(0x01000000u);

                // FIMG
                bw.Write(FIMG_MAGIC);
                bw.Write(fileOffset);

                foreach (var buffer in files)
                {
                    bw.Write(buffer);

                    if (buffer.Length % 4 != 0)
                        for (int i = 0; i < (4 - buffer.Length % 4); i++)
                        {
                            bw.Write(byte.MaxValue);
                        }
                }


                // Adjust file size in header
                bw.BaseStream.Position = 8L;
                bw.Write((uint)bw.BaseStream.Length);
            }

            files.Clear();
            files = null;
        }

        /// <summary>
        /// Extract the contents of this Archive to a directory.
        /// </summary>
        /// <param name="directory">The directory to extract to.</param>
        public void Extract(string directory)
        {
            if (Directory.Exists(directory)) Directory.Delete(directory, true);
            Directory.CreateDirectory(directory);

            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                for (int i = 0; i < fileCount; i++)
                {
                    // Read file
                    var buffer = new byte[fileSizes[i]];

                    fs.Seek(fileOffsets[i] + imageStartOffset, SeekOrigin.Begin);
                    fs.Read(buffer, 0, buffer.Length);

                    // Write to file
                    File.WriteAllBytes(System.IO.Path.Combine(directory, $"{i:D4}"), buffer);
                }
            }
        }

        /// <summary>
        /// Returns the given file in the Archive.
        /// </summary>
        /// <param name="index">The index of the file get.</param>
        /// <returns></returns>
        public byte[] GetFile(int index)
        {
            var offset = fileOffsets[index] + imageStartOffset;
            var size = fileSizes[index];
            var buffer = new byte[size];

            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buffer, 0, size);
            }

            return buffer;
        }

        /// <summary>
        /// Returns the given file in the Archive as a Stream.
        /// </summary>
        /// <param name="index">The index of the file to get.</param>
        /// <returns></returns>
        public MemoryStream GetFileStream(int index)
        {
            return new MemoryStream(GetFile(index));
        }

        /// <summary>
        /// Returns the first four bytes of the given file as a String. These are magic IDs in standard file types.
        /// </summary>
        /// <param name="index">The index of the file.</param>
        /// <returns></returns>
        public string GetFileType(int index)
        {
            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[4];

                fs.Seek(fileOffsets[index] + imageStartOffset, SeekOrigin.Begin);
                fs.Read(buffer, 0, 4);

                return Encoding.UTF8.GetString(buffer);
            }
        }

        /// <summary>
        /// Replaces the given file in the Archive.
        /// </summary>
        /// <param name="index">The index of the file to replace.</param>
        /// <param name="buffer">The data to replace.</param>
        public void ReplaceFile(int index, byte[] buffer)
        {
            using (var fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                if (buffer.Length <= fileSizes[index])
                {
                    //if (buffer.Length < fileSizes[index])
                    //    Array.Resize(ref buffer, fileSizes[index]);

                    // Simple overwrite of file data
                    fs.Seek(fileOffsets[index] + imageStartOffset, SeekOrigin.Begin);
                    fs.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    // To insert a larger file:
                    // TODO: Add padding to files not divisble by 4?

                    // Load all files beyond index
                    var endOfFileData = fileOffsets.Last() + fileSizes.Last();
                    var startOfNextFile = fileOffsets.Last();
                    if (index + 1 < fileCount)
                        startOfNextFile = fileOffsets[index + 1];

                    var beyond = new byte[endOfFileData - startOfNextFile];
                    fs.Seek(startOfNextFile + imageStartOffset, SeekOrigin.Begin);
                    fs.Read(beyond, 0, endOfFileData - startOfNextFile);

                    // Repalce file at index
                    fs.Seek(fileOffsets[index] + imageStartOffset, SeekOrigin.Begin);
                    fs.Write(buffer, 0, buffer.Length);

                    // Place beyond files
                    fs.Write(beyond, 0, beyond.Length);

                    // Adjust allocation table
                    var difference = buffer.Length - fileSizes[index];

                    fileSizes[index] = buffer.Length;
                    for (int i = index + 1; i < fileCount; i++)
                    {
                        fileOffsets[i] += difference;
                    }

                    // Overwrite entire allocation table starting with index
                    fs.Seek(allocationTableOffset + (index * 8), SeekOrigin.Begin);
                    for (int i = index; i < fileCount; i++)
                    {
                        fs.WriteInt32(fileOffsets[i]);
                        fs.WriteInt32(fileOffsets[i] + fileSizes[i]);
                    }

                    // Adjust file size in header
                    fs.Seek(8L, SeekOrigin.Begin);
                    fs.WriteInt32((int)fs.Length);
                }
            }
        }

        /// <summary>
        /// Replaces the given file in the Archive.
        /// </summary>
        /// <param name="index">The index of the file to replace.</param>
        /// <param name="stream">A stream with the data to replace.</param>
        public void ReplaceFile(int index, MemoryStream stream)
        {
            ReplaceFile(index, stream.ToArray());
        }

        public int FileCount
        {
            get { return fileCount; }
        }

        public int[] FileSizes
        {
            get { return fileSizes; }
        }

        public int[] FileOffsets
        {
            get { return fileOffsets; }
        }

        public int ImageOffset
        {
            get { return imageStartOffset; }
        }

        public string Path
        {
            get { return path; }
        }
    }
}
