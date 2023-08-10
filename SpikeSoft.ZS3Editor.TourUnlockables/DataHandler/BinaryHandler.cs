using System.Collections.Generic;
using SpikeSoft.UtilityManager;

namespace SpikeSoft.ZS3Editor.TourUnlockables.DataHandler
{
    class BinaryHandler
    {
        private List<TourUnlockablesTable.TourUnlockables> TourUnlockablesTable;
        public BinaryHandler(string filePath)
        {
            TourUnlockablesTable = DataMan.GetStructListFromFile<TourUnlockablesTable.TourUnlockables>(filePath, 0);
        }

        public TourUnlockablesTable.TourUnlockables this[int n]
        {
            get { DataMan.ValidateIndex(n, TourUnlockablesTable); return TourUnlockablesTable[n]; }
            set { DataMan.ValidateIndex(n, TourUnlockablesTable); TourUnlockablesTable[n] = value; }
        }

        public void IUpdateTableItemFromTmp(int n)
        {
            DataMan.UpdateTableItemFromTmp(n, TourUnlockablesTable);
        }
    }
}
