using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions.Contracts {
    public interface IParamActions : ITibiaAction {

        [ActionArgumentValidtion(Length = 2)]
        ScriptActionResult SetParam(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult GetParam(string[] args);

        [ActionArgumentValidtion(Length = 1), ArgumentType(0, TypeCode.Int32)]
        ScriptActionResult CountItem(string[] args);
    }
}
