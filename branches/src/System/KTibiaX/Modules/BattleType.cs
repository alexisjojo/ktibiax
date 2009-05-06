using System.ComponentModel;

namespace KTibiaX.Modules {
    public enum BattleType {

        [Description("Player")]
        Player = 1,

        [Description("Creature")]
        Creature = 2,

        [Description("GM")]
        GM = 3,

        [Description("PK")]
        PK = 4

    }
}
