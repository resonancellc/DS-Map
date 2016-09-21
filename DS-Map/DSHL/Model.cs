using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost
{
    public class Model
    {
        public const uint BMD0_MAGIC = 0x30444D42u;
        public const uint BMD0_VERSION = 0x0002FEFFu; // endian/version 2
        public const uint MDL0_MAGIC = 0x304C444Du;

        public Model(Stream stream)
        {
            using (var br = new BinaryReader(stream, Encoding.UTF8, true))
                LoadBMD0(br);
        }

        public Model(BinaryReader br)
        {
            LoadBMD0(br);
        }

        void LoadBMD0(BinaryReader br)
        {
            // --------------------------------------------
            // BMD0 section
            var startOffset = (uint)br.BaseStream.Position;
            if (br.ReadUInt32() != BMD0_MAGIC)
                throw new Exception();
            if (br.ReadUInt32() != BMD0_VERSION)
                throw new Exception();
            if (br.BaseStream.Length < br.ReadInt32())
                throw new Exception();
            if (br.ReadUInt16() != 0x10)
                throw new Exception();

            var sectionCount = br.ReadUInt16();
            if (sectionCount != 1 && sectionCount != 2)
                throw new Exception();

            var modelOffset = br.ReadUInt32();
            var textureOffset = 0u;
            if (sectionCount == 2)
                textureOffset = br.ReadUInt32();

            LoadMDL0(br);

            if (sectionCount == 2)
                ;
        }

        void LoadMDL0(BinaryReader br)
        {
            uint mdl0Offset = (uint)br.BaseStream.Position;

            // --------------------------------------------
            // Load the header
            // --------------------------------------------
            if (br.ReadUInt32() != MDL0_MAGIC)
                throw new Exception("Bad MDL0 magic stamp!");
            /*uint mdl0Size = */
            br.ReadUInt32();

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

            //mdl.UnknownBlock = new ushort[2];
            //for (int i = 0; i < objectCount; i++)
            //{
            //mdl.UnknownBlock[i] = new ushort[2];
            UnknownBlock[0] = br.ReadUInt16();
            UnknownBlock[1] = br.ReadUInt16();
            //}

            if (br.ReadUInt16() != 4) throw new Exception("Bad info block size!");
            ushort infoBlockSize = br.ReadUInt16();

            /*mdl.ModelOffsets = new uint[objectCount];
            for (int i = 0; i < objectCount; i++)
            {
                mdl.ModelOffsets[i] = br.ReadUInt32();
            }*/
            Offset = (uint)(br.ReadUInt32() + mdl0Offset);

            //mdl.Names = new string[objectCount];
            //for (int i = 0; i < objectCount; i++)
            //{
            Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
            //}

            #endregion

            if (br.BaseStream.Position != Offset)
                throw new Exception("Bad MDL data offset!");

            // --------------------------------------------
            // Load the MDL data header
            // --------------------------------------------
            #region
            BlockSize = br.ReadUInt32();
            BonesOffset = br.ReadUInt32() + Offset;
            MaterialsOffset = br.ReadUInt32() + Offset;
            PolygonStartOffset = br.ReadUInt32() + Offset;
            PolygonEndOffset = br.ReadUInt32() + Offset;
            Unknown = br.ReadBytes(3);
            ObjectCount = br.ReadByte();
            MaterialCount = br.ReadByte();
            PolygonCount = br.ReadByte();
            Unknown2 = br.ReadByte();
            ScaleMode = br.ReadByte();
            Unknown3 = br.ReadUInt64();
            VertexCount = br.ReadUInt16();
            SurfaceCount = br.ReadUInt16();
            TriangleCount = br.ReadUInt16();
            QuadCount = br.ReadUInt16();
            BoundingX = br.ReadSFixed16(); //GetSignedFixedPoint(br.ReadUInt16());
            BoundingY = br.ReadSFixed16();
            BoundingZ = br.ReadSFixed16();
            BoundingWidth = br.ReadSFixed16();
            BoundingHeight = br.ReadSFixed16();
            BoundingDepth = br.ReadSFixed16();
            RuntimeData = br.ReadUInt64(); // unused
            #endregion

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

                if (objectCount != ObjectCount)
                    throw new Exception($"objectCount doesn't match ObjectCount!\n{objectCount} vs. {ObjectCount}\n0x{br.BaseStream.Position:X6}");

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort objUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                // It doesn't have any effect on displaying?
                Objects = new Obj[objectCount];
                for (int i = 0; i < objectCount; i++)
                {
                    Objects[i].UnknownBlock = new ushort[2];
                    Objects[i].UnknownBlock[0] = br.ReadUInt16();
                    Objects[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Info data
                if (br.ReadUInt16() != 4) throw new Exception("Bad object info block size!");
                ushort objInfoBlockDataSize = br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < objectCount; i++)
                {
                    Objects[i].Offset = br.ReadUInt32() + objInfoOffset;
                }

                // Names
                for (int i = 0; i < objectCount; i++)
                {
                    Objects[i].Name = br.ReadString(16); //Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }
            }
            #endregion

            // --------------------------------------------
            // Load the objects data section
            // --------------------------------------------
            #region
            {
                for (int i = 0; i < Objects.Length; i++)
                {
                    if (br.BaseStream.Position != Objects[i].Offset)
                        throw new Exception("Uh-oh!\nBad object" + i + " offset!");
                    //MessageBox.Show("Object Data @ 0x" + br.BaseStream.Position.ToString("X"));

                    ushort transFlag = br.ReadUInt16();
                    br.BaseStream.Seek(2, SeekOrigin.Current); // padding

                    // trans flag analysis
                    Objects[i].T = (byte)(transFlag & 1);
                    Objects[i].R = (byte)((transFlag >> 1) & 1);
                    Objects[i].S = (byte)((transFlag >> 2) & 1);
                    Objects[i].P = (byte)((transFlag >> 3) & 1);
                    Objects[i].N = (byte)((transFlag >> 4) & 0xF);

                    // ...
                    if (Objects[i].T == 0)
                    {
                        Objects[i].XValue = br.ReadUInt32();
                        Objects[i].YValue = br.ReadUInt32();
                        Objects[i].ZValue = br.ReadUInt32();
                    }

                    if (Objects[i].P == 1)
                    {
                        Objects[i].Value1 = br.ReadUInt16();
                        Objects[i].Value2 = br.ReadUInt16();
                    }

                    if (Objects[i].S == 0)
                    {
                        Objects[i].XScale = br.ReadUInt32();
                        Objects[i].YScale = br.ReadUInt32();
                        Objects[i].ZScale = br.ReadUInt32();
                    }

                    if (Objects[i].P == 0 && Objects[i].R == 0)
                    {
                        Objects[i].Rotation = new ushort[8];
                        for (int n = 0; n < 8; n++)
                        {
                            Objects[i].Rotation[n] = br.ReadUInt16();
                        }
                    }
                }
            }
            #endregion

            // --------------------------------------------
            // Load the bones commands
            // --------------------------------------------
            #region
            {
                if (br.BaseStream.Position != BonesOffset)
                    throw new Exception("Bad bones offset!");

                var bones = new List<Bone>();
                byte cmd = 0x0; // It will auto quite or something like that
                do
                {
                    // Read the command
                    cmd = br.ReadByte();

                    var bone = new Bone();
                    bone.Command = cmd;
                    switch (cmd)
                    {
                        case 0x0:
                            bone.Size = 0;
                            break;
                        case 0x1:
                            bone.Size = 0;
                            break;
                        case 0x2:
                            bone.Size = 2;
                            bone.Parameters = new byte[2];
                            bone.Parameters[0] = br.ReadByte();  // Node ID
                            bone.Parameters[1] = br.ReadByte();  // Visibility
                            break;
                        case 0x03:
                            bone.Size = 1;
                            bone.Parameters = new byte[1];
                            bone.Parameters[0] = br.ReadByte();  // Set Polygon Stack ID?
                            break;
                        case 0x04:
                            bone.Parameters = new byte[3];
                            bone.Parameters[0] = br.ReadByte();  // Material ID
                            bone.Parameters[1] = br.ReadByte();  // 0x05
                            bone.Parameters[2] = br.ReadByte();  // Polygon ID
                            break;
                        case 0x05:
                            bone.Size = 1;
                            bone.Parameters = new byte[1];
                            bone.Parameters[0] = br.ReadByte();
                            break;
                        case 0x06:
                            bone.Size = 3;
                            bone.Parameters = new byte[3];
                            bone.Parameters[0] = br.ReadByte();  // Object ID
                            bone.Parameters[1] = br.ReadByte();  // Parent ID
                            bone.Parameters[2] = br.ReadByte();  // Dummy 0
                            break;
                        case 0x07:
                            bone.Size = 1;
                            bone.Parameters = new byte[1];
                            bone.Parameters[0] = br.ReadByte();
                            break;
                        case 0x08:
                            bone.Size = 1;
                            bone.Parameters = new byte[1];
                            bone.Parameters[0] = br.ReadByte();
                            break;
                        case 0x09:
                            bone.Size = 8;
                            bone.Parameters = new byte[8];
                            bone.Parameters[0] = br.ReadByte();
                            bone.Parameters[1] = br.ReadByte();
                            bone.Parameters[2] = br.ReadByte();
                            bone.Parameters[3] = br.ReadByte();
                            bone.Parameters[4] = br.ReadByte();
                            bone.Parameters[5] = br.ReadByte();
                            bone.Parameters[6] = br.ReadByte();
                            bone.Parameters[7] = br.ReadByte();
                            break;
                        case 0x0B:
                            bone.Size = 0;   // Begin Polygon/Material pairing
                            break;
                        case 0x24:
                            bone.Size = 3;
                            bone.Parameters = new byte[3];
                            bone.Parameters[0] = br.ReadByte();  // Material ID
                            bone.Parameters[1] = br.ReadByte();  // 0x05
                            bone.Parameters[2] = br.ReadByte();  // Polygon ID
                            break;
                        case 0x26:
                            bone.Size = 4;
                            bone.Parameters = new byte[4];
                            bone.Parameters[0] = br.ReadByte();  // Object ID
                            bone.Parameters[1] = br.ReadByte();  // Parent ID
                            bone.Parameters[2] = br.ReadByte();  // Dummy 0
                            bone.Parameters[3] = br.ReadByte();  // Stack ID
                            break;
                        case 0x2B:
                            bone.Size = 0;   // End Polygon/Material Pairing
                            break;
                        case 0x44:
                            bone.Size = 3;
                            bone.Parameters = new byte[3];
                            bone.Parameters[0] = br.ReadByte();  // Material ID
                            bone.Parameters[1] = br.ReadByte();  // 0x05
                            bone.Parameters[2] = br.ReadByte();  // Polygon ID
                            break;
                        case 0x46:
                            bone.Size = 4;
                            bone.Parameters = new byte[4];
                            bone.Parameters[0] = br.ReadByte();  // Object ID
                            bone.Parameters[1] = br.ReadByte();  // Parent ID
                            bone.Parameters[2] = br.ReadByte();  // Dummy 0
                            bone.Parameters[3] = br.ReadByte();  // Restore ID
                            break;
                        case 0x66:
                            bone.Size = 5;
                            bone.Parameters = new byte[5];
                            bone.Parameters[0] = br.ReadByte();  // Object ID
                            bone.Parameters[1] = br.ReadByte();  // Parent ID
                            bone.Parameters[2] = br.ReadByte();  // Dummy 0
                            bone.Parameters[3] = br.ReadByte();  // Stack ID
                            bone.Parameters[4] = br.ReadByte();  // Restore ID
                            break;

                        default:
                            throw new Exception("Unknown bone command 0x" + cmd.ToString("X") + "!");
                    }

                    bones.Add(bone);
                } while (cmd != 0x1);

                // Done
                Bones = bones.ToArray();

                // The bones data should end on an offset of 4
                // If it doesn't, there will be filler data
                // The DS likes multiples of four after all ;)
                if (br.BaseStream.Position % 4 != 0)
                {
                    br.BaseStream.Seek(4 - (br.BaseStream.Position % 4), SeekOrigin.Current);
                }
            }
            #endregion

            // --------------------------------------------
            // Load the materials section
            // It tells how textures/palettes should be used
            // --------------------------------------------
            #region
            {
                if (br.BaseStream.Position != MaterialsOffset)
                    throw new Exception("Bad materials offset!");

                // A small header
                uint texOffset = br.ReadUInt16() + MaterialsOffset;
                uint palOffset = br.ReadUInt16() + MaterialsOffset;

                #region 3D Info

                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte materialCount = br.ReadByte();
                ushort materialSectionSize = br.ReadUInt16();

                if (MaterialCount != materialCount)
                    throw new Exception("Invalid material count!");

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort objUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                Materials = new Material[materialCount];
                for (int i = 0; i < materialCount; i++)
                {
                    Materials[i].UnknownBlock = new ushort[2];
                    Materials[i].UnknownBlock[0] = br.ReadUInt16();
                    Materials[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Material info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad material block size!");
                /*ushort dataSize = */
                br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < materialCount; i++)
                {
                    Materials[i].DefinitionOffset = br.ReadUInt32() + MaterialsOffset;
                }

                // Names
                for (int i = 0; i < materialCount; i++)
                {
                    Materials[i].Name = br.ReadString(16);
                }

                #endregion

                #region Texture 3D Info

                if (br.BaseStream.Position != texOffset)
                    throw new Exception("Bad material texture definitions offset!");

                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte textureCount = br.ReadByte();
                ushort texSectionSize = br.ReadUInt16();

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort texUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                TextureDefinition[] texDefs = new TextureDefinition[textureCount];
                for (int i = 0; i < textureCount; i++)
                {
                    texDefs[i].UnknownBlock = new ushort[2];
                    texDefs[i].UnknownBlock[0] = br.ReadUInt16();
                    texDefs[i].UnknownBlock[1] = br.ReadUInt16();
                }

                /*if (textureCount == materialCount)
                    throw new Exception("They are the same count!");*/
                // Material info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad material block size!");
                /*ushort dataSize = */
                br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < textureCount; i++)
                {
                    texDefs[i].MatchingOffset = br.ReadUInt16() + MaterialsOffset;
                    texDefs[i].AssociatedMaterialNum = br.ReadByte();
                    if (br.ReadByte() != 0) throw new Exception("Expected dummy byte in texture def!");

                    /*long pos = br.BaseStream.Position;
                    br.BaseStream.Seek(texDefs[i].MatchingOffset, SeekOrigin.Begin);
                    texDefs[i].AssociatedMaterialID = br.ReadByte();
                    br.BaseStream.Seek(pos, SeekOrigin.Begin);*/
                }

                // Names
                for (int i = 0; i < textureCount; i++)
                {
                    texDefs[i].Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }

                #endregion

                #region Palette 3D Info

                if (br.BaseStream.Position != palOffset)
                    throw new Exception("Bad material palette definitions offset!");

                // Header
                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte paletteCount = br.ReadByte();
                ushort palSectionSize = br.ReadUInt16();

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort palUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                PaletteDefinition[] palDefs = new PaletteDefinition[paletteCount];
                for (int i = 0; i < paletteCount; i++)
                {
                    palDefs[i].UnknownBlock = new ushort[2];
                    palDefs[i].UnknownBlock[0] = br.ReadUInt16();
                    palDefs[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Material info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad material block size!");
                /*ushort dataSize = */
                br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < paletteCount; i++)
                {
                    palDefs[i].MatchingOffset = br.ReadUInt16() + MaterialsOffset;
                    palDefs[i].AssociatedMaterialNum = br.ReadByte();
                    if (br.ReadByte() != 0) throw new Exception("Expected dummy byte in palette def!");

                    /*long pos = br.BaseStream.Position;
                    br.BaseStream.Seek(palDefs[i].MatchingOffset, SeekOrigin.Begin);
                    palDefs[i].AssociatedMaterialID = br.ReadByte();
                    br.BaseStream.Seek(pos, SeekOrigin.Begin);*/
                }

                // Names
                for (int i = 0; i < paletteCount; i++)
                {
                    palDefs[i].Name = br.ReadString(16);
                }

                #endregion

                #region IDs

                // Read texture IDs
                for (int i = 0; i < textureCount; i++)
                {
                    //if (texDefs[i].MatchingOffset != br.BaseStream.Position)
                    //    throw new Exception("Texture def " + i + " doesn't have the correct offset!");

                    br.BaseStream.Seek(texDefs[i].MatchingOffset, SeekOrigin.Begin);
                    texDefs[i].AssociatedMaterialID = br.ReadByte();
                    //MessageBox.Show("Tex " + i + " associacted num " + texDefs[i].AssociatedMaterialNum + " and ID " + texDefs[i].AssociatedMaterialID);
                }

                //if (textureCount % 4 != 0) br.BaseStream.Position += 1;// (textureCount % 4);
                //MessageBox.Show("Current offset: " + br.BaseStream.Position + "\nPal def 0: " + palDefs[0].MatchingOffset + "\nTexture count: " + textureCount);

                // Read palette IDs
                for (int i = 0; i < paletteCount; i++)
                {
                    //if (palDefs[i].MatchingOffset != br.BaseStream.Position)
                    //    throw new Exception("Palette def " + i + " doesn't have the correct offset!\nExpected " + palDefs[i].MatchingOffset.ToString() + " but got " + br.BaseStream.Position.ToString());

                    br.BaseStream.Seek(palDefs[i].MatchingOffset, SeekOrigin.Begin);
                    palDefs[i].AssociatedMaterialID = br.ReadByte();
                }

                //if (paletteCount % 2 != 0) br.BaseStream.Position += 1;


                //MessageBox.Show("Texture defs: " + textureCount + "\nPalette defs: " + paletteCount + "\n\nOffset: " + br.BaseStream.Position.ToString("X"));

                //br.BaseStream.Position += texDefs.Length;


                //br.BaseStream.Position += palDefs.Length;

                /*if (mdl.Materials[0].DefinitionOffset - br.BaseStream.Position > 0)
                {
                    br.BaseStream.Seek(mdl.Materials[0].DefinitionOffset - br.BaseStream.Position, SeekOrigin.Current);
                }*/

                #endregion

                #region Materials Definitions/Matching

                br.BaseStream.Seek(Materials[0].DefinitionOffset, SeekOrigin.Begin);
                for (int i = 0; i < materialCount; i++)
                {
                    if (br.BaseStream.Position != Materials[i].DefinitionOffset)
                        throw new Exception("Bad material def " + i + " offset!\nExpected 0x" + Materials[i].DefinitionOffset.ToString("X") + " but got 0x" + br.BaseStream.Position.ToString("X"));

                    br.ReadBytes(0x2C); // this is an error in tinke ;)

                    // figure out which texture and palette goes to which
                    Materials[i].MatchedTex = false;
                    foreach (var tex in texDefs)
                    {
                        if (tex.AssociatedMaterialID == i)
                        {
                            Materials[i].MatchedTex = true;
                            Materials[i].Texture = tex;
                            break;
                        }
                    }

                    if (!Materials[i].MatchedTex)
                    {
                        //MessageBox.Show("Unable to match a texture for material " + mdl.Materials[i].Name);
                        // TODO: match texture manually ;)
                        foreach (var tex in texDefs)
                        {
                            // For now, just match the first one?
                            if (tex.AssociatedMaterialNum > 1)
                            {
                                Materials[i].MatchedTex = true;
                                Materials[i].Texture = tex;
                                break;
                            }
                        }
                    }

                    Materials[i].MatchedPal = false;
                    foreach (var pal in palDefs)
                    {
                        if (pal.AssociatedMaterialID == i)
                        {
                            Materials[i].MatchedPal = true;
                            Materials[i].Palette = pal;
                            break;
                        }
                    }

                    if (!Materials[i].MatchedPal)
                    {
                        //MessageBox.Show("Unable to match a palette for material " + mdl.Materials[i].Name);
                        // TODO: match texture manually ;)

                        foreach (var pal in palDefs)
                        {
                            if (pal.AssociatedMaterialNum > 1)
                            {
                                Materials[i].MatchedPal = true;
                                Materials[i].Palette = pal;
                                break;
                            }
                        }
                    }
                }

                #endregion
            }
            #endregion

            // --------------------------------------------
            // Load the polygons section
            // --------------------------------------------
            #region
            {
                // Offset check, again.
                if (br.BaseStream.Position != PolygonStartOffset)
                    throw new Exception("Bad polygon start offset!");

                #region 3D Info

                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte polyCount = br.ReadByte();
                ushort polySectionSize = br.ReadUInt16();

                if (PolygonCount != polyCount)
                    throw new Exception("Invalid polygon count!");

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort polyUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                Polygons = new Polygon[polyCount];
                for (int i = 0; i < polyCount; i++)
                {
                    Polygons[i].UnknownBlock = new ushort[2];
                    Polygons[i].UnknownBlock[0] = br.ReadUInt16();
                    Polygons[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Polygon info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad polygon block size!");
                /*ushort dataSize = */
                br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < polyCount; i++)
                {
                    Polygons[i].DefOffset = br.ReadUInt32() + PolygonStartOffset;
                }

                // Names
                for (int i = 0; i < polyCount; i++)
                {
                    Polygons[i].Name = br.ReadString(16);
                }

                #endregion

                #region Polygon Definition

                for (int i = 0; i < polyCount; i++)
                {
                    if (br.BaseStream.Position != Polygons[i].DefOffset)
                        throw new Exception("Bad polygon " + i + " definition offset!");

                    Polygons[i].Unknown1 = br.ReadUInt32();
                    Polygons[i].Unknown2 = br.ReadUInt32();
                    Polygons[i].DisplayOffset = br.ReadUInt32() + Polygons[i].DefOffset;
                    Polygons[i].DisplaySize = br.ReadUInt32();
                }

                #endregion Display List

                #region Geometry Commands

                for (int i = 0; i < polyCount; i++)
                {
                    if (br.BaseStream.Position != Polygons[i].DisplayOffset)
                        throw new Exception("Bad polygon " + i + " display offset!");

                    //byte[] buffer = br.ReadBytes((int)mdl.Polygons[i].DisplaySize);
                    var commands = new List<GeometryCommand>();
                    for (int c = 0; c < Polygons[i].DisplaySize; /*c += 4*/)
                    {
                        // Commands are stored in groups of four
                        byte[] fourCmds = br.ReadBytes(4);
                        c += 4;

                        // Read the commands
                        foreach (byte val in fourCmds)
                        {
                            GeometryCommand cmd = new GeometryCommand();
                            cmd.Value = val;

                            int paramSize = GeometryCommand.GetCommandSize(val);// GetGeometryCommandSize(val);
                            cmd.Parameters = new uint[paramSize];

                            if (paramSize > 0)
                            {
                                for (int n = 0; n < paramSize; n++)
                                {
                                    c += 4;
                                    cmd.Parameters[n] = br.ReadUInt32();
                                }
                            }

                            commands.Add(cmd);
                        }
                    }
                    Polygons[i].Commands = commands.ToArray();
                }

                #endregion

                #region Match Materials to Polygons

                // To do this, we have to look through the bones
                foreach (var bone in Bones)
                {
                    switch (bone.Command)
                    {
                        // All the necessary commands
                        // These three are all the same parameters:
                        // [material ID] [5] [polygon ID]
                        case 0x4:
                        case 0x24:
                        case 0x44:
                            byte material = bone.Parameters[0];
                            if (bone.Parameters[1] != 5) throw new Exception("Bad material-polygon matching bone command!");
                            byte polygon = bone.Parameters[2];

                            if (polygon < PolygonCount)
                            {
                                Polygons[polygon].MaterialID = material;
                            }
                            else
                            {
                                throw new Exception("Uh, help?");
                            }
                            break;

                        default: break;
                    }
                }

                #endregion
            }
            #endregion

            // And that's it!
        }

        public string Name { get; set; }
        public uint Offset { get; set; }
        public ushort[] UnknownBlock { get; } = new ushort[2];

        public uint BlockSize { get; set; }
        public uint BonesOffset { get; set; }
        public uint MaterialsOffset { get; set; }
        public uint PolygonStartOffset { get; set; }
        public uint PolygonEndOffset { get; set; }
        public byte[] Unknown { get; set; }
        public byte ObjectCount { get; set; }
        public byte MaterialCount { get; set; }
        public byte PolygonCount { get; set; }
        public byte Unknown2 { get; set; }
        public byte ScaleMode { get; set; }
        public ulong Unknown3 { get; set; }
        public ushort VertexCount { get; set; }
        public ushort SurfaceCount { get; set; }
        public ushort TriangleCount { get; set; }
        public ushort QuadCount { get; set; }
        public double BoundingX { get; set; }
        public double BoundingY { get; set; }
        public double BoundingZ { get; set; }
        public double BoundingWidth { get; set; }
        public double BoundingHeight { get; set; }
        public double BoundingDepth { get; set; }
        public ulong RuntimeData { get; set; }

        public Obj[] Objects { get; private set; }
        public Bone[] Bones { get; private set; }
        public Material[] Materials { get; private set; }
        public Polygon[] Polygons { get; private set; }

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

        public struct Bone
        {
            public byte Command;

            public int Size;
            public byte[] Parameters;
        }

        public struct Material
        {
            public ushort[] UnknownBlock;
            public uint DefinitionOffset;
            public string Name;

            public TextureDefinition Texture;
            public PaletteDefinition Palette;

            public bool MatchedTex, MatchedPal;
        }

        public struct TextureDefinition
        {
            public ushort[] UnknownBlock;
            public string Name;

            public uint MatchingOffset;
            public byte AssociatedMaterialNum;
            public byte AssociatedMaterialID;
            //public byte Dummy;
        }

        public struct PaletteDefinition
        {
            public ushort[] UnknownBlock;
            public string Name;

            public uint MatchingOffset;
            public byte AssociatedMaterialNum;
            public byte AssociatedMaterialID;
            //public byte Dummy;
        }

        public struct Polygon
        {
            // Header
            public ushort[] UnknownBlock;
            public uint DefOffset;
            public string Name;

            // Definition
            public uint Unknown1, Unknown2;
            public uint DisplayOffset, DisplaySize;

            // Geometry Commands
            public GeometryCommand[] Commands;

            // Materail
            public byte MaterialID;
        }

        public struct GeometryCommand
        {
            public byte Value;
            public uint[] Parameters;

            public static int GetCommandSize(byte cmd)
            {
                switch (cmd)
                {
                    case 0: return 0;

                    case 0x10: return 1;
                    case 0x11: return 0;
                    case 0x12: return 1;
                    case 0x13: return 1;
                    case 0x14: return 1;
                    case 0x15: return 0;
                    case 0x16: return 16;
                    case 0x17: return 12;
                    case 0x18: return 16;
                    case 0x19: return 12;
                    case 0x1A: return 9;
                    case 0x1B: return 3;
                    case 0x1C: return 3;

                    case 0x20: return 1;
                    case 0x21: return 1;
                    case 0x22: return 1;
                    case 0x23: return 2;
                    case 0x24: return 1;
                    case 0x25: return 1;
                    case 0x26: return 1;
                    case 0x27: return 1;
                    case 0x28: return 1;

                    case 0x29: return 1;
                    case 0x2A: return 1;
                    case 0x2B: return 1;

                    case 0x30: return 1;
                    case 0x31: return 1;
                    case 0x32: return 1;
                    case 0x33: return 1;
                    case 0x34: return 32;

                    case 0x40: return 1;
                    case 0x41: return 0;

                    case 0x50: return 1;

                    case 0x60: return 1;

                    case 0x70: return 3;
                    case 0x71: return 2;
                    case 0x72: return 1;

                    default:
                        return 0;
                }
            }
        }
    }
}
