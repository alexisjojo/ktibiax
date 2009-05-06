using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KTibiaX.Modules {
    public enum PointOperator {

        [Description("Equal")]
        Equal = 0,

        [Description("NotEqual")]
        NotEqual = 1,

        [Description("MoreThan")]
        MoreThan = 2,

        [Description("LessThan")]
        LessThan = 3,

        [Description("Max")]
        Max = 4,

        [Description("Min")]
        Min = 5

    }
}
