using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions.Contracts {
    public interface IKillActions : ITibiaAction {

        [ActionArgumentValidtion(Length = 2), ArgumentType(1, TypeCode.Int32)]
        ScriptActionResult KillPrior(string[] args);

        [ActionArgumentValidtion(Length = 3), ArgumentType(2, TypeCode.Int32)]
        ScriptActionResult KillSpell(string[] args);

        [ActionArgumentValidtion(Length = 4), ArgumentType(1, TypeCode.Int32), ArgumentType(2, TypeCode.Int32), ArgumentType(3, TypeCode.Int32)]
        ScriptActionResult KillRune(string[] args);

        [ActionArgumentValidtion(Length = 0)]
        ScriptActionResult KillOnlyMyCreatures(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult Ignore(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult AvoidFront(string[] args);
    }
}
