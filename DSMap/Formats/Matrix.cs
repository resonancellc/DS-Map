using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSMap.Formats
{
    public class Matrix
    {
        // Data
        private ushort[,] _layer1; // Map Files
        private byte[,] _layer2; // Map Border Heights
        private ushort[,] _layer3; // Map Headers
        
        // Header (in this order)
        private byte _width, _height;
        private bool _hasLayer2, _hasLayer3;
        private string _name;

        public Matrix(string file)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                // Load the matrix header
                _width = br.ReadByte();
                _height = br.ReadByte();
                _hasLayer2 = (br.ReadByte() == 1);
                _hasLayer3 = (br.ReadByte() == 1);

                byte nameLen = br.ReadByte();
                _name = Encoding.UTF8.GetString(br.ReadBytes(nameLen));

                // Load the data
                _layer1 = new ushort[_width, _height];
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        _layer1[x, y] = br.ReadUInt16();
                    }
                }

                if (_hasLayer2)
                {
                    _layer2 = new byte[_width, _height];
                    for (int y = 0; y < _height; y++)
                    {
                        for (int x = 0; x < _width; x++)
                        {
                            _layer2[x, y] = br.ReadByte();
                        }
                    }
                }
                else _layer2 = null;

                if (_hasLayer3)
                {
                    _layer3 = new ushort[_width, _height];
                    for (int y = 0; y < _height; y++)
                    {
                        for (int x = 0; x < _width; x++)
                        {
                            _layer3[x, y] = br.ReadUInt16();
                        }
                    }
                }
                else _layer3 = null;
            }
        }

        public string SaveToTempFile()
        {
            string file = Temporary.GetTemporaryFileName();
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // Header
                bw.Write(_width);
                bw.Write(_height);
                bw.Write((byte)(_hasLayer2 ? 1 : 0));
                bw.Write((byte)(_hasLayer3 ? 1 : 0));

                bw.Write((byte)_name.Length);
                bw.Write(Encoding.UTF8.GetBytes(_name));

                // Layer 1
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        bw.Write(_layer1[x, y]);
                    }
                }

                // Layer 2
                if (_hasLayer2)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        for (int x = 0; x < _width; x++)
                        {
                            bw.Write(_layer2[x, y]);
                        }
                    }
                }

                // Layer 3
                if (_hasLayer3)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        for (int x = 0; x < _width; x++)
                        {
                            bw.Write(_layer3[x, y]);
                        }
                    }
                }
            }
            return file;
        }

        public byte[] Save()
        {
            // This isn't the most efficient way to do it, but it gets the job done.
            // Save it to a temporary file
            string file = SaveToTempFile();

            // Read that data
            byte[] data = File.ReadAllBytes(file);

            // Delete temporary file
            File.Delete(file);

            // Done
            return data;
        }

        public static string[] LoadAllMatrixNames(NDS.NARC narc)
        {
            string[] names = new string[narc.FileCount];
            for (int i = 0; i < narc.FileCount; i++)
            {
                /*using (MemoryStream ms = narc.GetFileMemoryStream(i))
                {
                    BinaryReader br = new BinaryReader(ms);
                    br.BaseStream.Position = 4L;

                    byte len = br.ReadByte();
                    names[i] = Encoding.UTF8.GetString(br.ReadBytes(len));

                    br.Dispose();
                }*/

                // This method using raw byte arrays seems faster
                // And has less of a chance for memory leaks?
                byte[] matrix = narc.GetFile(i);

                byte len = matrix[4]; names[i] = "";
                for (int n = 5; n < len + 5; n++)
                {
                    names[i] += (char)matrix[n];
                }
            }
            return names;
        }
    }
}
