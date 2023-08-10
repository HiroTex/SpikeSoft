using SpikeSoft.ZS3Editor.CharaInfo.DataInfo;
using System.Collections.Generic;
using SpikeSoft.UtilityManager;

namespace SpikeSoft.ZS3Editor.CharaInfo.DataHandler
{
    class BinaryHandler
    {
        private List<CharaInfoObj.CharacterInfo> CharaInfoTable;

        public BinaryHandler(string filePath)
        {
            CharaInfoTable = DataMan.GetStructListFromFile<CharaInfoObj.CharacterInfo>(filePath, 0);
        }

        public CharaInfoObj.CharacterInfo this[int n]
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
