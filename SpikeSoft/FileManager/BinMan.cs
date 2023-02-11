using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SpikeSoft.FileManager
{
    public static class BinMan
    {
        static public byte[] GetBytes(string filePath, int objSize, int index)
        {
            byte[] obj = new byte[objSize];
            using (var f = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var b = new BinaryReader(f))
            {
                f.Seek(index, SeekOrigin.Begin);
                obj = b.ReadBytes(objSize);
            }
            return obj;
        }

        public static void SetBytes(string filePath, byte[] source, int index)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            using (var f = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            using (var b = new BinaryWriter(f))
            {
                f.Seek(index, SeekOrigin.Begin);
                b.Write(source);
            }
        }

        /// <summary>
        /// Gets Int32 Value with Endianness Parsed by Settings Automatically from a determined Offset in a File.
        /// </summary>
        /// <param name="filePath">Path to Binary File</param>
        /// <param name="dataOffset">Offset to Data</param>
        /// <returns></returns>
        public static int GetBinaryData_Int32(string filePath, int dataOffset)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var bin = new BinaryReader(fs))
            {
                fs.Seek(dataOffset, SeekOrigin.Begin);
                byte[] tmp = bin.ReadBytes(4);
                if (Properties.Settings.Default.WIIMODE) Array.Reverse(tmp);
                int result = BitConverter.ToInt32(tmp, 0);
                return result;
            }
        }

        /// <summary>
        /// Gets Int32 Value with Endianness Parsed by Settings Automatically from a determined Offset in a Byte Array.
        /// </summary>
        /// <param name="source">Byte Array with Data</param>
        /// <param name="dataOffset">Data Offset</param>
        /// <returns></returns>
        public static int GetBinaryData_Int32(byte[] source, int dataOffset)
        {
            var tmp = new byte[4];
            Array.Copy(source, dataOffset, tmp, 0, 4);
            if (Properties.Settings.Default.WIIMODE) Array.Reverse(tmp);
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Gets Null Ending String from a determined Offset in a Byte Array
        /// </summary>
        /// <param name="source">Byte Array with Data</param>
        /// <param name="dataOffset">Data Offset</param>
        /// <returns></returns>
        public static string GetBinaryData_String(byte[] source, int dataOffset)
        {
            var sLenght = 0;
            var counter = 0;
            while (dataOffset + counter < source.Length && source[dataOffset + counter++] != 0)
            {
                sLenght++;
            }

            var tmp = new byte[sLenght];
            Array.Copy(source, dataOffset, tmp, 0, tmp.Length);
            return Encoding.ASCII.GetString(tmp);
        }
    }
}
