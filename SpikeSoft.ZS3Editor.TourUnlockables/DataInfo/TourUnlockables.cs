﻿using System.Runtime.InteropServices;

namespace SpikeSoft.ZS3Editor.TourUnlockables
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TourUnlockables
    {
        public int Unknown_A;
        public int Unknown_B;
        public int Unknown_C;
        public int Unknown_D;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] Zeni_Winner;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] Zeni_RunnerUp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] ZItem;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] Chara1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] Chara2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] Map;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] Bgm;
    }
}
