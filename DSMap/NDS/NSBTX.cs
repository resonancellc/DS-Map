using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

//
// https://github.com/pleonex/tinke/blob/master/Plugins/3DModels/3DModels/BTX0.cs
// http://llref.emutalk.net/docs/?file=xml/btx0.xml#xml-doc
//

namespace DSMap.NDS
{
    public static class NSBTXLoader
    {
        // Magic stamps
        public const uint BTX0MAGIC = 0x30585442;
        public const uint BTX0VERSION = 0x0001FEFF;

        public const uint TEX0MAGIC = 0x30584554;

        //                                                 0  1  2  3  4  5  6  7
        public static byte[] FormatBitDepth = new byte[] { 0, 8, 2, 4, 8, 2, 8, 16 };

        public static TEX0 LoadBTX0(string file)
        {
            // BTX0 all has stuff we can calculate at runtime
            // So we'll just read it, then load the TEX0 section
            TEX0 tex0 = new TEX0();
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                // BTX0 Header
                if (br.ReadUInt32() != BTX0MAGIC)
                    throw new Exception("This is not a NSBTX file!");
                if (br.ReadUInt32() != BTX0VERSION)
                    throw new Exception("Invalid NSBTX version/format!");
                if (br.BaseStream.Length < br.ReadUInt32())
                    throw new Exception("Invalid NSBTX file size!");
                if (br.ReadUInt16() != 0x10)
                    throw new Exception("Invalid BTX0 header size!");
                if (br.ReadUInt16() != 1)
                    throw new Exception("Cannot load an NSBTX file with more than 1 section!");
                
                // Assuming all goes right, this will be at the correct offset
                uint tex0Offest = br.ReadUInt32();
                if (br.BaseStream.Position != tex0Offest)
                    throw new Exception("Wut? Bad TEX0 offset!");

                // Read TEX0 section
                tex0 = LoadTEX0(br);
            }

            return tex0;
        }

