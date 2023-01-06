using SpikeSoft.FileManager;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SpikeSoft.DataTypes.ZS3
{
    class CharacterInfoTable
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CharacterInfo
        {
            public int Initial_HP;
            public int Initial_KI;
            public int Max_Ki;
            public int Max_Blast_Units;
        }
    }
}
