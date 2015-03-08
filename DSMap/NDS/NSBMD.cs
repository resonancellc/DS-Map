using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DSMap.NDS
{
    public class NSBMDLoader
    {
        public const uint BMD0MAGIC = 0x30444D42;
        public const uint BMD0VERSION = 0x0002FEFF; // endian/version 2
        public const uint MDL0MAGIC = 0x304C444D;

        public static NSBMD LoadBMD0(string file)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                return LoadBMD0(br);
            }
        }

        public static NSBMD LoadBMD0(BinaryReader br)
        {
            // --------------------------------------------
            // BMD0 header
            // --------------------------------------------
            NSBMD bmd = new NSBMD();
            uint bmd0Offset = (uint)br.BaseStream.Position;

            if (br.ReadUInt32() != BMD0MAGIC)
                throw new Exception("This is not an NSBMD model!");
            if (br.ReadUInt32() != BMD0VERSION)
                throw new Exception("Invaild NSBMD version/format!");
            if (br.BaseStream.Length < br.ReadUInt32())
                throw new Exception("Invalid NSBMD file size!");
            if (br.ReadUInt16() != 0x10)
                throw new Exception("Invalid BMD0 header size!");

            ushort sectionCount = br.ReadUInt16();
            if (sectionCount != 1 && sectionCount != 2)
                throw new Exception("Invalid section number!!");

            uint mdl0Offest = br.ReadUInt32() + bmd0Offset;
            uint tex0Offset = (sectionCount == 2 ? br.ReadUInt32() + bmd0Offset : 0);

            // Read MDL0
            if (mdl0Offest != br.BaseStream.Position)
                throw new Exception("Unexpected error, baby.");
            
            // 
            bmd.MDL0 = LoadMDL0(br);

            if (sectionCount == 2)
            {
                MessageBox.Show("Ooh!\nThis model has textures included!");

                // Read TEX0 section (if it has one)
                br.BaseStream.Seek(tex0Offset, SeekOrigin.Begin);
                bmd.TEX0 = NSBTXLoader.LoadTEX0(br);

                bmd.HasTEX0 = true;
            }

            return bmd;
        }

        public static MDL0 LoadMDL0(BinaryReader br)
        {
            MDL0 mdl = new MDL0();
            uint mdl0Offset = (uint)br.BaseStream.Position;

            // --------------------------------------------
            // Load the header
            // --------------------------------------------
            if (br.ReadUInt32() != MDL0MAGIC)
                throw new Exception("Bad MDL0 magic stamp!");
            uint mdl0Size = br.ReadUInt32(); // who knows when I may need this?

            // --------------------------------------------
            // Load the 3D Info section
            // --------------------------------------------
            #region MDL 3D Info

            if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
            if (br.ReadByte() != 1)
                throw new Exception("Expected only one model!");
            ushort mdl0InfoSectionSize = br.ReadUInt16();

            if (br.ReadUInt16() != 8)
                throw new Exception("Bad unknown block size!");
            ushort unknownBlockSize = br.ReadUInt16();
            if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                throw new Exception("Bad unknown block constant!");

            mdl.UnknownBlock = new ushort[2];
            //for (int i = 0; i < objectCount; i++)
            //{
            //mdl.UnknownBlock[i] = new ushort[2];
            mdl.UnknownBlock[0] = br.ReadUInt16();
            mdl.UnknownBlock[1] = br.ReadUInt16();
            //}

            if (br.ReadUInt16() != 4) throw new Exception("Bad info block size!");
            ushort infoBlockSize = br.ReadUInt16();

            /*mdl.ModelOffsets = new uint[objectCount];
            for (int i = 0; i < objectCount; i++)
            {
                mdl.ModelOffsets[i] = br.ReadUInt32();
            }*/
            mdl.Offset = (uint)(br.ReadUInt32() + mdl0Offset);

            //mdl.Names = new string[objectCount];
            //for (int i = 0; i < objectCount; i++)
            //{
            mdl.Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
            //}

            #endregion

            if (br.BaseStream.Position != mdl.Offset)
                throw new Exception("Bad MDL data offset!");

            // --------------------------------------------
            // Load the MDL data header
            // --------------------------------------------
            mdl.BlockSize = br.ReadUInt32();
            mdl.BonesOffset = br.ReadUInt32() + mdl0Offset;
            mdl.MaterialsOffset = br.ReadUInt32() + mdl0Offset;
            mdl.PolygonStartOffset = br.ReadUInt32() + mdl0Offset;
            mdl.PolygonEndOffset = br.ReadUInt32() + mdl0Offset;
            mdl.Unknown = br.ReadBytes(3);
            mdl.ObjectCount = br.ReadByte();
            mdl.MaterialCount = br.ReadByte();
            mdl.PolygonCount = br.ReadByte();
            mdl.Unknown2 = br.ReadByte();
            mdl.ScaleMode = br.ReadByte();
            mdl.Unknown3 = br.ReadUInt64();
            mdl.VertexCount = br.ReadUInt16();
            mdl.SurfaceCount = br.ReadUInt16();
            mdl.TriangleCount = br.ReadUInt16();
            mdl.QuadCount = br.ReadUInt16();
            mdl.BoundingX = GetSignedFixedPoint(br.ReadUInt16());
            mdl.BoundingY = GetSignedFixedPoint(br.ReadUInt16());
            mdl.BoundingZ = GetSignedFixedPoint(br.ReadUInt16());
            mdl.BoundingWidth = GetSignedFixedPoint(br.ReadUInt16());
            mdl.BoundingHeight = GetSignedFixedPoint(br.ReadUInt16());
            mdl.BoundingDepth = GetSignedFixedPoint(br.ReadUInt16());
            mdl.RuntimeData = br.ReadUInt64(); // unused, but necessary..?

            // --------------------------------------------
            // Load the objects section
            // --------------------------------------------
            #region
            {
                // ~:~
                uint objInfoOffset = (uint)br.BaseStream.Position;

                // Header
                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte objectCount = br.ReadByte();
                ushort sectionSize = br.ReadUInt16();

                if (objectCount != mdl.ObjectCount)
                    throw new Exception("objectCount doesn't match mdl.ObjectCount!\n" + objectCount + " vs. " + mdl.ObjectCount + "\n 0x" + br.BaseStream.Position.ToString("X"));

                // The unknown block
                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort objUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                // It doesn't have any effect on displaying?
                mdl.Objects = new MDL0.Obj[objectCount];
                for (int i = 0; i < objectCount; i++)
                {
                    mdl.Objects[i].UnknownBlock = new ushort[2];
                    mdl.Objects[i].UnknownBlock[0] = br.ReadUInt16();
                    mdl.Objects[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Info data
                if (br.ReadUInt16() != 4) throw new Exception("Bad object info block size!");
                ushort objInfoBlockDataSize = br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < objectCount; i++)
                {
                    mdl.Objects[i].Offset = br.ReadUInt32() + objInfoOffset;
                }

                // Names
                for (int i = 0; i < objectCount; i++)
                {
                    mdl.Objects[i].Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }
            }
            #endregion

            // --------------------------------------------
            // Load the objects data section
            // --------------------------------------------
            #region
            {
                for (int i = 0; i < mdl.Objects.Length; i++)
                {
                    if (br.BaseStream.Position != mdl.Objects[i].Offset)
                        throw new Exception("Uh-oh!\nBad object" + i + " offset!");
                    MessageBox.Show("Object Data @ 0x" + br.BaseStream.Position.ToString("X"));

                    ushort transFlag = br.ReadUInt16();
                    br.BaseStream.Seek(2, SeekOrigin.Current); // padding

                    // trans flag analysis
                    mdl.Objects[i].T = (byte)(transFlag & 1);
                    mdl.Objects[i].R = (byte)((transFlag >> 1) & 1);
                    mdl.Objects[i].S = (byte)((transFlag >> 2) & 1);
                    mdl.Objects[i].P = (byte)((transFlag >> 3) & 1);
                    mdl.Objects[i].N = (byte)((transFlag >> 4) & 0xF);

                    // ...
                    if (mdl.Objects[i].T == 0)
                    {
                        mdl.Objects[i].XValue = br.ReadUInt32();
                        mdl.Objects[i].YValue = br.ReadUInt32();
                        mdl.Objects[i].ZValue = br.ReadUInt32();
                    }

                    if (mdl.Objects[i].P == 1)
                    {
                        mdl.Objects[i].Value1 = br.ReadUInt16();
                        mdl.Objects[i].Value2 = br.ReadUInt16();
                    }

                    if (mdl.Objects[i].S == 0)
                    {
                        mdl.Objects[i].XScale = br.ReadUInt32();
                        mdl.Objects[i].YScale = br.ReadUInt32();
                        mdl.Objects[i].ZScale = br.ReadUInt32();
                    }

                    if (mdl.Objects[i].P == 0 && mdl.Objects[i].R == 0)
                    {
                        mdl.Objects[i].Rotation = new ushort[8];
                        for (int n = 0; n < 8; n++)
                        {
                            mdl.Objects[i].Rotation[n] = br.ReadUInt16();
                        }
                    }
                }
            }
            #endregion

            // --------------------------------------------
            // Load the bones commands
            // --------------------------------------------
            {
                if (br.BaseStream.Position != mdl.BonesOffset)
                    throw new Exception("Bad bones offset?\nExpected 0x" +
                        mdl.BonesOffset.ToString("X") + " but got 0x" + br.BaseStream.Position.ToString("X"));
            }

            return mdl;
        }

        public static double GetSignedFixedPoint(ushort value)
        {
            double point = ((value >> 12) & 7);

            point += (double)(value & 0xFFF) / 0x1000;

            // sign
            if ((value >> 15) == 1)
                point = -point;

            return point;
        }
    }

    public struct NSBMD
    {
        public MDL0 MDL0; 

        public bool HasTEX0;
        public NSBTX TEX0;
    }

    public struct MDL0
    {
        public ushort[] UnknownBlock;
        //public uint ModelOffset;
        public uint Offset;
        public string Name;

        // header
        public uint BlockSize;
        public uint BonesOffset;
        public uint MaterialsOffset;
        public uint PolygonStartOffset;
        public uint PolygonEndOffset;
        public byte[] Unknown;
        public byte ObjectCount, MaterialCount, PolygonCount;
        public byte Unknown2;
        public byte ScaleMode;
        public ulong Unknown3;
        public ushort VertexCount, SurfaceCount, TriangleCount, QuadCount;
        public double BoundingX, BoundingY, BoundingZ;
        public double BoundingWidth, BoundingHeight, BoundingDepth;
        public ulong RuntimeData;

        public Obj[] Objects;

        public struct Obj
        {
            // 3D Info
            public ushort[] UnknownBlock;
            public uint Offset;
            public string Name;

            // Data
            //public ushort TransFlag;
            public byte N, P, S, R, T;

            // If T=0
            public uint XValue, YValue, ZValue;

            // If P=1
            public ushort Value1, Value2;

            // If S=0
            public uint XScale, YScale, ZScale;

            // If P=0 && R=0
            /*public ushort rot1, rot2;
            public ushort rot3;
            public ushort rot4;
            public ushort rot5;
            public ushort rot6;
            public ushort rot7;
            public ushort rot8;*/
            public ushort[] Rotation;
        }
    }
}
