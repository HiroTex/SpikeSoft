using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpikeSoft.DataTypes;
using SpikeSoft.DataTypes.ZS3;
using SpikeSoft.FileManager;
using System.Runtime.InteropServices;
using System.IO;

namespace SpikeSoft.GUI.ZS3.TourUnlockables
{
    class TourUnlockablesDataHandler
    {
        private List<TourUnlockablesTable.TourUnlockables> TourUnlockablesTable;
        public TourUnlockablesDataHandler(string filePath)
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
