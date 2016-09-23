using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lost
{
    public class Header
    {
#if HEARTGOLD
        // TODO: HeartGold format is different
#else
        public byte MapTextures, ObjectTexutres;
        public ushort Matrix, Scripts, LevelScripts, Texts;
        public ushort MusicDay, MusicNight;
        public ushort WildPokemon;
        public ushort Events, Name;
        public byte NameFrame, Weather, Camera, NameStyle, Flags;
#endif

        public Header(string arm9, uint tableStart, int id)
        {
            // TODO: save arm9, tableStart, etc. with the header?
            using (BinaryReader br = new BinaryReader(File.OpenRead(arm9)))
            {
#if HEARTGOLD
                throw new NotSupportedException();
#else
                // Seek
                br.BaseStream.Seek(tableStart + id * 24, SeekOrigin.Begin);

                // And read
                // This format is unique to DPPt
                MapTextures = br.ReadByte();
                ObjectTexutres = br.ReadByte();

                Matrix = br.ReadUInt16();
                Scripts = br.ReadUInt16();
                LevelScripts = br.ReadUInt16();
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
#endif
            }
        }

        public void Save(string arm9, uint tableStart, int id)
        {
            using (BinaryWriter bs = new BinaryWriter(File.OpenWrite(arm9)))
            {
#if HEARTGOLD
                throw new NotSupportedException();
#else
                bs.BaseStream.Seek(tableStart + id * 24, SeekOrigin.Begin);

                bs.Write(MapTextures);
                bs.Write(ObjectTexutres);

                bs.Write(Matrix);
                bs.Write(Scripts);
                bs.Write(LevelScripts);
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
#endif
            }
        }

        public static string[] LoadHeaderNames(string file)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                // Calculate number of names
                string[] names = new string[br.BaseStream.Length / 16];

                // And read them all
                for (int i = 0; i < names.Length; i++)
                    names[i] = br.ReadString(16);

                return names;
            }
        }

        public static void SaveHeaderNames(string file, string[] names)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                for (int i = 0; i < names.Length; i++)
                {
                    var name = Encoding.UTF8.GetBytes(names[i]);
                    Array.Resize(ref name, 16);

                    bw.Write(name);
                }
            }
        }

#if !HEARTGOLD
        public static void MakeAllInteriors3D(string arm9, uint tableStart, int headerCount)
        {
            // Untested, may or may not work
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
#endif

        // Match a matrix to a map's header
        public static Dictionary<int, int> LoadHeaderMatrixMatches(string arm9, uint tableStart, int headerCount)
        {
            var matches = new Dictionary<int, int>();
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
