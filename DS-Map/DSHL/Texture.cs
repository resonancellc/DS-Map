using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Lost
{
    public class TextureSet
    {
        public const uint BTX0MAGIC = 0x30585442;
        public const uint BTX0VERSION = 0x0001FEFF;
        public const uint TEX0MAGIC = 0x30584554;

        //                                          0  1  2  3  4  5  6  7
        static byte[] FormatBitDepth = new byte[] { 0, 8, 2, 4, 8, 2, 8, 16 };

        public TextureSet(Stream stream)
        {
            using (var br = new BinaryReader(stream))
                LoadBTX0(br);
        }

        void LoadBTX0(BinaryReader br)
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
            LoadTEX0(br);
        }

        void LoadTEX0(BinaryReader br)
        {
            uint tex0Offset = (uint)br.BaseStream.Position;

            // --------------------------------------------
            // Load the header
            // --------------------------------------------
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
                TextureDataSize = br.ReadUInt16();
                TextureInfoOffset = (ushort)(br.ReadUInt16() + offsetAdjust);
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding 2
                TextureDataOffset = br.ReadUInt32() + offsetAdjust; // - 0x14?
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding 3
                CompressedTextureDataSize = (ushort)(br.ReadUInt16() << 3);
                CompressedTextureInfoOffset = (ushort)(br.ReadUInt16() + offsetAdjust);
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding 4
                CompressedTextureDataOffset = br.ReadUInt32() + offsetAdjust;
                CompressedTextureInfoDataOffset = br.ReadUInt32() + offsetAdjust;
                br.BaseStream.Seek(4L, SeekOrigin.Current); // padding
                PaletteDataSize = (uint)(br.ReadUInt32() << 3);
                PaletteInfoOffset = br.ReadUInt32() + offsetAdjust;
                PaletteDataOffset = br.ReadUInt32() + offsetAdjust;


            }
            #endregion

            // --------------------------------------------
            // Load the 3D Info sections
            // --------------------------------------------

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

                Textures = new Texture[objectCount];
                for (int j = 0; j < objectCount; j++)
                {
                    Textures[j].UnknownBlock = new ushort[2];
                    Textures[j].UnknownBlock[0] = br.ReadUInt16();
                    Textures[j].UnknownBlock[1] = br.ReadUInt16();
                }

                // Texture info block
                if (br.ReadUInt16() != 8) throw new Exception("Bad texture block size!");
                ushort dataSize = br.ReadUInt16(); // not sure if we'll ever want this

                for (int k = 0; k < objectCount; k++)
                {
                    Textures[k].Offset = (uint)(br.ReadUInt16() * 8); // This is a ushort
                    ushort parameters = br.ReadUInt16();

                    Textures[k].Width2 = br.ReadByte(); // look into this nonsense
                    Textures[k].Unknown = br.ReadByte();
                    Textures[k].Unknown2 = br.ReadByte();
                    Textures[k].Unknown3 = br.ReadByte();

                    // Do the parameters
                    Textures[k].CoordTransf = (byte)(parameters & 14);
                    Textures[k].Color0 = (byte)((parameters >> 13) & 1);
                    Textures[k].Format = (byte)((parameters >> 10) & 7);
                    Textures[k].Height = (byte)(8 << ((parameters >> 7) & 7));
                    Textures[k].Width = (byte)(8 << ((parameters >> 4) & 7));
                    Textures[k].FlipY = (byte)((parameters >> 3) & 1);
                    Textures[k].FlipX = (byte)((parameters >> 2) & 1);
                    Textures[k].RepeatY = (byte)((parameters >> 1) & 1);
                    Textures[k].RepeatX = (byte)(parameters & 1);

                    // Not sure why, but this is a thing
                    if (Textures[k].Width == 0)
                    {
                        if ((Textures[k].Unknown & 3) == 2)
                            Textures[k].Width = 0x200;
                        else
                            Textures[k].Height = 0x100;
                    }

                    if (Textures[k].Height == 0)
                    {
                        if (((Textures[k].Unknown >> 4) & 3) == 2)
                            Textures[k].Height = 0x200;
                        else
                            Textures[k].Height = 0x100;
                    }

                    // Do some stuff with the format
                    Textures[k].BitDepth = FormatBitDepth[Textures[k].Format];
                    if (Textures[k].Format == 5)
                    {
                        // Compressed
                        Textures[k].Compressed = true;
                        Textures[k].Offset += CompressedTextureDataOffset;
                    }
                    else
                    {
                        Textures[k].Offset += TextureDataOffset;
                    }

                    //if (texInfo.Width << 3 != texInfo.Width2) throw new Exception("Uh-oh!\nWidth: " + (texInfo.Width << 3) + "\nWidth2: " + texInfo.Width2);
                }

                // Names
                for (int n = 0; n < objectCount; n++)
                {
                    Textures[n].Name = br.ReadString(16); //Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                    //Textures[n].TileData = null; // And tile data ;)
                }
            }
            #endregion

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

                Palettes = new Palette[objectCount];
                for (int j = 0; j < objectCount; j++)
                {
                    Palettes[j].UnknownBlock = new ushort[2];
                    Palettes[j].UnknownBlock[0] = br.ReadUInt16();
                    Palettes[j].UnknownBlock[1] = br.ReadUInt16();
                }

                // Palette info block
                if (br.ReadUInt16() != 4) throw new Exception("Bad palette block size!");
                //throw new Exception("Bad palette block size " + br.ReadUInt16());
                ushort dataSize = br.ReadUInt16(); // not sure if we'll ever want this

                //uint lastPalOffset = (uint)br.BaseStream.Position;
                for (int i = 0; i < objectCount; i++)
                {
                    Palettes[i].Offset = (uint)(br.ReadUInt16() << 3) + PaletteDataOffset;
                    Palettes[i].Unknown = br.ReadUInt16();
                }

                // Read name block
                for (int n = 0; n < objectCount; n++)
                {
                    Palettes[n].Name = br.ReadString(16); //Encoding.UTF8.GetString(br.ReadBytes(16)).Replace("\0", "");
                }
            }
            #endregion

            // --------------------------------------------
            // Load the tile data
            // --------------------------------------------
            for (int t = 0; t < Textures.Length; t++)
            {
                // Go to the texture's tile data
                br.BaseStream.Seek(Textures[t].Offset, SeekOrigin.Begin);

                // Read the data (width * height * bpp / 8)
                var size = Textures[t].Width * Textures[t].Height * Textures[t].BitDepth / 8;
                Textures[t].TileData = br.ReadBytes(size);

                // Compressed textures have another format
                if (Textures[t].Format == 5)
                {
                    //stream.Seek(spdataoffset + (mat.texoffset - sptexoffset) / 2, SeekOrigin.Begin);
                    br.BaseStream.Seek(CompressedTextureDataOffset + (Textures[t].Offset - CompressedTextureInfoOffset) / 2, SeekOrigin.Begin);

                    Textures[t].CompressedData = br.ReadBytes(size / 2);
                }
            }

            // --------------------------------------------
            // Load the palette data
            // --------------------------------------------
            br.BaseStream.Seek(PaletteDataOffset, SeekOrigin.Begin);

            // Sort palette offsets to calculate sizes
            var paletteOffsets = new List<uint>();
            for (int i = 0; i < Palettes.Length; i++)
            {
                if (!paletteOffsets.Contains(Palettes[i].Offset))
                    paletteOffsets.Add(Palettes[i].Offset);
            }

            paletteOffsets.Add(PaletteDataOffset + PaletteDataSize);
            paletteOffsets.Sort();

            // Calculate each palette's size
            for (int i = 0; i < Palettes.Length; i++)
            {
                // Offset of next palette
                var j = paletteOffsets.IndexOf(Palettes[i].Offset) + 1;

                Palettes[i].Data = new Color[paletteOffsets[j] - Palettes[i].Offset];
            }

            // Load the palettes
            for (int i = 0; i < Palettes.Length; i++)
            {
                try
                {
                    br.BaseStream.Seek(Palettes[i].Offset, SeekOrigin.Begin);

                    for (int j = 0; j < Palettes[i].Data.Length; j++)
                        Palettes[i].Data[j] = br.ReadColor();
                }
                catch { }
            }
        }

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

        public Color[] PaletteData;

        public Texture[] Textures;
        public Palette[] Palettes;

        public struct Texture
        {
            public ushort[] UnknownBlock;

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
            public byte[] CompressedData;
        }

        public struct Palette
        {
            public ushort[] UnknownBlock;
            public uint Offset;
            public ushort Unknown;
            public string Name;

            public Color[] Data;
        }
    }

    public class GlTexture : IDisposable
    {
        int id;
        bool disposed = false;

        public GlTexture(ref TextureSet.Texture tex, ref TextureSet.Palette pal)
        {
            // generate and bind a new OpenGL texture
            id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            // set texture data
            var buffer = TextureDrawing.DrawTexture(ref tex, ref pal);

            // set some nice paramters for this texture
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.Repeat);
        }

        ~GlTexture()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (disposed)
                return;

            GL.DeleteTexture(id);
            disposed = true;
        }

    }

    static class TextureDrawing
    {
        // returns raw RGB pixel data for a texture
        public static Color[] DrawTexture(ref TextureSet.Texture tex, ref TextureSet.Palette pal)
        {
            var buffer = new Color[tex.Width * tex.Height];
            var data = tex.TileData;

            if (tex.Format == 2)    // TODO: remove
                data = BytesToBit2(data);
            if (tex.Format == 3)
                data = BytesToBit4(data);

            for (int y = 0; y < tex.Height; y++)
            {
                for (int x = 0; x < tex.Width; x++)
                {
                    Color color = Color.Red;
                    switch (tex.Format)
                    {
                        case 1:     // alpha3 index5
                            {
                                var index = data[x + y * tex.Width] & 0x1F;
                                var alpha = data[x + y * tex.Width] >> 5;
                                alpha = (alpha * 4 + alpha / 2) * 8;

                                color = Color.FromArgb(alpha, pal.Data[index]);
                            }
                            break;

                        case 2:
                        case 3:
                        case 4:
                            {
                                var index = data[x + y * tex.Width];
                                color = pal.Data[index];
                            }
                            break;

                        case 6:
                            {
                                var index = data[x + y * tex.Width] & 7;
                                var alpha = data[x + y * tex.Width] >> 3;
                                alpha <<= 3;

                                color = Color.FromArgb(alpha, pal.Data[index]);
                            }
                            break;
                    }
                    buffer[x + y * tex.Width] = color;
                }
            }
            return buffer;
        }

        public static Bitmap DrawTextureBitmap(ref TextureSet.Texture tex, ref TextureSet.Palette pal)
        {
            var bmp = new Bitmap(tex.Width, tex.Height);
            var colors = DrawTexture(ref tex, ref pal);

            for (int i = 0; i < colors.Length; i++)
                bmp.SetPixel(i % tex.Width, i / tex.Width, colors[i]);

            return bmp;
        }

        private static byte[] BytesToBit2(byte[] data)
        {
            var bit2 = new List<byte>();

            for (int i = 0; i < data.Length; i++)
            {
                bit2.Add((byte)(data[i] & 0x3));
                bit2.Add((byte)((data[i] >> 2) & 0x3));
                bit2.Add((byte)((data[i] >> 4) & 0x3));
                bit2.Add((byte)((data[i] >> 6) & 0x3));
            }

            return bit2.ToArray();
        }

        private static byte[] BytesToBit4(byte[] data)
        {
            var bit4 = new byte[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                bit4[i * 2] = (byte)(data[i] & 0xF);
                bit4[i * 2 + 1] = (byte)((data[i] >> 4) & 0xF);
            }
            return bit4;
        }
    }
}
