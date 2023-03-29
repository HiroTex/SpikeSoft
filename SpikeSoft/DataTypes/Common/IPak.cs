using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes.Common
{
    interface IPak
    {
        string FilePath { get; set; }
        string UnpackedDir { get; set; }
        int FileCount { get; set; }
        List<int> FilePointers { get; set; }
        List<string> FileNames { get; set; }
        int EOF { get; set; }
        bool ZBPE { get; set; }

        bool Unpack();

        bool Repack();

        void WriteInfo(string dir, string type);

        void InitializeSubFileCount(string filePath);

        void InitializeFilePointersList(string filePath);

        void InitializeEndOfFilePointer(string filePath);

        void InitializeFilenamesList(string filePath, int subFileCount);

        List<string> GenerateDefaultFilenamesList(string filePath, int subFileCount);
    }
}
