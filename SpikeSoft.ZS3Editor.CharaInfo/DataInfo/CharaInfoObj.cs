using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Editor.CharaInfo.DataInfo
{
    class CharaInfoObj
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
