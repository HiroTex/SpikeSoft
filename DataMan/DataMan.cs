using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace SpikeSoft.UtilityManager
{
    public class DataMan
    {
        /// <summary>
        /// Updates Object in List with current stored Temporal Data
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="n">Object Index</param>
        /// <param name="list">Object List</param>
        public static void UpdateTableItemFromTmp<T>(int n, List<T> list)
        {
            ValidateIndex(n, list);
            int Index = n * Marshal.SizeOf(typeof(T));
            list[n] = (T)GetStructFromFile(TmpMan.GetDefaultTmpFile(), Index, typeof(T));
        }

        /// <summary>
        /// Validates Index supplied for List of Objects and Throws an exception if wrong parameter or index
        /// </summary>
        /// <typeparam name="T">Type of Objects on List</typeparam>
        /// <param name="n">Object Index</param>
        /// <param name="list">List of Objects</param>
        public static void ValidateIndex<T>(int n, List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException($"{list}");
            }
            if (n >= list.Count || n < 0)
            {
                throw new IndexOutOfRangeException("Character ID out of Bounds");
            }
        }

        /// <summary>
        /// Get List of Objects of a specified Struct Type from a File
        /// </summary>
        /// <typeparam name="T">Type of Struct Object</typeparam>
        /// <param name="filePath">Full Path to File</param>
        /// <param name="StartIndex">Byte Position where Objects are located</param>
        /// <returns></returns>
        public static List<T> GetStructListFromFile<T>(string filePath, int StartIndex)
        {
            // Initialize List
            var ObjectTable = new List<T>();

            // Structure Byte Array Size
            int ObjSize = Marshal.SizeOf(typeof(T));

            // Total File Length
            int FileLength = File.ReadAllBytes(filePath).Length;

            // Iterate through File to Obtain all Character Info Structs
            for (var CurrentIndex = StartIndex; FileLength > CurrentIndex; CurrentIndex = StartIndex + (ObjectTable.Count * ObjSize))
            {
                if (FileLength <= CurrentIndex + ObjSize)
                {
                    break;
                }

                ObjectTable.Add((T)GetStructFromFile(filePath, CurrentIndex, typeof(T)));
            }

            return ObjectTable;
        }

        /// <summary>
        /// Get Struct Object from specific Index on File
        /// </summary>
        /// <param name="filePath">Complete Path to File</param>
        /// <param name="index">Byte Position where Object is located</param>
        /// <param name="type">Type of Struct</param>
        /// <returns></returns>
        public static object GetStructFromFile(string filePath, int index, Type type)
        {
            return DataToStruct(BinMan.GetBytes(filePath, Marshal.SizeOf(type), index), type);
        }

        /// <summary>
        /// Converts a Data Array into a Struct of a Defined Type
        /// </summary>
        /// <param name="data">Object to Convert</param>
        /// <param name="type">Type of Struct to get Object</param>
        /// <returns></returns>
        public static object DataToStruct(byte[] data, Type type)
        {
            data = ParseStructEndianness(data, type);
            object str = Activator.CreateInstance(type);
            int size = Marshal.SizeOf(str);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(data, 0, ptr, size);
                str = Marshal.PtrToStructure(ptr, str.GetType());
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return str;
        }

        /// <summary>
        /// Converts Struct object to Data Array
        /// </summary>
        /// <param name="str">Struct Object with Values to convert to Array</param>
        /// <returns></returns>
        public static byte[] StructToData(object str)
        {
            int size = Marshal.SizeOf(str);
            byte[] data = new byte[size];
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(str, ptr, true);
                Marshal.Copy(ptr, data, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return ParseStructEndianness(data, str.GetType());
        }

        /// <summary>
        /// Parses Data as Big Endian if Wii Mode is Enabled or if System's Endianness is reversed
        /// </summary>
        /// <param name="data">Data belonging to a particular struct</param>
        /// <param name="type">Struct Type</param>
        /// <returns></returns>
        public static byte[] ParseStructEndianness(byte[] data, Type type)
        {
            if (SpikeSoft.UtilityManager.Properties.Settings.Default.WIIMODE || (!BitConverter.IsLittleEndian))
            {
                foreach (var field in type.GetFields())
                {
                    if (field.IsStatic)
                    {
                        // Do not Swap Static Values
                        continue;
                    }

                    var fieldType = field.FieldType;
                    var offset = Marshal.OffsetOf(type, field.Name);
                    if (fieldType.IsEnum)
                    {
                        fieldType = Enum.GetUnderlyingType(fieldType);
                    }

                    Array.Reverse(data, (int)offset, Marshal.SizeOf(fieldType));
                }
            }

            return data;
        }
    }
}
