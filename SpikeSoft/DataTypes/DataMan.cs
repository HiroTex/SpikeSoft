using SpikeSoft.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes
{
    static public class DataMan
    {

        /// <summary>
        /// Get Struct Object from specific Index on File
        /// </summary>
        /// <param name="filePath">Complete Path to File</param>
        /// <param name="index">Byte Position where Object is located</param>
        /// <param name="type">Type of Struct</param>
        /// <returns></returns>
        public static object GetStructFromFile(string filePath, int index, Type type)
        {
            return DataMan.DataToStruct(BinMan.GetBytes(filePath, Marshal.SizeOf(type), index), type);
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
            if (Properties.Settings.Default.WIIMODE || (!BitConverter.IsLittleEndian))
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
