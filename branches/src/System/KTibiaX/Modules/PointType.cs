using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KTibiaX.Modules {
    public enum PointType {
        [Description("None")]
        None = 0,

        [Description("MP")]
        MP = 1,

        [Description("HP")]
        HP = 2,

        [Description("Stamina")]
        Stamina = 3,

        [Description("Soul")]
        Soul = 4,

        [Description("Cap")]
        Cap = 5,

        [Description("Location")]
        Location = 6
    }
}
