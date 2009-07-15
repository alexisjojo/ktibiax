﻿using System;
using Keyrox.Scripting.Actions.Components.Player;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Parser;
using Keyrox.Shared.Objects;
using Keyrox.Scripting.Actions.Contracts;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Player Actions")]
    public class PlayerActions : IPlayerActions {

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

        [ActionTitle("Say", "Say a defined message in default channel.")]
        [ActionConfig(ImageIndex = 4, InputText = "say(\"\")", CarretPosition = -2)]
        [ActionParameter("Message", "The message to be said.", 0, NeedQuotes = true)]
        [ActionExamples("say(\"hi\")", "say(\"bye\")")]
        public ScriptActionResult Say(string[] args) {
            Client.Features.Player.SayOnDefault(args[0]);
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Say After", "Say a defined message in default channel after some seconds.")]
        [ActionConfig(ImageIndex = 4, InputText = "sayafter(\"\", 0)", CarretPosition = -5)]
        [ActionParameter("Message", "The message to be said.", 0, NeedQuotes = true)]
        [ActionParameter("Delay", "The delay in seconds to continue.", 1)]
        [ActionExamples("sayafter(\"hi\", 1)", "sayafter(\"deposit all\", 2)", "sayafter(\"yes\", 2)")]
        public ScriptActionResult SayAfter(string[] args) {
            Client.Features.Player.SayOnDefault(args[0]);
            System.Threading.Thread.Sleep(args[1].ToInt32());
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Buy", "Buy some item after the npc conversation has been started.")]
        [ActionConfig(ImageIndex = 10, InputText = "buy()", CarretPosition = -1, AutoList = AutoListType.Item)]
        [ActionParameter("Item ID", "The ID of the item to be purchased.", 0)]
        [ActionParameter("Quantity", "The item count.", 1)]
        [ActionExamples("buy({Spear}, 20)", "buy(3277, getparam(\"SpearToBuy\"))")]
        public ScriptActionResult Buy(string[] args) {
            #region "[rgn] Argument Validation "
            if (args[0].ToInt32() <= 0) { throw new ArgumentException("Invalid Item ID!"); }
            if (args[1].ToInt32() <= 0) { throw new ArgumentException("Invalid Item Quantity!"); }
            #endregion

            //TODO: Set No Atk no Folow
            var itemID = args[0].ToUInt32();
            var quantity = args[1].ToInt32();
            return new ScriptActionResult() { Success = new eBuyAction(Client).Execute(itemID, quantity) };
        }

        [ActionTitle("Go to", "Move the player to defined location.")]
        [ActionConfig(ImageIndex = 16, InputText = "goto()", CarretPosition = -1)]
        [ActionParameter("Location X", "The map coordinate X.", 0)]
        [ActionParameter("Location Y", "The map coordinate Y.", 1)]
        [ActionParameter("Location Z", "The map floor Z.", 2)]
        [ActionExamples("goto(12345, 12345, 7)")]
        public ScriptActionResult GoTo(string[] args) {
            #region "[rgn] Argument Validation "
            if (args[0].ToInt32() <= 0) { throw new ArgumentException("Invalid X Location!"); }
            if (args[1].ToInt32() <= 0) { throw new ArgumentException("Invalid Y Location!"); }
            #endregion

            var location = new Tibia.Features.Structures.Location(args[0].ToUInt32(), args[1].ToUInt32(), args[2].ToUInt32());
            var success = new eGotoAction(Client).Execute(location);
            return success ? new ScriptActionResult() { Success = success } : new ScriptActionResult() { MustReExecute = true };
        }
    }
}