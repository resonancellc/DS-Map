using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DSMap.NDS;

namespace DSMap.Formats
{
    public class Map
    {
        private Movement[,] _movements;
        private List<MapObject> _objects;
        private byte[] _rawModel;
        private byte[] _rawBDHC;

        public Map(MemoryStream file)
        {
            using (BinaryReader br = new BinaryReader(file))
            {
                // Header
                int movementSize = br.ReadInt32();
                int objectSize = br.ReadInt32();
                int modelSize = br.ReadInt32();
                int bdhcSize = br.ReadInt32();

                if (movementSize != 2048) throw new Exception("Bad movements size!");

                // Movements
                _movements = new Movement[32, 32];
                for (int y = 0; y < 32; y++)
                {
                    for (int x = 0; x < 32; x++)
                    {
                        _movements[x, y] = new Movement(); // I don't think this is needed.
                        _movements[x, y].Permission = br.ReadByte();
                        _movements[x, y].Flag = br.ReadByte();
                    }
                }

                // Objects
                // These are things like buildings
                int objectCount = objectSize / 48;
                _objects = new List<MapObject>();

                if (objectCount > 0)
                {
                    for (int i = 0; i < objectCount; i++)
                    {
                        MapObject obj = new MapObject();
                        obj.Number = br.ReadInt32();

                        obj.XFlag = br.ReadUInt16();
                        //short y = (short)(br.ReadByte() + (br.ReadByte() << 8));
                        short y = br.ReadInt16();
                        obj.X = y > 16 ? (short)(y - 0xFFEF) : (short)(y + 17);

                        obj.YFlag = br.ReadUInt16();
                        obj.Y = br.ReadInt16();

                        obj.ZFlag = br.ReadUInt16();
                        //short z = (short)(br.ReadByte() + (br.ReadByte() << 8));
                        short z = br.ReadInt16();
                        obj.Z = z > 16 ? (short)(z - 0xFFEF) : (short)(z + 17);

                        br.BaseStream.Seek(13L, SeekOrigin.Current);

                        obj.Width = br.ReadInt32();
                        obj.Height = br.ReadInt32();
                        obj.Length = br.ReadInt32();

                        br.BaseStream.Seek(7L, SeekOrigin.Current);
                    }
                }

                // Model (NSBMD model)
                _rawModel = br.ReadBytes(modelSize);

                // BDHC (not sure what this does yet)
                _rawBDHC = br.ReadBytes(bdhcSize);
            }
        }

        // TODO: Map files

        public static string[] LoadMapNames(NARC narc)
        {
            string[] names = new string[narc.FileCount];
            for (int i = 0; i < names.Length; i++)
            {
                using (MemoryStream ms = narc.GetFileMemoryStream(i))
                {
                    // Read part of header
                    uint moveSize = ReadUInt32(ms);
                    uint objSize = ReadUInt32(ms);

                    // Skip to the location of the model's name
                    ms.Seek(moveSize + objSize + 60, SeekOrigin.Current);

                    // Read
                    byte[] buffer = new byte[16];
                    ms.Read(buffer, 0, 16);

                    names[i] = "";
                    foreach (byte b in buffer)
                    {
                        if (b == 0) break;
                        else names[i] += (char)b;
                    }
                }
            }
            return names;
        }

        private static uint ReadUInt32(MemoryStream ms)
        {
            byte[] buffer = new byte[4];
            ms.Read(buffer, 0, 4);
            return (uint)((buffer[3] << 24) | (buffer[2] << 16)
                | (buffer[1] << 8) | buffer[0]);
        }

        public struct Movement
        {
            public byte Permission, Flag;
        }

        // Only classes allow for manipulation of properties outside initialization in a list
        public class MapObject
        {
            public int Number;

            public short X, Y, Z;
            public ushort XFlag, YFlag, ZFlag;

            public int Width, Height, Length;
        }
    }
}
