using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpikeSoft.UtilityManager
{
    public class StructMan<T>
    {
        protected List<T> ObjTable;

        /// <summary>
        /// Create all data structs in order from File
        /// </summary>
        /// <param name="filePath">File path with only same Struct data type</param>
        public StructMan(string filePath)
        {
            ObjTable = DataMan.GetStructListFromFile<T>(filePath, 0);
        }

        /// <summary>
        /// Creates single data struct contained inside file
        /// </summary>
        /// <param name="filePath">File path that contains the data</param>
        /// <param name="index">Start Index of Data</param>
        public StructMan(string filePath, int index)
        {
            ObjTable = new List<T>();
            ObjTable.Add(DataMan.GetStructFromFile<T>(filePath, index));
        }

        /// <summary>
        /// Create multiple data structs contained inside file
        /// </summary>
        /// <param name="filePath">File path that contains the data</param>
        /// <param name="index">Start index of data</param>
        /// <param name="count">Struct count</param>
        public StructMan(string filePath, int index, int count)
        {
            ObjTable = new List<T>();
            for (int i = 0; i < count; i++)
            {
                ObjTable.Add(DataMan.GetStructFromFile<T>(filePath, index));
                index += Marshal.SizeOf(typeof(T));
            }
        }

        public T this[int n]
        {
            get { DataMan.ValidateIndex(n, ObjTable); return ObjTable[n]; }
            set { DataMan.ValidateIndex(n, ObjTable); ObjTable[n] = value; }
        }

        public int Count
        {
            get { return ObjTable.Count; }
        }

        public void IUpdateTableItemFromTmp(int n)
        {
            DataMan.UpdateTableItemFromTmp(n, ObjTable);
        }
    }
}
