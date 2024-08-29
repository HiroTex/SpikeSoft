using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;

namespace SpikeSoft.DataTypes.Common
{
    class PCK : PAK, IPak
    {
        public int VERSION { get; set; }

        public override bool Repack(IProgress<ProgressInfo> progress)
        {
            return true;
        }

        public override void InitializeSubFileCount(string filePath)
        {
            VERSION = BinMan.GetBinaryData<int>(filePath, 0x4);
            FileCount = BinMan.GetBinaryData<int>(filePath, 0x8);
        }

        public override void InitializeFilePointersList(string filePath)
        {
            FilePointers = new List<int>();
            int total = BinMan.GetBinaryData<int>(filePath, 0x8);
            for (int i = 0; i < total; i++)
            {
                FilePointers.Add(BinMan.GetBinaryData<int>(filePath, i * 4 + 0x10));
            }
        }

        public override void InitializeEndOfFilePointer(string filePath)
        {
            EOF = File.ReadAllBytes(filePath).Length;
        }

        public override void InitializeFilenamesList(string filePath, int subFileCount)
        {
            FileNames = new List<string>();
            int ModelCount = 1;
            string format = "D2";
            if (subFileCount.ToString().Length > 2)
                format = "D" + subFileCount.ToString().Length;
            for (int i = 0; i < subFileCount; i++)
            {
                string basename = $"_Model_{ModelCount}";
                string extension = ".dbt";
                if (i % 3 == 1) extension = ".mdl";
                if (i % 3 == 2) extension = ".anm";
                FileNames.Add((i + 1).ToString(format) + basename + extension);
                ModelCount = ((i + 1) / 3) + 1;
            }
        }
    }
}
