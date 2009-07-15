using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Client;

namespace Keyrox.Scripting.Actions.Components.Player {
    public class eDepotActions  : ActionBaseComponent {

        /// <summary>
        /// Initializes a new instance of the <see cref="DepotActions"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public eDepotActions(TibiaClient client)
            : base(client) {
        }

        /// <summary>
        /// Finds the empty depot.
        /// </summary>
        /// <returns></returns>
        public bool FindEmptyDepot() {
            return false;
        }

        /// <summary>
        /// Deposits the loot.
        /// </summary>
        public void DepositLoot() {
        }
    }
}
