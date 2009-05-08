using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Keyrox.Scripting {
    public enum AutoListType {

        [Description("No auto list is needed.")]
        None = 0,

        [Description("General Language auto list.")]
        General = 1,

        [Description("Player attributes auto list.")]
        Player = 2,

        [Description("Custom items auto list.")]
        Item = 3,

        [Description("Script Sections auto list.")]
        Sections = 4,

        [Description("Script Params auto list.")]
        Params = 5
    }
}
