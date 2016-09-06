using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSHL.Formats.Pokémon
{
    public class WildPokémon
    {
        public int WalkingRate;
        public int[] WalkingSpecies;
        public int[] WalkingLevels; // 12 of them
        public int[] Morning, Day, Night; // 2 each
        public int[] Radar; // 4
        public int[] Unknown; // 6
        public int[] Ruby, Sapphire, Emerald, FireRed, LeafGreen; // 2 each

        public int SurfingRate;
        public byte[] SurfingMinLevels, SurfingMaxLevels;
        // ushort[] padding
        public int[] SurfingSpecies;

        public int OldRodRate;
        public byte[] OldRodMinLevels, OldRodMaxLevels;
        // ushort[] padding
        public int[] OldRodSpecies;

        public int GoodRodRate;
        public byte[] GoodRodMinLevels, GoodRodMaxLevels;
        // ushort[] padding
        public int[] GoodRodSpecies;

        public int SuperRodRate;
        public byte[] SuperRodMinLevels, SuperRodMaxLevels;
        // ushort[] padding
        public int[] SuperRodSpecies;

        public WildPokémon(MemoryStream file)
        {
            // Initialize a ton of arrays
            WalkingSpecies = new int[12];
            WalkingLevels = new int[12];
            Morning = new int[2];
            Day = new int[2];
            Night = new int[2];
            Radar = new int[4];
            Unknown = new int[6];
            Ruby = new int[2];
            Sapphire = new int[2];
            Emerald = new int[2];
            FireRed = new int[2];
            LeafGreen = new int[2];

            SurfingMinLevels = new byte[5];
            SurfingMaxLevels = new byte[5];
            SurfingSpecies = new int[5];

            OldRodMinLevels = new byte[5];
            OldRodMaxLevels = new byte[5];
            OldRodSpecies = new int[5];

            GoodRodMinLevels = new byte[5];
            GoodRodMaxLevels = new byte[5];
            GoodRodSpecies = new int[5];

            SuperRodMinLevels = new byte[5];
            SuperRodMaxLevels = new byte[5];
            SuperRodSpecies = new int[5];

            // Read the data
            using (BinaryReader br = new BinaryReader(file))
            {
                WalkingRate = br.ReadInt32();
                for (int i = 0; i < 12; i++)
                {
                    WalkingLevels[i] = br.ReadInt32();
                    WalkingSpecies[i] = br.ReadInt32();
                }

                Morning[0] = br.ReadInt32();
                Morning[1] = br.ReadInt32();
                Day[0] = br.ReadInt32();
                Day[1] = br.ReadInt32();
                Night[0] = br.ReadInt32();
                Night[1] = br.ReadInt32();

                for (int i = 0; i < 4; i++)
                {
                    Radar[i] = br.ReadInt32();
                }

                for (int i = 0; i < 6; i++)
                {
                    Unknown[i] = br.ReadInt32();
                }

                Ruby[0] = br.ReadInt32();
                Ruby[1] = br.ReadInt32();
                Sapphire[0] = br.ReadInt32();
                Sapphire[1] = br.ReadInt32();
                Emerald[0] = br.ReadInt32();
                Emerald[1] = br.ReadInt32();
                FireRed[0] = br.ReadInt32();
                FireRed[1] = br.ReadInt32();
                LeafGreen[0] = br.ReadInt32();
                LeafGreen[1] = br.ReadInt32();

                SurfingRate = br.ReadInt32();
                for (int i = 0; i < 5; i++)
                {
                    SurfingMaxLevels[i] = br.ReadByte();
                    SurfingMinLevels[i] = br.ReadByte();
                    br.BaseStream.Seek(2L, SeekOrigin.Current); // padding
                    SurfingSpecies[i] = br.ReadInt32();
                }

                OldRodRate = br.ReadInt32();
                for (int i = 0; i < 5; i++)
                {
                    OldRodMaxLevels[i] = br.ReadByte();
                    OldRodMinLevels[i] = br.ReadByte();
                    br.BaseStream.Seek(2L, SeekOrigin.Current); // padding
                    OldRodSpecies[i] = br.ReadInt32();
                }

                GoodRodRate = br.ReadInt32();
                for (int i = 0; i < 5; i++)
                {
                    GoodRodMaxLevels[i] = br.ReadByte();
                    GoodRodMinLevels[i] = br.ReadByte();
                    br.BaseStream.Seek(2L, SeekOrigin.Current); // padding
                    GoodRodSpecies[i] = br.ReadInt32();
                }

                SuperRodRate = br.ReadInt32();
                for (int i = 0; i < 5; i++)
                {
                    SuperRodMaxLevels[i] = br.ReadByte();
                    SuperRodMinLevels[i] = br.ReadByte();
                    br.BaseStream.Seek(2L, SeekOrigin.Current); // padding
                    SuperRodSpecies[i] = br.ReadInt32();
                }
            }
        }

        public byte[] Save()
        {
            // Write to a temp file
            string tempFile = Temporary.GetTemporaryFileName();
            using (BinaryWriter bw = new BinaryWriter(File.Create(tempFile)))
            {
                bw.Write(WalkingRate);
                for (int i = 0; i < 12; i++)
                {
                    bw.Write(WalkingLevels[i]);
                    bw.Write(WalkingSpecies[i]);
                }

                bw.Write(Morning[0]);
                bw.Write(Morning[1]);
                bw.Write(Day[0]);
                bw.Write(Day[1]);
                bw.Write(Night[0]);
                bw.Write(Night[1]);

                for (int i = 0; i < 4; i++)
                {
                    bw.Write(Radar[i]);
                }

                for (int i = 0; i < 6; i++)
                {
                    bw.Write(Unknown[i]);
                }

                bw.Write(Ruby[0]);
                bw.Write(Ruby[1]);
                bw.Write(Sapphire[0]);
                bw.Write(Sapphire[1]);
                bw.Write(Emerald[0]);
                bw.Write(Emerald[1]);
                bw.Write(FireRed[0]);
                bw.Write(FireRed[1]);
                bw.Write(LeafGreen[0]);
                bw.Write(LeafGreen[1]);

                bw.Write(SurfingRate);
                for (int i = 0; i < 5; i++)
                {
                    bw.Write(SurfingMaxLevels[i]);
                    bw.Write(SurfingMinLevels[i]);
                    bw.Write(ushort.MinValue); // 00 00
                    bw.Write(SurfingSpecies[i]);
                }

                bw.Write(OldRodRate);
                for (int i = 0; i < 5; i++)
                {
                    bw.Write(OldRodMaxLevels[i]);
                    bw.Write(OldRodMinLevels[i]);
                    bw.Write(ushort.MinValue);
                    bw.Write(OldRodSpecies[i]);
                }

                bw.Write(GoodRodRate);
                for (int i = 0; i < 5; i++)
                {
                    bw.Write(GoodRodMaxLevels[i]);
                    bw.Write(GoodRodMinLevels[i]);
                    bw.Write(ushort.MinValue);
                    bw.Write(GoodRodSpecies[i]);
                }

                bw.Write(SuperRodRate);
                for (int i = 0; i < 5; i++)
                {
                    bw.Write(SuperRodMaxLevels[i]);
                    bw.Write(SuperRodMinLevels[i]);
                    bw.Write(ushort.MinValue);
                    bw.Write(SuperRodSpecies[i]);
                }
            }

            // return & delete temp
            byte[] restult = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);
            return restult;
        }
    }
}
