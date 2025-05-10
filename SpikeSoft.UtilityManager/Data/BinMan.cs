using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.UtilityManager
{
    public class BinMan
    {
        /// <summary>
        /// Get Generic Data as Byte Array from File at Specified Offset
        /// </summary>
        /// <param name="filePath">Path to File</param>
        /// <param name="objSize">Array Size</param>
        /// <param name="index">Offset of Binary File where Data is located</param>
        /// <returns></returns>
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

        /// <summary>
        /// Set Generic Data as Byte Array on File at Specified Offset
        /// </summary>
        /// <param name="filePath">Path to Binary File</param>
        /// <param name="source">Byte Array to Insert on File</param>
        /// <param name="index">Specified Offset of Binary File</param>
        public static void SetBytes(string filePath, byte[] source, int index)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            using (var f = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            using (var b = new BinaryWriter(f))
            {
                if (index > f.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                f.Seek(index, SeekOrigin.Begin);
                b.Write(source);
            }
        }

        /// <summary>
        /// Get Binary Data of Specified Type from File
        /// </summary>
        /// <typeparam name="T">Type of Data to Get</typeparam>
        /// <param name="filePath">File Path</param>
        /// <param name="offset">Data Offset</param>
        /// <returns></returns>
        public static T GetBinaryData<T>(string filePath, int offset) where T : struct
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return ReadBinaryData<T>(fs, offset);
            }
        }

        /// <summary>
        /// Get Binary Data of Specified Type from Object
        /// </summary>
        /// <typeparam name="T">Type of Data to Get</typeparam>
        /// <param name="data">Object</param>
        /// <param name="offset">Data Offset</param>
        /// <returns></returns>
        public static T GetBinaryData<T>(byte[] data, int offset) where T : struct
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return ReadBinaryData<T>(ms, offset);
            }
        }

        private static T ReadBinaryData<T>(Stream stream, int offset) where T : struct
        {
            if (offset > stream.Length)
            {
                throw new Exception($"Offset OOB: {offset}");
            }

            stream.Seek(offset, SeekOrigin.Begin);

            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[size];
            stream.Read(buffer, 0, size);

            if (Properties.Settings.Default.WIIMODE && size > 1)
            {
                Array.Reverse(buffer);
            }

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
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
            Array.Copy(source, tmp, sLenght);
            if (Properties.Settings.Default.WIIMODE) Array.Reverse(tmp);
            return Encoding.ASCII.GetString(tmp);
        }
    }
}
