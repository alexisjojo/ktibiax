using System;
using Tibia.Client;

namespace Keyrox.Scripting.Actions.Components.Player {
    public class eItemActions : ActionBaseComponent {

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemActions"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public eItemActions(TibiaClient client)
            : base(client) {
        }

        /// <summary>
        /// Counts the specified item ID.
        /// </summary>
        /// <param name="ItemID">The item ID.</param>
        /// <returns></returns>
        public int Count(uint ItemID) {
            var res = 0;
            if (Player.Containers.Count > 0) {
                foreach (var bp in Player.Containers) {
                    var items = bp.Slots.SearchAll(ItemID);
                    res += items.Count;
                }
            }
            return res;
        }

    }
}
