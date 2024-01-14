using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.GenericItemList
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GenericHdList
    {
        public int itemCount;
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 12)]
        public byte[] padding;
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
        public int[] items;
    }
}
