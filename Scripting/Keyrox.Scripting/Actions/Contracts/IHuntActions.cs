using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions.Contracts {
    public interface IHuntActions : ITibiaAction {

        [ActionArgumentValidtion(Length = 1), ArgumentType(0, TypeCode.Int32)]
        ScriptActionResult SetLoot(string[] args);

        [ActionArgumentValidtion(Length = 0)]
        ScriptActionResult FindEmptyDP(string[] args);

        [ActionArgumentValidtion(Length = 0)]
        ScriptActionResult DepositLoot(string[] args);
    }
}
