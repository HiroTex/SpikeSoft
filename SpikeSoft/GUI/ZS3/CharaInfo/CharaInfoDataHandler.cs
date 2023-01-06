using SpikeSoft.DataTypes;
using SpikeSoft.DataTypes.ZS3;
using SpikeSoft.FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace SpikeSoft.GUI.ZS3.CharaInfo
{
    class CharaInfoDataHandler
    {
        private List<CharacterInfoTable.CharacterInfo> CharaInfoTable;

        public CharacterInfoTable.CharacterInfo this[int n]
        {
            get { ValidateIndex(n); return CharaInfoTable[n]; }
            set { ValidateIndex(n); CharaInfoTable[n] = value; }
        }

        public int GetTotalItems()
        {
            return CharaInfoTable.Count;
        }

        public void InitializeTable(string filePath)
        {
            // Initialize List
            CharaInfoTable = new List<CharacterInfoTable.CharacterInfo>();

            // Structure Byte Array Size
            int CharaInfoSize = Marshal.SizeOf(typeof(CharacterInfoTable.CharacterInfo));

            // Total File Length
            int FileLength = File.ReadAllBytes(filePath).Length;

            // Iterate through File to Obtain all Character Info Structs
            for (var CurrentIndex = 0; FileLength > CurrentIndex; CurrentIndex = CharaInfoTable.Count * CharaInfoSize)
            {
                CharaInfoTable.Add((CharacterInfoTable.CharacterInfo)DataMan.GetStructFromFile(filePath, CurrentIndex, typeof(CharacterInfoTable.CharacterInfo)));
            }
        }

        public void UpdateTableItemFromTmp(int n)
        {
            ValidateIndex(n);
            int Index = n * Marshal.SizeOf(typeof(CharacterInfoTable.CharacterInfo));
            CharaInfoTable[n] = (CharacterInfoTable.CharacterInfo)DataMan.GetStructFromFile(TmpMan.GetDefaultTmpFile(), Index, typeof(CharacterInfoTable.CharacterInfo));
        }

        private void ValidateIndex(int n)
        {
            if (CharaInfoTable == null)
            {
                throw new ArgumentNullException("CharInfoTable");
            }
            if (n >= CharaInfoTable.Count)
            {
                throw new IndexOutOfRangeException("Character ID out of Bounds");
            }
        }
    }
}
