using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Evaluate Expressions")]
    public class EvalActions : ITibiaAction {

        #region ITibiaAction Members
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public Tibia.Client.TibiaClient Client { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public Keyrox.Scripting.Parser.ScriptInfo Script { get; set; }
        #endregion

        [ActionTitle("Check Value", "Execute the command set if the expression is true.")]
        [ActionConfig(ImageIndex = 11, InputText = "checkvalue()", CarretPosition = -1)]
        [ActionParameter("Evaluable Expression", "The expression to be evaluated.", 0)]
        [ActionExamples("checkvalue(countitem({Spear}) &lt; 5 = gotosection(HuntToDepot))", "checkvalue([CAP] &lt; getparam(\"MinCap\") = gotosection(HuntToDepot))", "checkvalue(countitem(3031) &gt; 3000 = gotoline(53))")]
        public ScriptActionResult CheckValue(string[] args) {
            #region "[rgn] Argument Validation "
            if (args.Count() != 1) { throw new ArgumentException("Invalid number of arguments!"); }
            #endregion

            

            return null;
        }

        [ActionTitle("Get Value", "Parse the expression and returns the value.")]
        [ActionConfig(ImageIndex = 11, InputText = "getvalue()", CarretPosition = -1)]
        [ActionParameter("Evaluable Expression", "The expression to be evaluated.", 0)]
        [ActionExamples("setparam(\"SpearToBuy\", getvalue([CAP] / getparam(\"SpearWeigth\")))", "setparam(\"MoneyToSpears\", getvalue(getparam(\"SpearToBuy\") * getparam(\"SpearCost\")))", "setparam(\"CustomValue\", getvalue((30 * 10) / 129 + 5))")]
        public ScriptActionResult GetValue(string[] args) {
            return null;
        }

    }
}
