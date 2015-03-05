using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSMap.Formats
{
    public class Header
    {
        // I'm not bothering with properties for this
        // It would take too long to type :p
        public byte Textures, ObjectTexutres;
        public ushort Matrix, Scripts, Unknown1, Texts;
        public ushort MusicDay, MusicNight;
        public ushort WildPokemon;
        public ushort Events, Name;
        public byte NameFrame, Weather, Camera, NameStyle, Flags;

        public Header(string arm9, uint tableStart, int id)
        {
            // TODO: save arm9, tableStart, etc. with the header?
            using (BinaryReader br = new BinaryReader(File.OpenRead(arm9)))
            {
                // Seek
                br.BaseStream.Seek(tableStart + id * 24, SeekOrigin.Begin);

                // And read
                // This format is unique to DPPt
                Textures = br.ReadByte();
                ObjectTexutres = br.ReadByte();

                Matrix = br.ReadUInt16();
                Scripts = br.ReadUInt16();
                Unknown1 = br.ReadUInt16();
                Texts = br.ReadUInt16();
                MusicDay = br.ReadUInt16();
                MusicNight = br.ReadUInt16();
                WildPokemon = br.ReadUInt16();
                Events = br.ReadUInt16();

                Name = br.ReadUInt16();
                Weather = br.ReadByte();
                Camera = br.ReadByte();
                NameStyle = br.ReadByte();
                Flags = br.ReadByte();
            }
        }

        public void Save(string arm9, uint tableStart, int id)
        {
            using (BinaryWriter bs = new BinaryWriter(File.OpenWrite(arm9)))
            {
                bs.BaseStream.Seek(tableStart + id * 24, SeekOrigin.Begin);

                bs.Write(Textures);
                bs.Write(ObjectTexutres);

                bs.Write(Matrix);
                bs.Write(Scripts);
                bs.Write(Unknown1);
                bs.Write(Texts);
                bs.Write(MusicDay);
                bs.Write(MusicNight);
                bs.Write(WildPokemon);
                bs.Write(Events);
                bs.Write(Name);

                bs.Write(Weather);
                bs.Write(Camera);
                bs.Write(NameStyle);
                bs.Write(Flags);
            }
        }

        public static string[] LoadHeaderNames(string file, out int headerCount)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                // Calculate number of names
                string[] names = new string[br.BaseStream.Length / 16];
                headerCount = names.Length;

                // And read them all
                for (int i = 0; i < names.Length; i++)
                {
                    byte[] buffer = br.ReadBytes(16); // 16 byte name, extra characters 0'd out

                    names[i] = "";
                    foreach(byte b in buffer)
                    {
                        if (b == 0) break;
                        else names[i] += (char)b;
                    }
                }

                return names;
            }
        }

        public static void SaveHeaderNames(string file, string[] names)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                for (int i = 0; i < names.Length; i++)
                {
                    byte[] name = Encoding.UTF8.GetBytes(names[i]);
                    
                    bw.Write(name);
                    for (int j = name.Length; j < 16; j++)
                    {
                        bw.Write((byte)0);
                    }
                }
            }
        }

        public static void MakeAllInteriors3D(string arm9, uint tableStart, int headerCount)
        {
            // I haven't tested this yet
            // It may or may not work
            using (FileStream fs = File.Open(arm9, FileMode.Open, FileAccess.ReadWrite))
            {
                // Go through all headers
                for (int i = 0; i < headerCount; i++)
                {
                    // Goto current camera for this header
                    fs.Seek(tableStart + (i * 24) + 21, SeekOrigin.Begin);

                    // This will be tricky
                    int camera = fs.ReadByte();
                    if (camera == 4) // All interiors normally use 4 for the camera angle
                    {
                        fs.Seek(-1, SeekOrigin.Current);
                        fs.WriteByte(0); // Setting it to 0 makes it 3D
                    }
                }
            }
        }

        // Match a matrix to a map's header
        // Step 1 in matching headers to maps
        public static Dictionary<int, int> LoadHeaderMatrixMatches(string arm9, uint tableStart, int headerCount)
        {
            Dictionary<int, int> matches = new Dictionary<int, int>();
            using (BinaryReader br = new BinaryReader(File.OpenRead(arm9)))
            {
                br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                for (int i = 0; i < headerCount; i++)
                {
                    br.BaseStream.Seek(2L, SeekOrigin.Current);
                    int matrix = br.ReadUInt16();
                    br.BaseStream.Seek(20, SeekOrigin.Current);

                    matches[i] = matrix;
                }
            }
            return matches;
        }
    }
}
