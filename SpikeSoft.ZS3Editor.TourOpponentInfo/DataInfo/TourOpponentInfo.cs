using System.Runtime.InteropServices;

namespace SpikeSoft.ZS3Editor.TourOpponentInfo
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TourOpponentInfo
    {
        public int AI;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public short[] ZItems;
    }
}
