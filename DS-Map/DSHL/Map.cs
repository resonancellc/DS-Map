using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost
{
    public class Map
    {
        public Movement[,] Movements { get; }  = new Movement[32, 32];
        public List<Building> Buildings { get; } = new List<Building>();

        public Map(Stream stream)
        {
            using (var br = new BinaryReader(stream))
            {
                var movementSize = br.ReadInt32();
                var buildingsSize = br.ReadInt32();
                var modelSize = br.ReadInt32();
                var terrainSize = br.ReadInt32();
#if HEARTGOLD
                var soundSize = br.ReadInt32();
#endif

                // TODO: check for invalid sizes?
                //       should always be 32x32 = 2048 bytes

                // movement section
                for (int y = 0; y < 32; y++)
                {
                    for (int x = 0; x < 32; x++)
                    {
                        Movements[x, y] = new Movement();
                        Movements[x, y].Behavior = br.ReadByte();
                        Movements[x, y].Flag = br.ReadByte();
                    }
                }

                // building section
#if HEARTGOLD
                br.BaseStream.Position = 0x14 + movementSize;
#else
                br.BaseStream.Position = 0x10 + movementSize;
#endif
                for (int i = 0; i < buildingsSize / 0x30; i++)
                {
                    var building = new Building();

                    building.Number = br.ReadInt32();
                    building.XFlag = br.ReadUInt16();
                    building.X = br.ReadUInt16();
                    building.YFlag = br.ReadUInt16();
                    building.Y = br.ReadUInt16();
                    building.ZFlag = br.ReadUInt16();
                    building.Z = br.ReadUInt16();
                    br.BaseStream.Position += 13L;      // filler
                    building.Width = br.ReadUInt16();
                    br.BaseStream.Position += 2L;
                    building.Height = br.ReadUInt16();
                    br.BaseStream.Position += 2L;
                    building.Length = br.ReadUInt16();
                    br.BaseStream.Position += 9L;

                    Buildings.Add(building);
                }

                // model section
                // TODO

                // terrain section
                // http://www.pokecommunity.com/showthread.php?t=371052
                // BDHC
                // pointCount
                // qCount
                // heightCount
                // rectangleCount
                // transitionCount
                // connectionCount
                // 
                // points:
                //  0000 XXXX 0000 YYYY     (x, y) point on the map
                // q:
                //  12 * qCount bytes       ????
                // heights:
                //  TTTT HHHH               height_in_tiles, partial_tile_height
                // rectangles:
                //  LLLL RRRR 0000 HHHH     left_point, right_point, height indexes above sections
                // transitions:
                //  0000 YYYY NNNN UUUU     y_position, connection_count, connection_start
                // connections:
                //  RRRR                    rectangle_index
#if HEARTGOLD
                br.BaseStream.Position = 0x14 + movementSize + buildingsSize + modelSize;
#else
                br.BaseStream.Position = 0x10 + movementSize + buildingsSize + modelSize;
#endif
                if (terrainSize > 0) {
                    if (br.ReadUInt32() != 0x43484442)
                        Console.WriteLine("bad BDHC"); // TODO exception bad map

                    var pointCount = br.ReadUInt16();
                    var qCount = br.ReadUInt16();
                    var heightCount = br.ReadUInt16();
                    var plateCount = br.ReadUInt16();
                    var transitionCount = br.ReadUInt16();
                    var connectionCount = br.ReadUInt16();

                    // points
                    var points = new Point[pointCount];
                    for (int i = 0; i < pointCount; i++)
                    {
                        br.BaseStream.Position += 2L;
                        points[i].X = br.ReadInt16();
                        br.BaseStream.Position += 2L;
                        points[i].Y = br.ReadInt16();
                    }

                    // q
                    br.BaseStream.Position += qCount * 12L;

                    // heights
                    var heights = new Height[heightCount];
                    for (int i = 0; i < heightCount; i++)
                    {
                        heights[i] = new Height()
                        {
                            Tiles = br.ReadInt16(),
                            Parts = br.ReadInt16(),
                        };
                    }

                    // plates
                    var plates = new List<Plate>();
                    for (int i = 0; i < plateCount; i++)
                    {
                        var topLeft = points[br.ReadUInt16()];
                        var bottomRight = points[br.ReadUInt16()];
                        br.BaseStream.Position += 2L;
                        var height = heights[br.ReadUInt16()];

                        plates.Add(new Plate()
                        {
                            Left = (short)topLeft.X,
                            Top = (short)topLeft.Y,
                            Right = (short)bottomRight.X,
                            Bottom = (short)bottomRight.Y,
                            Height = height,
                        });
                    }

                    // transitions
                    var transitions = new List<Transition>();
                    var connections = new Connection[transitionCount];

                    for (int i = 0; i < transitionCount; i++)
                    {
                        transitions.Add(new Transition()
                        {
                            Y = br.ReadInt16(),
                        });

                        connections[i].Count = br.ReadUInt16();
                        connections[i].Index = br.ReadUInt16();
                    }

                    // connections
                    var connectionData = new ushort[connectionCount];
                    for (int i = 0; i < connectionCount; i++)
                    {
                        connectionData[i] = br.ReadUInt16();
                    }

                    // link connections to transitions
                    for (int i = 0; i < transitionCount; i++)
                    {
                        for (int j = 0; j < connections[i].Count; j++)
                            transitions[i].ConnectedPlates.Add(connectionData[connections[i].Index + j]);
                    }
                }

                // sound section
#if HEARTGOLD
                br.BaseStream.Position = 0x14 + movementSize + buildingsSize + modelSize + terrainSize;
                // TODO
#endif
            }
        }

        public class Movement
        {
            public byte Behavior { get; set; }
            public byte Flag { get; set; }
        }

        public class Building
        {
            public int Number { get; set; }

            public ushort XFlag { get; set; }
            public ushort YFlag { get; set; }
            public ushort ZFlag { get; set; }

            public ushort X { get; set; }
            public ushort Y { get; set; }
            public ushort Z { get; set; }

            public ushort Width { get; set; }
            public ushort Height { get; set; }
            public ushort Length { get; set; }
        }

        public class Plate // would prefer Rectangle but oh well
        {
            public short Left { get; set; }
            public short Top { get; set; }
            public short Right { get; set; }
            public short Bottom { get; set; }

            public Height Height { get; set; }
        }

        public class Height
        {
            public short Tiles { get; set; }
            public short Parts { get; set; }
        }

        public class Transition
        {
            public short Y { get; set; }
            public List<ushort> ConnectedPlates { get; } = new List<ushort>();
        }

        struct Connection
        {
            public ushort Count;
            public ushort Index;
        }
    }
}
