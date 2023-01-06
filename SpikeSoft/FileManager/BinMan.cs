using System;
using System.IO;

namespace SpikeSoft.FileManager
{
    public static class BinMan
    {
        public static int GetInt32(string filePath, int index)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            using (var f = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var b = new BinaryReader(f))
            {
                f.Seek(index, SeekOrigin.Begin);
                return b.ReadInt32(); 
            }
        }

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
    }
}
