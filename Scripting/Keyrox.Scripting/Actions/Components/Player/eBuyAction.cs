using System;
using Tibia.Client;

namespace Keyrox.Scripting.Actions.Components.Player {
    public class eBuyAction : ActionBaseComponent {

        /// <summary>
        /// Initializes a new instance of the <see cref="BuyAction"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public eBuyAction(TibiaClient client)
            : base(client) {
        }

        /// <summary>
        /// Executes the specified item ID.
        /// </summary>
        /// <param name="itemID">The item ID.</param>
        /// <param name="quantity">The quantity.</param>
        public bool Execute(uint itemID, int quantity) {
            return true;
        }

        /// <summary>
        /// Determines whether [is NPC active].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is NPC active]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNpcActive() {
            return true;
        }
    }
}
