using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions.Contracts {
    public interface IEvalActions : ITibiaAction {

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult CheckValue(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult GetValue(string[] args);
    }
}
