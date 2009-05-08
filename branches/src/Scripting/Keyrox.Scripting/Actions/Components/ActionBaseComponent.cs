using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Client;

namespace Keyrox.Scripting.Actions.Components {
    public class ActionBaseComponent {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseComponent"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ActionBaseComponent(TibiaClient client) {
            this.Client = client;
        }

        /// <summary>
        /// Waits the specified miliseconds.
        /// </summary>
        /// <param name="miliseconds">The miliseconds.</param>
        public void Wait(int miliseconds) {
            System.Threading.Thread.Sleep(miliseconds);
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public TibiaClient Client { get; private set; }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Tibia.Features.Model.Player Player { get { return Client != null ? Client.Features.Player : null; } }
    }
}
