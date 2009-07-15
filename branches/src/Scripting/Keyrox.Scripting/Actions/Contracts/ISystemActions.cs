using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions.Contracts {
    public interface ISystemActions : ITibiaAction {

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult Log(string[] args);

        [ActionArgumentValidtion(Length = 0)]
        ScriptActionResult Pause(string[] args);

        [ActionArgumentValidtion(Length = 0)]
        ScriptActionResult Resume(string[] args);

        [ActionArgumentValidtion(Length = 1), ArgumentType(0, TypeCode.Int32)]
        ScriptActionResult Wait(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult Error(string[] args);

        [ActionArgumentValidtion(Length = 1), ArgumentType(0, TypeCode.Int32)]
        ScriptActionResult GoToLine(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult GoToSection(string[] args);

        [ActionArgumentValidtion(Length = 1)]
        ScriptActionResult RunSection(string[] args);
    }
}