        public static TEX0 LoadTEX0(string file)
        {
            TEX0 tex0 = new TEX0();
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                tex0 = LoadTEX0(br);
            }
            return tex0;
        }

        public static TEX0 LoadTEX0(BinaryReader br)
        {
            TEX0 tex = new TEX0();

            uint tex0Offset = (uint)br.BaseStream.Position;

            // Header
            // This is the worst format
            #region Header
            {
                // This accounts for relative offset nonsense
                // So if we read this from a BTX0 file or something, it will know
                uint offsetAdjust = (uint)br.BaseStream.Position;

                if (br.ReadUInt32() != TEX0MAGIC)
                    throw new Exception("Invalid TEX0 magic stamp!");
                uint sectionSize = br.ReadUInt32(); // not sure what to do with this yet
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding
                tex.TextureDataSize = br.ReadUInt16();
                tex.TextureInfoOffset = (ushort)(br.ReadUInt16() + offsetAdjust);
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding 2
                tex.TextureDataOffset = br.ReadUInt32() + offsetAdjust; // - 0x14?
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding 3
                tex.CompressedTextureDataSize = (ushort)(br.ReadUInt16() << 3);
                tex.CompressedTextureInfoOffset = (ushort)(br.ReadUInt16() + offsetAdjust);
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding 4
                tex.CompressedTextureDataOffset = br.ReadUInt32() + offsetAdjust;
                tex.CompressedTextureInfoDataOffset = br.ReadUInt32() + offsetAdjust;
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding
                tex.PaletteDataSize = (uint)(br.ReadUInt32() << 3);
                tex.PaletteInfoOffset = br.ReadUInt32() + offsetAdjust;
                tex.PaletteDataOffset = br.ReadUInt32() + offsetAdjust;


            }
            #endregion

            // Texture Info section
            // Offset 0x50
            #region Texture Info3D
            {
                // ...
                if (br.ReadByte() != 0x0) throw new Exception("Expected dummy byte!");
                byte objectCount = br.ReadByte();
                ushort sectionSize = br.ReadUInt16();

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort unknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                tex.TextureInfo = new Info3D(); // Yeah
                tex.TextureInfo.UnknownBlock1 = new ushort[objectCount];
                tex.TextureInfo.UnknownBlock2 = new ushort[objectCount];
                for (int j = 0; j < objectCount; j++)
                {
                    tex.TextureInfo.UnknownBlock1[j] = br.ReadUInt16();
                    tex.TextureInfo.UnknownBlock2[j] = br.ReadUInt16();
                }

                // Texture info block
                if (br.ReadUInt16() != 8) throw new Exception("Bad texture block size!");
                ushort dataSize = br.ReadUInt16(); // not sure if we'll ever want this

                tex.TextureInfo.InfoBlock = new Info3D.InfoSection[objectCount];
                for (int k = 0; k < objectCount; k++)
                {
                    Info3D.TextureInfo texInfo = new Info3D.TextureInfo();
                    texInfo.Offset = (uint)(br.ReadUInt16() * 8); // This is a ushort
                    ushort parameters = br.ReadUInt16();

                    texInfo.Width2 = br.ReadByte(); // look into this nonsense
                    texInfo.Unknown = br.ReadByte();
                    texInfo.Unknown2 = br.ReadByte();
                    texInfo.Unknown3 = br.ReadByte();

                    // Do the parameters
                    texInfo.CoordTransf = (byte)(parameters & 14);
                    texInfo.Color0 = (byte)((parameters >> 13) & 1);
                    texInfo.Format = (byte)((parameters >> 10) & 7);
                    texInfo.Height = (byte)(8 << ((parameters >> 7) & 7));
                    texInfo.Width = (byte)(8 << ((parameters >> 4) & 7));
                    texInfo.FlipY = (byte)((parameters >> 3) & 1);
                    texInfo.FlipX = (byte)((parameters >> 2) & 1);
                    texInfo.RepeatY = (byte)((parameters >> 1) & 1);
                    texInfo.RepeatX = (byte)(parameters & 1);

                    // Not sure why, but this is a thing
                    if (texInfo.Width == 0)
                    {
                        if ((texInfo.Unknown & 3) == 2) texInfo.Width = 0x200;
                        else texInfo.Height = 0x100;
                    }

                    if (texInfo.Height == 0)
                    {
                        if (((texInfo.Unknown >> 4) & 3) == 2) texInfo.Height = 0x200;
                        else texInfo.Height = 0x100;
                    }

                    // Do some stuff with the format
                    texInfo.BitDepth = FormatBitDepth[texInfo.Format];
                    if (texInfo.Format == 5)
                    {
                        // Compressed
                        texInfo.Compressed = true;
                    }

                    // Finally, adjust the offset according to where we'll read it from
                    if (texInfo.Compressed)
                    {
                        texInfo.Offset += tex.CompressedTextureDataOffset;
                    }
                    else
                    {
                        texInfo.Offset += tex.TextureDataOffset;
                    }
                    //if (texInfo.Width << 3 != texInfo.Width2) throw new Exception("Uh-oh!\nWidth: " + (texInfo.Width << 3) + "\nWidth2: " + texInfo.Width2);
                    tex.TextureInfo.InfoBlock[k] = texInfo;
                }

                // Names
                tex.TextureInfo.NameBlock = new string[objectCount];
                for (int n = 0; n < objectCount; n++)
                {
                    tex.TextureInfo.NameBlock[n] = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }

                // Just one more thing
                tex.TextureData = new byte[objectCount][];
            }
            #endregion

            // Offset variable
            #region Palette Info3D
            {
                if (br.ReadByte() != 0x0) throw new Exception("Expected dummy byte!");
                byte objectCount = br.ReadByte();
                ushort sectionSize = br.ReadUInt16();

                // Uknown block
                if (br.ReadUInt16() != 8) throw new Exception("Bad unknown block size!");
                ushort unknownBlockSize = br.ReadUInt16();
                if (br.ReadUInt32() != 0x0000017F) // offset 0x58
                    throw new Exception("Bad unknown block constant!");

                tex.PaletteInfo = new Info3D(); // Yeah
                tex.PaletteInfo.UnknownBlock1 = new ushort[objectCount];
                tex.PaletteInfo.UnknownBlock2 = new ushort[objectCount];
                for (int j = 0; j < objectCount; j++)
                {
                    tex.PaletteInfo.UnknownBlock1[j] = br.ReadUInt16();
                    tex.PaletteInfo.UnknownBlock2[j] = br.ReadUInt16();
                }

                // Palette info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad palette block size!");
                //throw new Exception("Bad palette block size " + br.ReadUInt16());
                ushort dataSize = br.ReadUInt16(); // not sure if we'll ever want this

                tex.PaletteInfo.InfoBlock = new Info3D.InfoSection[objectCount];

                uint lastPalOffset = (uint)br.BaseStream.Position;
                for (int i = 0; i < objectCount; i++)
                {
                    Info3D.PaletteInfo palInfo = new Info3D.PaletteInfo();
                    palInfo.Offset = br.ReadUInt16() + tex.PaletteInfoOffset;
                    palInfo.Unkown = br.ReadUInt16();
                    tex.PaletteInfo.InfoBlock[i] = palInfo;
                }

                // Read name block
                tex.PaletteInfo.NameBlock = new string[objectCount];
                for (int n = 0; n < objectCount; n++)
                {
                    tex.PaletteInfo.NameBlock[n] = Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }
            }
            #endregion

            //throw new Exception("Data: 0x" + tex.TextureDataOffset.ToString("X"));

            //
            #region Texture Data
            {
                //throw new Exception("Data: 0x" + tex.TextureDataOffset.ToString("X"));
                //if (br.BaseStream.Position != tex.TextureDataOffset) throw new Exception("Uh-oh.\nActual: 0x" + br.BaseStream.Position.ToString("X") + "\nExpected: 0x" + tex.TextureDataOffset.ToString("X"));
                for (int i = 0; i < tex.TextureInfo.InfoBlock.Length; i++)
                {
                    // Hi
                    var texInfo = (Info3D.TextureInfo)tex.TextureInfo.InfoBlock[i];

                    // We skip compressed textures
                    if (texInfo.Compressed) continue;

                    // Offset test
                    //if (texInfo.Offset != br.BaseStream.Position)
                    //    throw new Exception("Oops " + i + ".\nActual: 0x" + br.BaseStream.Position.ToString("X") + "\nExpected: 0x" + (texInfo.Offset).ToString("X"));

                    //byte[] tiles = br.ReadBytes(texInfo.Width * texInfo.Height * texInfo.BitDepth / 8);
                    tex.TextureData[i] = br.ReadBytes(texInfo.Width * texInfo.Height * texInfo.BitDepth / 8);
                }
            }
            #endregion


            #region Compressed Texture Data
            {
                //if (br.BaseStream.Position != tex.CompressedTextureDataOffset) throw new Exception("Uh-oh.\nActual: 0x" + br.BaseStream.Position.ToString("X") + "\nExpected: 0x" + tex.CompressedTextureDataOffset.ToString("X"));
                for (int i = 0; i < tex.TextureInfo.InfoBlock.Length; i++)
                {
                    // Get texture info
                    var texInfo = (Info3D.TextureInfo)tex.TextureInfo.InfoBlock[i];

                    // This time, skip the non-compressed textures
                    if (!texInfo.Compressed) continue;

                    // Offset test
                    //if (texInfo.Offset != br.BaseStream.Position)
                    //    throw new Exception("Oops " + i + ".\nActual: 0x" + br.BaseStream.Position.ToString("X") + "\nExpected: 0x" + (texInfo.Offset).ToString("X"));

                    //byte[] tiles = br.ReadBytes(texInfo.Width * texInfo.Height * texInfo.BitDepth / 8);
                    tex.TextureData[i] = br.ReadBytes(texInfo.Width * texInfo.Height * texInfo.BitDepth / 8);
                }
            }
            #endregion

            // I know the least about this
            // It's very odd
            // So if I make a writer, I may just have it so that it doesn't write any at all
            #region Compressed Texture Info
            {
                // Compressed textures allow for lots of palettes to be used across a large texture
                // The texture is broken into "tiles", which all reference a specific palette
                // The palettes are all at most 4 colors
                // Each tile is 4 x 4 pixels, and stored in a 32-bit unsigned integer
                // (there are 2 bits per pixel)
                // The format is as follows:
                // [tile - uint32] [palette_info - uint16]
                // > tile is as stated above
                // > palette_info breaks into two parts, palette_offset and palette_mode
                // >> palette_offset is the first 12 bits
                // >>> This gives the relative offset in the palette data for the desired palette
                // >> palette_mode is the last 2 bits
                // >>> This gives the color blending mode. Very odd.
                // >> 2 bits remain unused?

                // Each entry is width * height / 8 bytes long

                // Why someone made such a convoluted format is beyond me

                // Whoo...
                List<byte[]> data = new List<byte[]>();
                for (int i = 0; i < tex.TextureInfo.InfoBlock.Length; i++)
                {
                    // Get texture info
                    var texInfo = (Info3D.TextureInfo)tex.TextureInfo.InfoBlock[i];

                    // This time, skip the non-compressed textures
                    if (!texInfo.Compressed) continue;

                    // Read the info
                    data.Add(br.ReadBytes(texInfo.Width * texInfo.Height / 8));
                }
                tex.CompressedInfoData = data.ToArray();
                // This will be horrible to convert to a bitmap
            }
            #endregion

            #region Palette Data
            {
                if (br.BaseStream.Position != tex.PaletteDataOffset) throw new Exception("Uh-oh. Bad palette data offset!\nActual: 0x" + br.BaseStream.Position.ToString("X") + "\nExpected: 0x" + tex.PaletteDataOffset.ToString("X"));

                // A palette's size can only be determined by the texture that's using it's format
                // That means I'll just have to load all the colors into memory
                // I can use this array to get the palette sizes depending on format:
                // static int[] PaletteSize = new int[] { 0x00, 0x40, 0x08, 0x20, 0x200, 0x200, 0x10, 0x00 };
                // (that's in bytes, so I'll adjust it for colors next...)

                int[] paletteSizes = new int[tex.PaletteInfo.InfoBlock.Length];
                for (int i = 0; i < tex.PaletteInfo.InfoBlock.Length; i++)
                {

                }


                /*int colorCount = (int)tex.PaletteDataSize / 2;
                tex.PaletteData = new Color[colorCount];

                for (int i = 0; i < colorCount; i++)
                {
                    // Read 15-bit color -- Nintendo loves these
                    ushort color = br.ReadUInt16();

                    // Get color components
                    int r = (color & 0x1F) * 8;
                    int g = ((color & 0x3E0) >> 5) * 8;
                    int b = ((color & 0x7C00) >> 10) * 8;

                    // Color-ify
                    tex.PaletteData[i] = Color.FromArgb(r, g, b);
                }*/

                //throw new Exception("Palette Count: " + paletteCount + "\nActual: " + tex.PaletteInfo.InfoBlock.Length);
            }
            #endregion

            return tex;
        }
    }

    // File
    /*public struct BTX0
    {

    }*/

    // Texture
    public struct TEX0
    {
        // Header
        //public char[] type;
        //public uint section_size;
        //public uint padding1;
        public ushort TextureDataSize;
        public ushort TextureInfoOffset;
        //public uint padding2;
        public uint TextureDataOffset;
        //public uint padding3;
        public ushort CompressedTextureDataSize;
        public ushort CompressedTextureInfoOffset;
        //public uint padding4;
        public uint CompressedTextureDataOffset;
        public uint CompressedTextureInfoDataOffset;
        //public uint padding5;
        public uint PaletteDataSize;
        public uint PaletteInfoOffset;
        public uint PaletteDataOffset;

        // Some stuff
        public Info3D TextureInfo;
        public Info3D PaletteInfo;
        public byte[][] TextureData; // Regular and compressed
        public byte[][] CompressedInfoData;
        public Color[] PaletteData;
    }

    // Used for Textures and Models
    public struct Info3D
    {
        // The unknown block is in all 3D information blocks
        // Then, it has a unique format depending on type
        public ushort[] UnknownBlock1;
        public ushort[] UnknownBlock2;

        public InfoSection[] InfoBlock;

        public string[] NameBlock;

        public interface InfoSection { }

        public struct TextureInfo : InfoSection
        {
            // Custom info
            public uint Offset;
            public ushort Parameters;
            public byte Width2;
            public byte Unknown, Unknown2, Unknown3;

            // Parameters
            public byte RepeatX;   // 0 = freeze; 1 = repeat
            public byte RepeatY;   // 0 = freeze; 1 = repeat
            public byte FlipX;     // 0 = no; 1 = flip each 2nd texture (requires repeat)
            public byte FlipY;     // 0 = no; 1 = flip each 2nd texture (requires repeat)
            public ushort Width;      // 8 << width
            public ushort Height;     // 8 << height
            public byte Format;     // Texture format (bit depth)
            public byte Color0; // 0 = displayed; 1 = transparent
            public byte CoordTransf; // Texture coordination transformation mode

            public byte BitDepth; // gotten from format
            //public uint CompressedDataStart;
            public bool Compressed;
        }

        public struct PaletteInfo : InfoSection
        {
            public uint Offset;
            public int Size;
            public ushort Unkown; // either 0 or 1
        }
    }

    // A better thing?
    public struct NSBTX
    {
        // Header
        //public char[] type;
        //public uint section_size;
        //public uint padding1;
        public ushort TextureDataSize;
        public ushort TextureInfoOffset;
        //public uint padding2;
        public uint TextureDataOffset;
        //public uint padding3;
        public ushort CompressedTextureDataSize;
        public ushort CompressedTextureInfoOffset;
        //public uint padding4;
        public uint CompressedTextureDataOffset;
        public uint CompressedTextureInfoDataOffset;
        //public uint padding5;
        public uint PaletteDataSize;
        public uint PaletteInfoOffset;
        public uint PaletteDataOffset;

        public Texture[] Textures;
        public Palette[] Palettes;
        public Color[] PaletteColors;

        public struct Texture
        {
            //
            public ushort[] UnkownBlock;

            // Info block
            public uint Offset;
            public ushort Parameters;
            public byte Width2;
            public byte Unknown, Unknown2, Unknown3;

            // Parameters
            public byte RepeatX;   // 0 = freeze; 1 = repeat
            public byte RepeatY;   // 0 = freeze; 1 = repeat
            public byte FlipX;     // 0 = no; 1 = flip each 2nd texture (requires repeat)
            public byte FlipY;     // 0 = no; 1 = flip each 2nd texture (requires repeat)
            public ushort Width;      // 8 << width
            public ushort Height;     // 8 << height
            public byte Format;     // Texture format (bit depth)
            public byte Color0; // 0 = displayed; 1 = transparent
            public byte CoordTransf; // Texture coordination transformation mode

            public byte BitDepth; // gotten from format
            //public uint CompressedDataStart;
            public bool Compressed;

            // Name block
            public string Name;

            //
            public byte[] TileData;
            // yeah...
        }

        public struct Palette
        {
            //
            public ushort[] UnknownBlock;

            public uint Offset;
            public ushort Unknown;

            public string Name;
        }
    }
}
