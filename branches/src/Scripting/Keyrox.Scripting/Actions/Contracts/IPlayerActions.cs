using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions.Contracts {
    public interface IPlayerActions : ITibiaAction {

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult Say(string[] args);

        [ActionArgumentValidtion(Length = 2), ArgumentType(1, TypeCode.Int32)]
        ScriptActionResult SayAfter(string[] args);

        [ActionArgumentValidtion(Length = 2), ArgumentType(0, TypeCode.Int32), ArgumentType(1, TypeCode.Int32)]
        ScriptActionResult Buy(string[] args);

        [ActionArgumentValidtion(Length = 2), ArgumentType(0, TypeCode.Int32), ArgumentType(1, TypeCode.Int32), ArgumentType(2, TypeCode.Int32)]
        ScriptActionResult GoTo(string[] args);
    }
}
