using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace DSMap
{
    public static class Patching
    {
        public static unsafe void uCreatePatch(string originalFile, string modifiedFile, string patchFile)
        {
            // Read the original files
            byte[] originalData = File.ReadAllBytes(originalFile);
            byte[] modifiedData = File.ReadAllBytes(modifiedFile);

            // Let's go
            using (PatchWriter pw = new PatchWriter(patchFile))
            {
                // Write header
                pw.Write(Encoding.UTF8.GetBytes("DSPatch!")); // magic
                pw.Write((ushort)1); // format
                pw.Write(GetChecksum(originalData)); // checksum 1
                pw.Write(GetChecksum(modifiedData)); // checksum 2
                pw.Write(originalData.Length); // length 1
                pw.Write(modifiedData.Length); // length 2

                // Resize the original data to match the modified data in length
                if (modifiedData.Length != originalData.Length)
                {
                    Array.Resize<byte>(ref originalData, modifiedData.Length);
                }

                // Analyze & write the patch
                // Pointers are messy business
                int i = 0;
                int changeStart = -1;
                List<byte> change = new List<byte>();
                fixed (byte* ptrOrig = &originalData[0], ptrMod = &modifiedData[0])
                {
                    while (i < modifiedData.Length)
                    {
                        if (*(ptrOrig + i) != *(ptrMod + i))
                        {
                            if (changeStart == -1) // Start a block
                            {
                                changeStart = i;
                                change.Clear();
                            }

                            // Add to the block
                            // By using the ^ operator, this patch is reversible
                            change.Add((byte)(*(ptrOrig + i) ^ *(ptrMod + i)));
                        }
                        else if (changeStart != -1)
                        {
                            pw.WriteBlock(changeStart, change.ToArray());
                            changeStart = -1;
                        }

                        i++;
                    }
                }

                // Add the final block to the patch
                // This could be mammoth if there is an expanded ROM
                if (changeStart != -1)
                {
                    pw.WriteBlock(changeStart, change.ToArray());
                }
            }
        }

        public static void ApplyPatch(string patchFile, string toModifyFile)
        {
            byte[] fileData = File.ReadAllBytes(toModifyFile);
            byte checksum = GetChecksum(fileData);

            using (PatchReader pr = new PatchReader(patchFile))
            {
                if (Encoding.UTF8.GetString(pr.ReadBytes(8)) != "DSPatch!") throw new Exception("This is not a patch file!");
                if (pr.ReadUInt16() != 1) throw new Exception("Invalid patch version!");

                byte check1 = pr.ReadByte();
                byte check2 = pr.ReadByte();

                int len1 = pr.ReadInt32();
                int len2 = pr.ReadInt32();

                if (checksum == check1)
                {
                    if (fileData.Length != len1)
                    {
                        Array.Resize<byte>(ref fileData, len1);
                    }
                }
                else if (checksum == check2)
                {
                    if (fileData.Length != len2)
                    {
                        Array.Resize<byte>(ref fileData, len2);
                    }
                }
                else
                {
                    throw new Exception("This patch cannot be applied to this file!");
                }

                // Read the blocks
                while (!pr.EndOfStream)
                {
                    // Read the block
                    int offset = pr.ReadBlockOffset();
                    byte[] block = pr.ReadBlockData();

                    // Apply the block
                    for (int k = 0; k < block.Length; k++)
                    {
                        fileData[offset + k] = (byte)(fileData[offset + k] ^ block[k]);
                    }
                }

                // Save the modified file
                File.WriteAllBytes(toModifyFile, fileData);
            }
        }

        // A rather simple checksum method
        // It's the same used for Gen. 1's save files
        // Using something like CRC16/32 would be safer
        // But this will work for format 1
        private static byte GetChecksum(byte[] buffer)
        {
            byte sum = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                sum += buffer[i];
            }
            return (byte)(~sum);
        }
    }

    public class PatchWriter : BinaryWriter
    {
        public PatchWriter(string file) : base(File.Create(file)) { }

        public void WriteBlock(int offset, byte[] changes)
        {
            base.Write7BitEncodedInt(offset);
            base.Write7BitEncodedInt(changes.Length);
            base.Write(changes);
        }

        private bool IsTheSame(byte[] buffer)
        {
            byte b0 = buffer[0];
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] != b0) return false;
            }
            return true;
        }
    }

    public class PatchReader : BinaryReader
    {
        public PatchReader(string file) : base(File.OpenRead(file)) { }

        public int ReadBlockOffset()
        {
            return base.Read7BitEncodedInt();
        }

        public byte[] ReadBlockData()
        {
            int len = base.Read7BitEncodedInt();
            return base.ReadBytes(len);
        }

        public bool EndOfStream
        {
            get { return (base.BaseStream.Position >= base.BaseStream.Length); }
        }
    }
}
