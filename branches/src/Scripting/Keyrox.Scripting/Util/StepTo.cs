using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Keyrox.Scripting.Util {
    public enum StepTo {

        [Description("None")]
        None = 0,

        [Description("Next Row")]
        NextRow = 1,

        [Description("Next Break Point")]
        NextBreakPoint = 2

    }
}
