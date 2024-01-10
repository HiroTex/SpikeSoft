using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DBTManager.DataInfo
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 32)]
    public struct DBTHd
    {
        public int ImageCount;
        public int ImageTablePtr;
        public int TotalBufTexSize;
        public int TotalBufCLUTSize;
        public int BufImageTablePtr;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DBTImageHd
    {
        public int TexDataPtr;
        public int PalDataPtr;
        public int TexDataLength;
        public int PalDataLength;
        public int TexSize;
        public int CLUTSize;
        public int TexBufferWidth;
        public int CLUTBufferWidth;
        public int TexBufferPtr;
        public int CLUTBufferPrt;
        public int field11;
        public byte OutlineID;
        public byte Glow;
        public byte field14;
        public byte field15;
        public ulong GSTEX0;
        public int BufDMATexDataPtr;
        public int BufDMAPalDataPtr;
    }
}
