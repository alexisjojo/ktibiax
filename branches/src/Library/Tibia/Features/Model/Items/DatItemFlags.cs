﻿using System;

namespace Tibia.Features.Model {
    public enum DatItemFlag {
        WalkSpeed = 0x00000001,
        TopOrder1 = 0x00000002,
        TopOrder2 = 0x00000004,
        TopOrder3 = 0x00000008,
        IsContainer = 0x00000010,
        IsStackable = 0x00000020,
        IsCorpse = 0x00000040,
        IsUsable = 0x00000080,
        IsRune = 0x00000100,
        IsWritable = 0x00000200,
        IsReadable = 0x00000400,
        IsFluidContainer = 0x00000800,
        IsSplash = 0x00001000,
        Blocking = 0x00002000,
        IsImmovable = 0x00004000,
        BlocksMissiles = 0x00008000,
        BlocksPath = 0x00010000,
        IsPickupable = 0x00020000,
        IsHangable = 0x00040000,
        IsHangableHorizontal = 0x00080000,
        IsHangableVertizcal = 0x00100000,
        IsRotatable = 0x00200000,
        IsLightSource = 0x00400000,
        Floorchange = 0x00800000,
        IsShifted = 0x01000000,
        HasHeight = 0x02000000,
        IsLayer = 0x04000000,
        IsIdleAnimation = 0x08000000,
        HasAutoMapColor = 0x10000000,
        HasHelpLens = 0x20000000,
        IsGround = 0x40000000
    }

    public enum DatItemDefination {
        IsLadder = 0x44C,
        IsSewer = 0x44D,
        IsDoor = 0x450,
        IsDoorWithLock = 0x451,
        IsRopeSpot = 0x44E,
        IsSwitch = 0x44F,
        IsStairs = 0x452,
        IsMailbox = 0x453,
        IsDepot = 0x454,
        IsTrash = 0x455,
        IsHole = 0x456,
        HasSpecialDescription = 0x457,
        IsReadOnly = 0x458
    }
}
