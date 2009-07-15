﻿using System;
using Keyrox.Scripting.Actions.Components.Player;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Parser;
using Keyrox.Shared.Objects;
using Keyrox.Scripting.Actions.Contracts;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Parameter Management")]
    public class ParamActions : IParamActions {

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

        [ActionTitle("Set Parameter", "Set the value of a defined script parameter.")]
        [ActionConfig(ImageIndex = 7, InputText = "setparam(\"\")", CarretPosition = -2)]
        [ActionParameter("Name", "The name of the parameter.", 0, NeedQuotes = true)]
        [ActionParameter("Value", "The value of the parameter.", 1)]
        [ActionExamples("setparam(\"SpearCost\", 10)", "setparam(\"SpearWeigth\", 20)")]
        public ScriptActionResult SetParam(string[] args) {
            Script.SetParam(args[0], args[1]);
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Get Parameter", "Returns the value of a defined script parameter.")]
        [ActionConfig(ImageIndex = 7, InputText = "getparam(\"\")", CarretPosition = -2)]
        [ActionParameter("Name", "The name of the parameter.", 0, NeedQuotes = true)]
        [ActionExamples("getparam(\"SpearCost\")")]
        public ScriptActionResult GetParam(string[] args) {
            var paramname = args[0].ToString();
            return new ScriptActionResult() { ReturnValue = Script.GetParam(paramname), Success = true };
        }


        [ActionTitle("Count Item", "Returns the total count of some item looking into opened containers.")]
        [ActionConfig(ImageIndex = 11, InputText = "countitem()", CarretPosition = -1, AutoList = AutoListType.Item)]
        [ActionParameter("Item ID", "The ID of the item.", 0)]
        [ActionExamples("countitem(3031)", "countitem({Spear})")]
        public ScriptActionResult CountItem(string[] args) {
            var itemID = args[0].ToUInt32();
            return new ScriptActionResult() { ReturnValue = new eItemActions(Client).Count(itemID), Success = true };
        }
    }
}
