using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost
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

        public Matrix(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                // Load the matrix header
                _width = br.ReadByte();
                _height = br.ReadByte();
                _hasLayer2 = (br.ReadByte() == 1);
                _hasLayer3 = (br.ReadByte() == 1);

                byte nameLen = br.ReadByte();
                _name = br.ReadString(nameLen); //Encoding.UTF8.GetString(br.ReadBytes(nameLen));

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

        public byte[] Save()
        {
            // neededBytes:
            // 5 + name.Length + (2 * width * height) + (width * height) + (2 * width * height)
            // last two only if layers present

            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
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

                return ms.ToArray();
            }
        }

        public static string[] LoadNames(Archive arc)
        {
            string[] names = new string[arc.FileCount];
            for (int i = 0; i < arc.FileCount; i++)
            {
                // get contents of file from Archive
                var matrix = arc.GetFile(i);

                // get length of name
                var len = matrix[4];
                var sb = new StringBuilder(len);

                // get name
                for (int n = 0; n < len; n++)
                {
                    sb.Append((char)matrix[5 + n]);
                }

                names[i] = sb.ToString();
            }
            return names;
        }

        public static Dictionary<int, List<int>> LoadHeaderMapMatches(Archive arc, Dictionary<int, int> headerMatrixMatches)
        {
            var headerMapMatches = new Dictionary<int, List<int>>();
            foreach (int header in headerMatrixMatches.Keys)
            {
                // Load the matrix
                var matrixId = headerMatrixMatches[header];
                var matrix = new Matrix(arc.GetFileStream(matrixId));

                // Create a new list of maps for this header
                if (!headerMapMatches.ContainsKey(header))
                    headerMapMatches.Add(header, new List<int>());

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
