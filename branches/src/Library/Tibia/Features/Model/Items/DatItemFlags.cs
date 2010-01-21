using System;
using System.Collections.Generic;

namespace Tibia.Features.Model {

    public static class DatItemTypes {

        public static List<uint> UP {
            get {
                return new List<uint>(){
                    1947, //Stairs 
                    1950, //Ramp 
                    1952, //Ramp1 
                    1954, //Ramp2 
                    1956, //Ramp3 
                    1958, //WoodenStairs 
                    1960, //Ramp4 
                    1962, //Ramp5 
                    1964, //Ramp6 
                    1966, //Ramp7 
                    2192, //Ramp8 
                    2194, //Ramp9 
                    2196, //Ramp10 
                    2198, //Ramp11 
                    1969, //Ramp12 
                    1971, //Ramp13 
                    1973, //Ramp14 
                    1975, //Ramp15 
                    1977, //StoneStairs 
                    1978, //StoneStairs1 
                    5257, //Stairs1 
                    5258, //Stairs2 
                    5259, //Stairs3 
                    6909, //Ramp16 
                    6911, //Ramp17 
                    6913, //Ramp18 
                    6915, //Ramp19 
                    855, //StoneStairs2 
                    856, //StoneStairs3 
                    7542, //Ramp20 
                    7544, //Ramp21 
                    7546, //Ramp22 
                    7548, //Ramp23 
                    7881, //WoodenStairs1 
                    7887, //Ramp24 
                    7888, //StoneStairs4 
                    5033, //Roof 
                    5035, //Roof1 
                    5037, //Roof2 
                    5039, //Roof3 
                    8657, //CorkscrewStairs 
                    8830, //Ramp25 
                    8831, //Ramp26
                };
            }
        }

        public static List<uint> UPUse {
            get {
                return new List<uint>(){
                    1948, //Ladder3 
                    1968, //Ladder4
                    5542, //RopeLadder
                    7771, //Ladder5 
                    9116, //Ladder6 
                };
            }
        }

        public static List<uint> Rope {
            get {
                return new List<uint>(){
                    386, //DirtFloor
                    421, //StoneTile 
                    386, //DirtFloor1 
                    7762, //DirtFloor2 
                };
            }
        }

        public static List<uint> Down {
            get {
                return new List<uint>() {
                    293, //Grass 
                    294, //Pitfall 
                    369, //Trapdoor 
                    370, //Trapdoor1 
                    385, //Hole 
                    394, //Hole1 
                    411, //Trapdoor2 
                    412, //Trapdoor3 
                    413, //Stairs4 
                    414, //Stairs5 
                    428, //Stairs6 
                    432, //Trapdoor4 
                    433, //Ladder 
                    434, //Trapdoor5 
                    437, //Stairs7 
                    438, //Stairs8 
                    469, //Stairs9 
                    476, //OpenTrapdoor 
                    594, //Hole2 
                    595, //Hole3 
                    601, //Hole4 
                    600, //Hole5 
                    604, //Hole6 
                    605, //Hole7 
                    607, //Hole8 
                    609, //Hole9 
                    610, //Hole10 
                    615, //Hole11 
                    1156, //Trapdoor6 
                    482, //Ladder1 
                    483, //Ladder2 
                    484, //Trapdoor7 
                    485, //Stairway 
                    566, //Stairs10 
                    567, //Stairs11 
                    1066, //Pitfall1 
                    1067, //Pitfall2 
                    1080, //EarthHole 
                    4823, //Stairs12 
                    859, //Stairs13 
                    4825, //Stairs14 
                    4826, //Stairs15 
                    5081, //Stairs16 
                    5544, //Trapdoor8 
                    5691, //Trapdoor9 
                    5731, //Hole12 
                    5763, //Trapdoor10 
                    6127, //Ramp27 
                    6128, //Ramp28 
                    6129, //Ramp29 
                    6130, //Ramp30 
                    6172, //Trapdoor11 
                    6173, //Trapdoor12 
                    6754, //IceHut 
                    6755, //IceHut1 
                    6756, //IceHut2 
                    6917, //Ramp31 
                    6918, //Ramp32 
                    6919, //Ramp33 
                    6920, //Ramp34 
                    6921, //Ramp35 
                    6922, //Ramp36 
                    6923, //Ramp37 
                    6924, //Ramp38 
                    7053, //Trapdoor13 
                    166, //WoodenCoffin 
                    167, //WoodenCoffin1 
                    867, //LargeHole 
                    868, //Hole13 
                    874, //LargeHole1 
                    4824, //Stairs17 
                    7181, //CaveEntrance 
                    7182, //CaveEntrance1 
                    7476, //CaveEntrance2 
                    7477, //CaveEntrance3 
                    7478, //CaveEntrance4 
                    7479, //CaveEntrance5 
                    7515, //Hole14 
                    7516, //Hole15 
                    7517, //Hole16 
                    7518, //Hole17 
                    7519, //SomethingCrawling 
                    7520, //Hole18 
                    7521, //Hole19 
                    7522, //LargeHole2 
                    369, //Trapdoor14 
                    370, //Trapdoor15 
                    411, //Trapdoor16 
                    7767, //Trapdoor17 
                    413, //Trapdoor18 
                    414, //Stairs18 
                    428, //Stairs19 
                    432, //Trapdoor19 
                    433, //Trapdoor20 
                    7768, //Trapdoor21 
                    967, //Hole20 
                    7550, //TunnelEntrance 
                    7729, //Ramp39 
                    7730, //Ramp40 
                    7731, //Ramp41 
                    7732, //Ramp42 
                    7733, //Ramp43 
                    7734, //Ramp44 
                    7735, //Ramp45 
                    7736, //Ramp46 
                    7737, //Hole21 
                    7755, //Hole22 
                    7767, //Trapdoor22 
                    7768, //Trapdoor23 
                    7804, //WaterVortex 
                    8144, //LargeHole3 
                    8658, //CorkscrewStairs1 
                    8690, //Stairs20 
                    8709, //OpenTrapdoor1 
                    8932, //CorkscrewStairs2 
                };
            }
        }

        public static List<uint> DownUse {
            get {
                return new List<uint>(){
                    435, //SewerGrate 
                    7750, //SewerGrate1 
                };
            }
        }

        public static List<uint> Shovel {
            get {
                return new List<uint>(){
                    593, //StonePile 
                    606, //LooseStonePile 
                    608, //LooseIcePile 
                    7749, //LooseStonePile1 
                    7749, //Hole23 
                };
            }
        }

    }

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
