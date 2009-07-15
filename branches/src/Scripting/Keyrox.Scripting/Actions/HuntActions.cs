using System;
using Keyrox.Scripting.Actions.Components.Player;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Parser;
using Keyrox.Shared.Objects;
using Keyrox.Scripting.Actions.Contracts;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Hunt Configuration")]
    public class HuntActions : IHuntActions {

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

        [ActionTitle("Set Loot", "Add an item to the list of loot.")]
        [ActionConfig(ImageIndex = 10, InputText = "setloot()", CarretPosition = -1, AutoList = AutoListType.Item)]
        [ActionParameter("Item ID", "The ID of the item.", 0)]
        [ActionExamples("setloot({GP})", "setloot(3031)")]
        public ScriptActionResult SetLoot(string[] args) {
            Script.SetLoot(args[0].ToUInt32());
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Find Empty Depot", "Finds and go to any empty depot storage.\nUse this function to find a empty dp, instead of record the sqm of a storage depot.\nIf no empty depot was found, the script will wait for 5 seconds before search again.")]
        [ActionConfig(ImageIndex = 13, InputText = "findemptydp()")]
        [ActionExamples("findemptydp()")]
        public ScriptActionResult FindEmptyDP(string[] args) {
            new eDepotActions(Client).FindEmptyDepot();
            return new ScriptActionResult() { Success = true };
        }
        
        [ActionTitle("Deposit Loot", "Open the storage and deposit all items contained in loot list.\nIf the player is not in front of a depot storage, the script will stops.")]
        [ActionConfig(ImageIndex = 1, InputText = "depositloot()")]
        [ActionExamples("depositloot()")]
        public ScriptActionResult DepositLoot(string[] args) {
            new eDepotActions(Client).DepositLoot();
            return new ScriptActionResult() { Success = true };
        }

    }
}
