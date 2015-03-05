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

        public Matrix(byte[] file)
        {
            using (MemoryStream ms = new MemoryStream(file))
            {
                BinaryReader br = new BinaryReader(ms);

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

                br.Dispose();
            }
        }

        /*public Matrix(byte[] file)
        {
            _width = file[0];
            _height = file[1];
            _hasLayer2 = (file[2] == 1);
            _hasLayer3 = (file[3] == 1);

            byte nameLen = file[4];
            //byte[] nameBuffer = new byte[nameLen];
            //for (int i = 0; i < nameLen; i++) nameBuffer[i] = file[i + 4];
            _name = Encoding.UTF8.GetString(file, 5, nameLen);

            int layer1Start = 5 + nameLen;
            _layer1 = new ushort[_width, _height];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int index = layer1Start + (y * _width) + (x * 2);
                    _layer1[x, y] = (ushort)((file[index + 1] << 8) + file[index]);
                }
            }

            if (_hasLayer2)
            {
                int layer2Start = layer1Start + _width * _height * 2;
                _layer2 = new byte[_width, _height];
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        int index = layer2Start + (y * _width) + x;
                        _layer2[x, y] = file[index];
                    }
                }
            }
            else _layer2 = null;

            if (_hasLayer3)
            {
                int layer3Start = layer1Start + _width * _height * 2;
                if (_hasLayer2) layer3Start += _width * _height;

                _layer3 = new ushort[_width, _height];
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        int index = layer3Start + (y * _width) + (x * 2);
                        _layer3[x, y] = (ushort)((file[index + 1] << 8) + file[index]);
                    }
                }
            }
            else _layer3 = null;
        }*/

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

        public static Dictionary<int, List<int>> LoadHeaderMapMatches(NDS.NARC narc, Dictionary<int, int> headerMatrixMatches)
        {
            Dictionary<int, List<int>> headerMapMatches = new Dictionary<int, List<int>>();
            foreach (int header in headerMatrixMatches.Keys)
            {
                // Load the matrix
                int matrixId = headerMatrixMatches[header];
                Matrix matrix = new Matrix(narc.GetFile(matrixId)); // The MemoryStream approach. Faster.

                // Create a new list of maps for this header
                if (!headerMapMatches.ContainsKey(header)) headerMapMatches.Add(header, new List<int>());

                // Go through the matrix's data
                for (int x = 0; x < matrix._width; x++)
                {
                    for (int y = 0; y < matrix._height; y++)
                    {
                        // There are two types of matrixes that we will deal with
                        // A matrix with layer 3 will always have layer 2
                        // But that doesn't matter anyway, because layer 2's data is worthless
                        // For this kind of operation
                        if (matrix._hasLayer3)
                        {
                            // Check for current header
                            int h = matrix._layer1[x, y];
                            if (h != header) continue;

                            // If it matches... check for a map file
                            int m = matrix._layer3[x, y];
                            if (m == 0xFFFF) continue;
                            if (headerMapMatches[header].Contains(m)) continue;

                            headerMapMatches[header].Add(m);
                        }
                        else
                        {
                            // In a single layer matrix, the only data available will be map files
                            // This is why we needed to load the matrix-header matches
                            int m = matrix._layer1[x, y];
                            if (!headerMapMatches[header].Contains(m)) headerMapMatches[header].Add(m);
                        }
                    }
                }
            }
            return headerMapMatches;
        }

        #region Properties

        public ushort[,] Layer1
        {
            get { return _layer1; }
        }

        public byte[,] Layer2
        {
            get { return _layer2; }
        }

        public ushort[,] Layer3
        {
            get { return _layer3; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        // TODO: Resizing

        public bool HasLayer2
        {
            get { return _hasLayer2; }
            set
            {
                _hasLayer2 = value;
                if (_hasLayer2)
                {
                    _layer2 = new byte[_width, _height];
                    for (int y = 0; y < _height; y++)
                    {
                        for (int x = 0; x < _width; x++)
                        {
                            _layer2[x, y] = 0;
                        }
                    }
                }
                else _layer2 = null;
            }
        }

        public bool HasLayer3
        {
            get { return _hasLayer3; }
            set
            {
                _hasLayer3 = value;
                if (_hasLayer3)
                {
                    _layer3 = new ushort[_width, _height];
                    for (int y = 0; y < _height; y++)
                    {
                        for (int x = 0; x < _width; x++)
                        {
                            _layer3[x, y] = 0;
                        }
                    }
                }
                else _layer3 = null;
            }
        }

        #endregion
    }
}
