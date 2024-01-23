using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Editor.Mission
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MissionBattleInfo
    {
        public BattleSettings BattleSettings;
        public int Type;
        public int Dp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public int[] OpponentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SurvivalBattleInfo
    {
        public BattleSettings BattleSettings;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public int[] OpponentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CircuitBattleInfo
    {
        public BattleSettings BattleSettings;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] OpponentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RankingBattleInfo
    {
        public BattleSettings BattleSettings;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public int[] OpponentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BattleSettings
    {
        public int Referee;
        public int MapDestruction;
        public int Time;
        public int Map;
        public int Bgm;
        public int TransformableEnemies;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OpponentInfo
    {
        public int CharacterID;
        public int CostumeID;
        public int AI;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Customization;
    }

    public class Mission
    {
        public dynamic BattleInfo { get; set; }
        public List<OpponentInfo> OpponentInfo { get; set; }
        public string Title;

        public Mission(string Title, dynamic BattleInfo, List<OpponentInfo> OpponentInfo)
        {
            this.Title = Title;
            this.BattleInfo = BattleInfo;
            this.OpponentInfo = OpponentInfo;
        }

        public BattleSettings BattleSettings { get { return BattleInfo.BattleSettings; } set { BattleInfo.BattleSettings = value; } }
        public int DpMode { get { return BattleInfo.Dp; } set { BattleInfo.Dp = value; } }
        public int Condition { get { return BattleInfo.Type; } set { BattleInfo.Type = value; } }
        public int OpponentCount { get { return BattleInfo.OpponentID.Length; } }
    }
}
