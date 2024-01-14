using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
            list[n] = GetStructFromFile<T>(TmpMan.GetDefaultTmpFile(), Index);
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

                ObjectTable.Add(GetStructFromFile<T>(filePath, CurrentIndex));
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
        public static T GetStructFromFile<T>(string filePath, int index)
        {
            return DataToStruct<T>(BinMan.GetBytes(filePath, Marshal.SizeOf(typeof(T)), index));
        }

        /// <summary>
        /// Converts a Data Array into a Struct of a Defined Type
        /// </summary>
        /// <param name="data">Object to Convert</param>
        /// <param name="type">Type of Struct to get Object</param>
        /// <returns></returns>
        public static T DataToStruct<T>(byte[] data)
        {
            var type = typeof(T);
            data = ParseStructEndianness(data, type);
            var str = Activator.CreateInstance(type);
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
            return (T)str;
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
        /// Converts struct that contains dynamic data fields (arrays and/or strings) to Binary format
        /// </summary>
        /// <typeparam name="T">Struct Type</typeparam>
        /// <param name="structInstance">Struct Instance to convert to Data</param>
        /// <returns>Data in Binary format</returns>
        public byte[] DynamicStructToData<T>(T structInstance) where T : struct
        {
            int size = CalculateSize(structInstance);
            byte[] output = new byte[size];

            // Allocate memory for the struct
            IntPtr structPtr = Marshal.AllocHGlobal(size);

            try
            {
                // Manually copy each field's data to the allocated memory
                int offset = 0;

                foreach (var field in typeof(T).GetFields())
                {
                    if (field.FieldType.IsArray)
                    {
                        int arrayLength = ((Array)field.GetValue(structInstance))?.Length ?? 0;
                        int elementSize = Marshal.SizeOf(field.FieldType.GetElementType());

                        IntPtr arrayPtr = (IntPtr)((long)structPtr + offset);

                        for (int i = 0; i < arrayLength; i++)
                        {
                            object arrayElementValue = ((Array)field.GetValue(structInstance)).GetValue(i);
                            Marshal.StructureToPtr(arrayElementValue, arrayPtr, false);
                            arrayPtr = (IntPtr)((long)arrayPtr + elementSize);
                        }

                        offset += arrayLength * elementSize;
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        string stringValue = (string)field.GetValue(structInstance);
                        int stringLength = stringValue?.Length ?? 0;

                        IntPtr stringPtr = (IntPtr)((long)structPtr + offset);

                        for (int i = 0; i < stringLength; i++)
                        {
                            byte charValue = Convert.ToByte(stringValue[i]);
                            Marshal.StructureToPtr(charValue, stringPtr, false);
                            stringPtr = (IntPtr)((long)stringPtr + sizeof(char));
                        }

                        offset += stringLength;
                    }
                    else
                    {
                        object fieldValue = field.GetValue(structInstance);
                        Marshal.StructureToPtr(fieldValue, (IntPtr)((long)structPtr + offset), false);
                        offset += Marshal.SizeOf(field.FieldType);
                    }
                }
            }
            finally
            {
                // Copy the Data and Free the memory
                Marshal.Copy(structPtr, output, 0, size);
                Marshal.FreeHGlobal(structPtr);
            }

            return output;
        }

        /// <summary>
        /// Calculate Size in Bytes of Struct Instance
        /// </summary>
        /// <typeparam name="T">Type of Struct</typeparam>
        /// <param name="structInstance">Instance of Struct to calculate overall size in bytes</param>
        /// <returns>Size in Bytes of Struct Instance</returns>
        private int CalculateSize<T>(T structInstance) where T : struct
        {
            int sizeOfFixedPart = 0;
            foreach (var field in typeof(T).GetFields())
            {
                if (!field.FieldType.IsArray && field.FieldType != typeof(string))
                {
                    // Assuming all non-array, non-string fields are part of the fixed section
                    sizeOfFixedPart += System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType);
                }
            }

            int sizeOfInstance = sizeOfFixedPart;

            foreach (var field in typeof(T).GetFields())
            {
                if (field.FieldType.IsArray)
                {
                    // Handle variable-sized array
                    int arrayLength = ((Array)field.GetValue(structInstance))?.Length ?? 0;
                    sizeOfInstance += arrayLength * System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType.GetElementType());
                }
                else if (field.FieldType == typeof(string))
                {
                    // Handle string
                    string stringValue = (string)field.GetValue(structInstance);
                    int stringLength = stringValue?.Length ?? 0;
                    sizeOfInstance += stringLength;
                }
            }

            return sizeOfInstance;
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

        /// <summary>
        /// Generates List of Structs of a determined Type from Excel Data made with XlsxMan
        /// </summary>
        /// <typeparam name="T">Type of Structs to Create</typeparam>
        /// <param name="excelData">Excel Data generated with XlsxMan</param>
        /// <returns>List of Structs with Data</returns>
        public static List<T> FillStructsFromXlsx<T>(Dictionary<string, Dictionary<string, string>> excelData) where T : struct
        {
            var result = new List<T>();

            foreach (var source in excelData)
            {
                T obj = new T();

                foreach (var rowData in source.Value)
                {
                    obj = PopulateStructFields(obj, rowData.Key, rowData.Value);
                }

                result.Add(obj);
            }

            return result;
        }

        /// <summary>
        /// Fills struct fields with data from Xlsx Cell
        /// </summary>
        /// <typeparam name="T">Type of Struct</typeparam>
        /// <param name="target">Struct to fill</param>
        /// <param name="columnName">Name of Field of Struct to Set Value</param>
        /// <param name="cellValue">Cell Data to fill on Struct Field</param>
        /// <returns>Modified Struct</returns>
        private static T PopulateStructFields<T>(T target, string columnName, string cellValue)
        {
            TypedReference reference = __makeref(target);
            var targetType = target.GetType();
            var parts = columnName.Split('.');

            var arrayIndex = 0;
            var fieldName = columnName;

            if (columnName.Contains("["))
            {
                var fieldParts = columnName.Split('[', ']');
                fieldName = fieldParts[0];
                arrayIndex = int.Parse(fieldParts[1]);
            }

            var field = targetType.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);

            if (field != null)
            {
                Type fieldType = field.FieldType;

                // Check if the field is a nested struct
                if (fieldType.IsValueType && !fieldType.IsPrimitive && !fieldType.IsEnum)
                {
                    object subTarget = field.GetValue(target);
                    subTarget = PopulateStructFields(subTarget, columnName.Substring(columnName.IndexOf('.') + 1), cellValue);
                    field.SetValueDirect(reference, subTarget);
                }
                else if (fieldType.IsArray)
                {
                    // Handle arrays
                    Array array = (Array)field.GetValue(target);

                    if (array == null)
                    {
                        // Get the size of the array from SizeConst attribute
                        var sizeConstAttribute = field.GetCustomAttribute<MarshalAsAttribute>();
                        if (sizeConstAttribute != null)
                        {
                            int size = sizeConstAttribute.SizeConst;
                            array = Array.CreateInstance(fieldType.GetElementType(), size);
                        }
                        else
                        {
                            // Handle uninitialized arrays
                            int defaultSize = 1; // You can set a default size as needed
                            array = Array.CreateInstance(fieldType.GetElementType(), defaultSize);
                        }
                    }

                    if (parts.Length > 1)
                    {
                        // Split subFieldName into field name and array index
                        string subFieldName = parts[1];
                        string[] subFieldParts = subFieldName.Split('[', ']');

                        if (subFieldParts.Length > 1)
                        {
                            string subArrayFieldName = subFieldParts[0];
                            int subArrayIndex = int.Parse(subFieldParts[1]);

                            Type arrayElementType = fieldType.GetElementType();
                            FieldInfo subField = arrayElementType.GetField(subArrayFieldName, BindingFlags.Public | BindingFlags.Instance);

                            if (subField != null)
                            {
                                // Handle arrays
                                var mainArray = array.GetValue(arrayIndex);
                                var subarray = (Array)subField.GetValue(mainArray);

                                if (subarray == null)
                                {
                                    // Get the size of the array from SizeConst attribute
                                    var sizeConstAttribute = subField.GetCustomAttribute<MarshalAsAttribute>();
                                    if (sizeConstAttribute != null)
                                    {
                                        int size = sizeConstAttribute.SizeConst;
                                        subarray = Array.CreateInstance(subField.FieldType.GetElementType(), size);
                                    }
                                    else
                                    {
                                        // Handle uninitialized arrays
                                        int defaultSize = 1; // You can set a default size as needed
                                        subarray = Array.CreateInstance(subField.FieldType.GetElementType(), defaultSize);
                                    }
                                }

                                object fieldValue = ConvertValue(cellValue, subField.FieldType.GetElementType());
                                subarray.SetValue(fieldValue, subArrayIndex);
                                TypedReference Areference = __makeref(mainArray);
                                subField.SetValueDirect(Areference, subarray);
                                array.SetValue(mainArray, arrayIndex);
                                field.SetValueDirect(reference, array);
                            }
                        }
                    }
                    else
                    {
                        object fieldValue = ConvertValue(cellValue, fieldType.GetElementType());
                        array.SetValue(fieldValue, arrayIndex);
                        field.SetValueDirect(reference, array);
                    }
                }
                else
                {
                    var fieldValue = ConvertValue(cellValue, fieldType);
                    field.SetValueDirect(reference, fieldValue);
                }
            }
            else
            {
                // Handle nested struct fields
                var subField = targetType.GetField(parts[0], BindingFlags.Public | BindingFlags.Instance);
                var subTarget = subField.GetValue(target);
                subTarget = PopulateStructFields(subTarget, columnName.Substring(columnName.IndexOf('.') + 1), cellValue);
                subField.SetValueDirect(reference, subTarget);
            }

            return target;
        }

        /// <summary>
        /// Utility to convert Enums and other types of data to a certain Type
        /// </summary>
        /// <param name="value">Original Data</param>
        /// <param name="targetType">Type to Convert to</param>
        /// <returns>Converted Data</returns>
        private static object ConvertValue(string value, Type targetType)
        {
            if (targetType.IsEnum)
            {
                if (Enum.IsDefined(targetType, value))
                {
                    return Enum.Parse(targetType, value);
                }
                else
                {
                    // Handle bit flag enums by splitting the values
                    var flags = value.Split(',').Select(flag => flag.Trim());
                    int result = 0;

                    foreach (var flag in flags)
                    {
                        if (Enum.IsDefined(targetType, flag))
                        {
                            var enumValue = Enum.Parse(targetType, flag);
                            result |= (int)Convert.ToUInt32(enumValue);
                        }
                        else
                        {
                            // Handle error or missing flags here
                        }
                    }

                    return Enum.ToObject(targetType, result);
                }
            }
            else if (targetType == typeof(bool))
            {
                return bool.Parse(value);
            }
            else if (targetType == typeof(int))
            {
                return int.Parse(value);
            }
            else if (targetType == typeof(float))
            {
                value = value.Replace(',', '.');
                return float.Parse(value, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(short))
            {
                return short.Parse(value);
            }
            else if (targetType == typeof(byte))
            {
                return byte.Parse(value);
            }
            else
            {
                return value; // Handle other types as needed
            }
        }
    }
}
