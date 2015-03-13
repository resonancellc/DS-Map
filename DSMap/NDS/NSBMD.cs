using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
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
                throw new Exception("Invalid section number!");

            uint mdl0Offest = br.ReadUInt32() + bmd0Offset;
            uint tex0Offset = (sectionCount == 2 ? br.ReadUInt32() + bmd0Offset : 0);

            // Read MDL0
            if (mdl0Offest != br.BaseStream.Position)
                throw new Exception("Unexpected error, baby.");
            
            // 
            bmd.MDL0 = LoadMDL0(br);

            if (sectionCount == 2)
            {
                //MessageBox.Show("Ooh!\nThis model has textures included!");

                // Read TEX0 section (if it has one)
                br.BaseStream.Seek(tex0Offset, SeekOrigin.Begin);
                bmd.TEX0 = NSBTXLoader.LoadTEX0(br);

                bmd.HasTEX0 = true;
            }

            return bmd;
        }

        public static Model LoadMDL0(BinaryReader br)
        {
            Model mdl = new Model();
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
            #region
            mdl.BlockSize = br.ReadUInt32();
            mdl.BonesOffset = br.ReadUInt32() + mdl.Offset;
            mdl.MaterialsOffset = br.ReadUInt32() + mdl.Offset;
            mdl.PolygonStartOffset = br.ReadUInt32() + mdl.Offset;
            mdl.PolygonEndOffset = br.ReadUInt32() + mdl.Offset;
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

                if (objectCount != mdl.ObjectCount)
                    throw new Exception("objectCount doesn't match mdl.ObjectCount!\n" + objectCount + " vs. " + mdl.ObjectCount + "\n 0x" + br.BaseStream.Position.ToString("X"));

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort objUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                // It doesn't have any effect on displaying?
                mdl.Objects = new Model.Obj[objectCount];
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
                    //MessageBox.Show("Object Data @ 0x" + br.BaseStream.Position.ToString("X"));

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
            #region
            {
                if (br.BaseStream.Position != mdl.BonesOffset)
                    throw new Exception("Bad bones offset!");

                List<Model.Bone> bones = new List<Model.Bone>();
                byte cmd = 0x0; // It will auto quite or something like that
                do
                {
                    // Read the command
                    cmd = br.ReadByte();

                    var bone = new Model.Bone();
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
                mdl.Bones = bones.ToArray();

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
                if (br.BaseStream.Position != mdl.MaterialsOffset)
                    throw new Exception("Bad materials offset!");

                // A small header
                uint texOffset = br.ReadUInt16() + mdl.MaterialsOffset;
                uint palOffset = br.ReadUInt16() + mdl.MaterialsOffset;

                #region 3D Info

                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte materialCount = br.ReadByte();
                ushort materialSectionSize = br.ReadUInt16();

                if (mdl.MaterialCount != materialCount)
                    throw new Exception("Invalid material count!");

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort objUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                mdl.Materials = new Model.Material[materialCount];
                for (int i = 0; i < materialCount; i++)
                {
                    mdl.Materials[i].UnknownBlock = new ushort[2];
                    mdl.Materials[i].UnknownBlock[0] = br.ReadUInt16();
                    mdl.Materials[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Material info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad material block size!");
                /*ushort dataSize = */
                br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < materialCount; i++)
                {
                    mdl.Materials[i].DefinitionOffset = br.ReadUInt32() + mdl.MaterialsOffset;
                }

                // Names
                for (int i = 0; i < materialCount; i++)
                {
                    mdl.Materials[i].Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
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

                Model.TextureDef[] texDefs = new Model.TextureDef[textureCount];
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
                    texDefs[i].MatchingOffset = br.ReadUInt16() + mdl.MaterialsOffset;
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

                Model.PaletteDef[] palDefs = new Model.PaletteDef[paletteCount];
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
                    palDefs[i].MatchingOffset = br.ReadUInt16() + mdl.MaterialsOffset;
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
                    palDefs[i].Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
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

                br.BaseStream.Seek(mdl.Materials[0].DefinitionOffset, SeekOrigin.Begin);
                for (int i = 0; i < materialCount; i++)
                {
                    if (br.BaseStream.Position != mdl.Materials[i].DefinitionOffset)
                        throw new Exception("Bad material def " + i + " offset!\nExpected 0x" + mdl.Materials[i].DefinitionOffset.ToString("X") + " but got 0x" + br.BaseStream.Position.ToString("X"));

                    br.ReadBytes(0x2C); // this is an error in tinke ;)

                    // figure out which texture and palette goes to which
                    mdl.Materials[i].MatchedTex = false;
                    foreach (var tex in texDefs)
                    {
                        if (tex.AssociatedMaterialID == i)
                        {
                            mdl.Materials[i].MatchedTex = true;
                            mdl.Materials[i].Texture = tex;
                            break;
                        }
                    }

                    if (!mdl.Materials[i].MatchedTex)
                    {
                        //MessageBox.Show("Unable to match a texture for material " + mdl.Materials[i].Name);
                        // TODO: match texture manually ;)
                        foreach (var tex in texDefs)
                        {
                            // For now, just match the first one?
                            if (tex.AssociatedMaterialNum > 1)
                            {
                                mdl.Materials[i].MatchedTex = true;
                                mdl.Materials[i].Texture = tex;
                                break;
                            }
                        }
                    }

                    mdl.Materials[i].MatchedPal = false;
                    foreach (var pal in palDefs)
                    {
                        if (pal.AssociatedMaterialID == i)
                        {
                            mdl.Materials[i].MatchedPal = true;
                            mdl.Materials[i].Palette = pal;
                            break;
                        }
                    }

                    if (!mdl.Materials[i].MatchedPal)
                    {
                        //MessageBox.Show("Unable to match a palette for material " + mdl.Materials[i].Name);
                        // TODO: match texture manually ;)

                        foreach (var pal in palDefs)
                        {
                            if (pal.AssociatedMaterialNum > 1)
                            {
                                mdl.Materials[i].MatchedPal = true;
                                mdl.Materials[i].Palette = pal;
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
                if (br.BaseStream.Position != mdl.PolygonStartOffset)
                    throw new Exception("Bad polygon start offset!");

                #region 3D Info

                if (br.ReadByte() != 0) throw new Exception("Expected dummy byte!");
                byte polyCount = br.ReadByte();
                ushort polySectionSize = br.ReadUInt16();

                if (mdl.PolygonCount != polyCount)
                    throw new Exception("Invalid polygon count!");

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort polyUnknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                mdl.Polygons = new Model.Polygon[polyCount];
                for (int i = 0; i < polyCount; i++)
                {
                    mdl.Polygons[i].UnknownBlock = new ushort[2];
                    mdl.Polygons[i].UnknownBlock[0] = br.ReadUInt16();
                    mdl.Polygons[i].UnknownBlock[1] = br.ReadUInt16();
                }

                // Polygon info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad polygon block size!");
                /*ushort dataSize = */
                br.ReadUInt16(); // not sure if we'll ever want this

                for (int i = 0; i < polyCount; i++)
                {
                    mdl.Polygons[i].DefOffset = br.ReadUInt32() + mdl.PolygonStartOffset;
                }

                // Names
                for (int i = 0; i < polyCount; i++)
                {
                    mdl.Polygons[i].Name = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }

                #endregion

                #region Polygon Definition

                for (int i = 0; i < polyCount; i++)
                {
                    if (br.BaseStream.Position != mdl.Polygons[i].DefOffset)
                        throw new Exception("Bad polygon " + i + " definition offset!");

                    mdl.Polygons[i].Unknown1 = br.ReadUInt32();
                    mdl.Polygons[i].Unknown2 = br.ReadUInt32();
                    mdl.Polygons[i].DisplayOffset = br.ReadUInt32() + mdl.Polygons[i].DefOffset;
                    mdl.Polygons[i].DisplaySize = br.ReadUInt32();
                }

                #endregion Display List

                #region Geometry Commands

                for (int i = 0; i < polyCount; i++)
                {
                    if (br.BaseStream.Position != mdl.Polygons[i].DisplayOffset)
                        throw new Exception("Bad polygon " + i + " display offset!");

                    //byte[] buffer = br.ReadBytes((int)mdl.Polygons[i].DisplaySize);
                    List<Model.GeoCommand> commands = new List<Model.GeoCommand>();
                    for (int c = 0; c < mdl.Polygons[i].DisplaySize; /*c += 4*/)
                    {
                        // Commands are stored in groups of four
                        byte[] fourCmds = br.ReadBytes(4);
                        c += 4;

                        // Read the commands
                        foreach (byte val in fourCmds)
                        {
                            Model.GeoCommand cmd = new Model.GeoCommand();
                            cmd.Value = val;

                            int paramSize = GetGeometryCommandSize(val);
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
                    mdl.Polygons[i].Commands = commands.ToArray();
                }

                #endregion

                #region Match Materials to Polygons

                // To do this, we have to look through the bones
                foreach (var bone in mdl.Bones)
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

                            if (polygon < mdl.PolygonCount)
                            {
                                mdl.Polygons[polygon].MaterialID = material;
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

            return mdl;
        }

        public static byte[] ExtractMDL0(string file)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                return ExtractMDL0(br);
            }
        }

        public static byte[] ExtractMDL0(BinaryReader br)
        {
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
            if (sectionCount == 1)
            {
                uint mdl0Offset = br.ReadUInt32() + bmd0Offset;
                int mdl0Size = (int)(br.BaseStream.Length - mdl0Offset);

                br.BaseStream.Seek(mdl0Offset, SeekOrigin.Begin);
                return br.ReadBytes(mdl0Size);
            }
            else if (sectionCount == 2)
            {
                uint mdl0Offset = br.ReadUInt32() + bmd0Offset;
                uint tex0Offset = br.ReadUInt32() + bmd0Offset;
                int mdl0Size = (int)(tex0Offset - mdl0Offset);

                br.BaseStream.Seek(mdl0Offset, SeekOrigin.Begin);
                return br.ReadBytes(mdl0Size);
            }
            else
            {
                throw new Exception("Invalid section number!");
            }
        }

        public static int GetGeometryCommandSize(byte cmd)
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

        public static double GetSignedFixedPoint(ushort value)
        {
            double point = ((value >> 12) & 7);

            point += (double)(value & 0xFFF) / 0x1000;

            // sign
            if ((value >> 15) == 1)
                point = -point;

            return point;
        }

        public static float GetDouble(int value, bool signed, int integer, int fractional)
        {
            int integerMask = 0;
            float point = 0;

            if (signed)
            {
                if ((value >> (integer + fractional)) == 1)
                {
                    integerMask = (int)Math.Pow(2, integer + 1) - 1;
                    int intPart = ((value >> fractional) & integerMask);
                    point = intPart - (int)Math.Pow(2, integer + 1);
                }
                else
                {
                    integerMask = (int)Math.Pow(2, integer) - 1;
                    point = ((value >> fractional) & integerMask);
                }
            }


            // Fractional part
            int fractionalMask = (int)Math.Pow(2, fractional) - 1;
            point += (float)(value & fractionalMask) / (fractionalMask + 1);

            return point;
        }
    }

    public class NSBMDSaver
    {
        public const uint BMD0MAGIC = 0x30444D42;
        public const uint BMD0VERSION = 0x0002FEFF; // endian/version 2
        public const uint MDL0MAGIC = 0x304C444D;

        public static byte[] CreateBMD0(byte[] mdl0)
        {
            string file = Temporary.GetTemporaryFileName();
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // Header
                bw.Write(BMD0MAGIC);
                bw.Write(BMD0VERSION);
                bw.Write((uint)(16 + mdl0.Length));
                bw.Write((ushort)16);
                bw.Write((ushort)1); // sections

                bw.Write((uint)0x14);
                bw.Write(mdl0);
            }

            byte[] buffer = File.ReadAllBytes(file);
            return buffer;
        }

        public static byte[] CreateBMD0(byte[] mdl0, byte[] tex0)
        {
            string file = Temporary.GetTemporaryFileName();
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // Header
                bw.Write(BMD0MAGIC);
                bw.Write(BMD0VERSION);
                bw.Write((uint)(16 + mdl0.Length + tex0.Length));
                bw.Write((ushort)16);
                bw.Write((ushort)2); // sections

                bw.Write((uint)0x18);
                bw.Write((uint)0x18 + mdl0.Length);

                bw.Write(mdl0);
                bw.Write(tex0);
            }

            byte[] buffer = File.ReadAllBytes(file);
            return buffer;
        }
    }

    public struct NSBMD
    {
        public Model MDL0; 

        public bool HasTEX0;
        public NSBTX TEX0;
    }

    public struct Model
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
        public Bone[] Bones;
        public Material[] Materials;
        public Polygon[] Polygons;

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

            public TextureDef Texture;
            public PaletteDef Palette;

            public bool MatchedTex, MatchedPal;
        }

        public struct TextureDef
        {
            public ushort[] UnknownBlock;
            public string Name;

            public uint MatchingOffset;
            public byte AssociatedMaterialNum;
            public byte AssociatedMaterialID;
            //public byte Dummy;
        }

        public struct PaletteDef
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
            public GeoCommand[] Commands;

            // Materail
            public byte MaterialID;
        }

        public struct GeoCommand
        {
            public byte Value;
            public uint[] Parameters;
        }
    }

    public enum GeometryCmd : byte
    {
        Unknown,
        NOP = 0x00,
        MTX_MODE = 0x10,
        MTX_PUSH = 0x11,
        MTX_POP = 0x12,
        MTX_STORE = 0x13,
        MTX_RESTORE = 0x14,
        MTX_IDENTITY = 0x15,
        MTX_LOAD_4x4 = 0x16,
        MTX_LOAD_4x3 = 0x17,
        MTX_MULT_4x4 = 0x18,
        MTX_MULT_4x3 = 0x19,
        MTX_MULT_3x3 = 0x1A,
        MTX_SCALE = 0x1B,
        MTX_TRANS = 0x1C,
        COLOR = 0x20,
        NORMAL = 0x21,
        TEXCOORD = 0x22,
        VTX_16 = 0x23,
        VTX_10 = 0x24,
        VTX_XY = 0x25,
        VTX_XZ = 0x26,
        VTX_YZ = 0x27,
        VTX_DIFF = 0x28,
        POLYGON_ATTR = 0x29,
        TEXIMAGE_PARAM = 0x2A,
        PLTT_BASE = 0x2B,
        DIF_AMB = 0x30,
        SPE_EMI = 0x31,
        LIGHT_VECTOR = 0x32,
        LIGHT_COLOR = 0x33,
        SHININESS = 0x34,
        BEGIN_VTXS = 0x40,
        END_VTXS = 0x41,
        SWAP_BUFFERS = 0x50,
        VIEWPORT = 0x60,
        BOX_TEST = 0x70,
        POS_TEST = 0x71,
        VEC_TEST = 0x72,
    }
}
