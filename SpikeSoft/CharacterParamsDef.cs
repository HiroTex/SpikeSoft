using System;
using System.Runtime.InteropServices;

namespace ZS4Editor_Data.DataInfo
{
    #region TypeDefs
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharacterProperties
    {
        public CharacterSpecialProperties Special;
        public CharacterMovesetProperties Moveset;
        public CharacterSparkingProperties Sparking;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharacterBlastCombos
    {
        public CharacterBlastComboType Vanishing;
        public CharacterBlastComboType Dragon_Tornado;
        public CharacterBlastComboType Heavy_Crush;
        public CharacterBlastComboType Blaster_Wave;
        public CharacterBlastComboType Rolling_Hurricane;
        public CharacterBlastComboType Power_Press;
        public CharacterBlastComboType Energy_Storm;
        public CharacterBlastComboType Burst_Meteor;
        public CharacterBlastComboType Air_Combo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharacterTransformationInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ResultID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] BlastStockUnitCost;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CharacterTransformationType[] Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CharacterTransformationProperty[] Property;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] PartnerID;
        public byte QuickForm;
    }

    public enum CharacterTransformationType : byte
    {
        Change = 0,
        Back = 1,
        Oozaru = 2,
        PartnerAbsorption = 3,
        Union = 4
    }

    public enum CharacterTransformationProperty : byte
    {
        AuraOff = 0,
        AuraBlue = 1,
        AuraYellow = 2,
        Moon = 3,
        ArtificialMoon = 4,
        Unk5 = 5,
        Unk6 = 6,
        ExpansiveWave = 7,
        AuraRed = 8,
        AuraGreen = 9,
        AuraEvilViolet = 10,
        AuraSuperSaiyan = 11
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharacterFusionInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] BlastStockUnitCost;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public CharacterFusionType[] Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] ResultID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] PartnerID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public CharacterFusionPartnerInfo[] PartnerForms;
    }

    public enum CharacterFusionType : byte
    {
        None = 0,
        Metamoran = 1,
        Potara = 2
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharacterFusionPartnerInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] FormID;
    }

    [Flags]
    public enum TransformationBonus : byte
    {
        HP_HALF_BAR = 1 << 0,
        HP_BAR = 1 << 1,
        KI_FULL = 1 << 2,
        MAX_MODE = 1 << 3,
        COSTUME_REPAIRED_FORM_1 = 1 << 4,
        COSTUME_REPAIRED_FORM_2 = 1 << 5,
        COSTUME_REPAIRED_FORM_3 = 1 << 6,
        COSTUME_REPAIRED_FORM_4 = 1 << 7,
    }

    [Flags]
    public enum CharacterType : ushort
    {
        Saiyan = 1 << 0,
        Super_Saiyan = 1 << 1,
        Super_Saiyan_2 = 1 << 2,
        Super_Saiyan_3 = 1 << 3,
        Super_Saiyan_4 = 1 << 4,
        Legendary_Super_Saiyan = 1 << 5,
        Enhanced_Super_Saiyan = 1 << 6,
        Android = 1 << 7,
        Majin = 1 << 8,
        Great_Ape = 1 << 9,
        Unused_10 = 1 << 10,
        Unused_11 = 1 << 11,
        Unused_12 = 1 << 12,
        Unused_13 = 1 << 13,
        Unused_14 = 1 << 14,
        Unused_15 = 1 << 15
    }

    public enum CharacterWeight : byte
    {
        Little = 0,
        Small = 1,
        Normal = 2,
        Big = 3,
        Giant = 4
    }

    public enum CharacterAuraColor : byte
    {
        White = 0,
        Violet = 1,
        Yellow = 2,
        Red = 3,
        Green = 4,
        Blue = 5,
        Pink = 6,
        Dark_Blue = 7,
        Super_Saiyan = 8,
        Super_Saiyan_3 = 9,
        Super_Saiyan_4 = 10
    }

    [Flags]
    public enum CharacterSpecialProperties : uint
    {
        Tail = 1 << 0,
        Absorb_Energy_Blasts = 1 << 1,
        Body_Armor = 1 << 2,
        Weak = 1 << 3,
        Aura_Off = 1 << 4,
        Robot = 1 << 5,
        Sword_SFX_EQUIPMENT01 = 1 << 6,
        Sword_SFX_EQUIPMENT02 = 1 << 7,
        Continuous_Energy_Blast_Barrage = 1 << 8,
        Downward_Throw = 1 << 9,
        Downward_Throw_Early_Escape = 1 << 10,
        Giant = 1 << 11,
        Flightless = 1 << 12,
        Unk_0x2000 = 1 << 13,
        Cancel_Special_Throws = 1 << 14,
        Metallic_Step_SFX = 1 << 15,
        Uninterpolated_Tired_Face = 1 << 16,
        Uninterpolated_Dash_Animation = 1 << 17,
        Saiyan_Transformation_SFX = 1 << 18,
        Throw_Animation_Modifier = 1 << 19,
        Air_Combo_Teleport_SFX = 1 << 20,
        Enable_Halo_by_Z_Item = 1 << 21,
        Unused_22 = 1 << 22,
        Unused_23 = 1 << 23,
        Solar_Flare_Immunity_1p = 1 << 24,
        Solar_Flare_Immunity_1p_dmg = 1 << 25,
        Solar_Flare_Immunity_2p = 1 << 26,
        Solar_Flare_Immunity_2p_dmg = 1 << 27,
        Solar_Flare_Immunity_3p = 1 << 28,
        Solar_Flare_Immunity_3p_dmg = 1 << 29,
        Solar_Flare_Immunity_4p = 1 << 30,
        Solar_Flare_Immunity_4p_dmg = (uint)1 << 31,
    }

    [Flags]
    public enum CharacterMovesetProperties : uint
    {
        Step_In = 1 << 0,
        Rush_In = 1 << 1,
        Unknown_0x4 = 1 << 2,
        High_Speed_Rush_Movement = 1 << 3,
        Jump_Energy_Volley = 1 << 4,
        Deflect_Energy_Blasts = 1 << 5,
        Throw_Recovery = 1 << 6,
        Sonic_Sway = 1 << 7,
        Sonic_Sway_Strike = 1 << 8,
        Unused_9 = 1 << 9,
        Unused_10 = 1 << 10,
        Lift_Strike = 1 << 11,
        Ground_Slash = 1 << 12,
        Vanishing_Attack = 1 << 13,
        Lightning_Attack = 1 << 14,
        Burst_Meteor = 1 << 15,
        Burst_Smash = 1 << 16,
        Dragon_Tornado = 1 << 17,
        Heavy_Crush = 1 << 18,
        Giant_Throw = 1 << 19,
        Sonic_Impact = 1 << 20,
        Kiai_Cannon_Smash = 1 << 21,
        Spiral_Slash = 1 << 22,
        Rolling_Hurricane = 1 << 23,
        Power_Press = 1 << 24,
        Energy_Storm = 1 << 25,
        Tri_Attack = 1 << 26,
        Aerial_Barrage = 1 << 27,
        Delta_Storm = 1 << 28,
        Blaster_Wave_Combo = 1 << 29,
        Unused_30 = 1 << 30,
        Unused_31 = (uint)1 << 31,
    }

    [Flags]
    public enum CharacterSparkingProperties : uint
    {
        Melee_Charge_Halved = 1 << 0,
        Unused_1 = 1 << 1,
        Unused_2 = 1 << 2,
        Super_Movement = 1 << 3,
        Violent_Rush = 1 << 4,
        Unk_0x20 = 1 << 5,
        Body_Armor_Melee = 1 << 6,
        Body_Armor_Energy = 1 << 7,
        Increase_Melee_Stun_Power = 1 << 8,
        Hyper_Smash = 1 << 9,
        Unused_10 = 1 << 10,
        Unused_11 = 1 << 11,
        Blast_Combo_Vanishing = 1 << 12,
        Blast_Combo_Dragon_Tornado = 1 << 13,
        Blast_Combo_Heavy_Crush = 1 << 14,
        Blast_Combo_Blaster_Wave = 1 << 15,
        Blast_Combo_Rolling_Hurricane = 1 << 16,
        Blast_Combo_Power_Press = 1 << 17,
        Blast_Combo_Energy_Storm = 1 << 18,
        Blast_Combo_Burst_Meteor = 1 << 19,
        Blast_Combo_Air_Combo = 1 << 20,
        Unused_21 = 1 << 21,
        Unused_22 = 1 << 22,
        Unused_23 = 1 << 23,
        Unused_24 = 1 << 24,
        Unused_25 = 1 << 25,
        Unused_26 = 1 << 26,
        Unused_27 = 1 << 27,
        Unused_28 = 1 << 28,
        Unused_29 = 1 << 29,
        Unk_0x40000000 = 1 << 30,
        Unk_0x80000000 = (uint)1 << 31,
    }

    public enum CharacterSearchType : byte
    {
        Z_Slow = 0,
        Z_Fast = 1,
        Scouter_Slow = 2,
        Scouter_Mid = 3,
        Scouter_Fast = 4,
        Android_Z = 5,
        Android_Scouter = 6,
        Dmg_Scouter_Slow = 7,
        Dmg_Scouter_Fast = 8,
        Special_Scouter = 9
    }

    public enum CharacterRushComboType : byte
    {
        Heavy_Finish = 0,
        Kiai_Cannon = 1,
        Blaster_Wave = 2,
        Flying_Kick = 3,
        Energy_Wave = 4,
        Rolling_Hammer = 5
    }

    public enum CharacterParryComboType : byte
    {
        Strike = 0,
        Heavy_Finish = 1,
        Kiai_Cannon = 2,
        Blaster_Wave = 3,
        Flying_Kick = 4,
        Energy_Wave = 5,
        Rolling_Hammer = 6
    }

    public enum CharacterComboInType : byte
    {
        Heavy_Finish = 0,
        Sway_Heavy_Finish = 1,
        Kiai_Cannon = 2,
        Sway_Kiai_Cannon = 3,
        Blaster_Wave = 4,
        Sway_Blaster_Wave = 5,
        Flying_Kick = 6,
        Sway_Flying_Kick = 7,
        Energy_Wave = 8,
        Sway_Energy_Wave = 9,
        Rolling_Hammer = 10,
        Sway_Rolling_Hammer = 11
    }

    public enum CharacterBlastComboType : byte
    {
        BLAST2_A_Type_1 = 0,
        BLAST2_B_Type_1 = 1,
        BLAST2_A_Type_2 = 2,
        BLAST2_A_Type_1_Instant = 3,
        BLAST2_B_Type_1_Instant = 4,
        BLAST2_A_Type_2_Instant = 5
    }
    #endregion

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 256)]
    public struct CharacterPrmInfo
    {
        public CharacterType Type;
        public CharacterWeight Weight;
        public CharacterAuraColor AuraColor;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] CollisionCapsule;
        public CharacterProperties Properties;
        public int Unk0x1C;
        public int Unk0x20;
        public int Unk0x24;
        public int PassiveKiLimit;
        public int FatigueKiLimit;
        public int ActiveKiCharge;
        public int ActiveKiChargeUnderwater;
        public int PassiveKiCharge;
        public int PassiveKiChargeFatigue;
        public int BlastStockUnitLimit;
        public int PassiveBlastGaugeCharge;
        public int QuickRecoveryKiCost;
        public int ParryStanceKiCost;
        public float SparkingModeKiFill;
        public float SparkingModeKiDepletion;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CharacterSearchType[] SearchType;
        public float Unk0x5C;
        public float KnockbackTime;
        public float SwitchGaugeFill;
        public float SideStepMomentum;
        public float BackStepMomentum;
        public short ExtraDragonHomingAttack;
        public short ExtraVanishingAttack;
        public float BaseMomentum;
        public float MomentumGrowth;
        public float SelfDamageMultiplier;
        public short ConsecutiveKidan;
        public short ConsecutiveChargedKidan;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CharacterRushComboType[] RushComboString;
        public CharacterParryComboType ParryStrike;
        public byte SwayStepIn;
        public CharacterComboInType StepInStrike;
        public byte SwayLiftStrike;
        public byte SwayGroundSlash;
        public CharacterComboInType RushInStrike;
        public CharacterComboInType StunMove;
        public CharacterBlastCombos BlastCombos;
        public CharacterTransformationInfo TransformationInfo;
        public TransformationBonus TransformProperties;
        public CharacterFusionInfo FusionInfo;
        public byte Unk0xC6;
        public byte Devilmite_Damage_Multiplier;
        public float Unk0xC8;
    }
}
