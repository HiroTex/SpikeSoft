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

        public CharaInfoDataHandler(string filePath)
        {
            CharaInfoTable = DataMan.GetStructListFromFile<CharacterInfoTable.CharacterInfo>(filePath, 0);
        }

        public CharacterInfoTable.CharacterInfo this[int n]
        {
            get { DataMan.ValidateIndex(n, CharaInfoTable); return CharaInfoTable[n]; }
            set { DataMan.ValidateIndex(n, CharaInfoTable); CharaInfoTable[n] = value; }
        }

        public int GetTotalItems()
        {
            return CharaInfoTable.Count;
        }

        public void IUpdateTableItemFromTmp(int n)
        {
            DataMan.UpdateTableItemFromTmp(n, CharaInfoTable);
        }
    }
}
