using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Keyrox.Scripting.Util {
    public enum RunnerState {

        [Description("Stoped")]
        Stoped = 0,

        [Description("Paused")]
        Paused = 1,

        [Description("Running")]
        Running = 2,
    }
}
