using System;
using Tibia.Client;

namespace KTibiaX.UI.Events {
    public class TibiaClientEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="TibiaClientEventArgs"/> class.
        /// </summary>
        /// <param name="tibiaClient">The tibiaClient.</param>
        public TibiaClientEventArgs(TibiaClient tibiaClient) {
            TibiaClient = tibiaClient;
        }

        /// <summary>
        /// Gets or sets the tibiaClient.
        /// </summary>
        /// <value>The tibiaClient.</value>
        public TibiaClient TibiaClient { get; set; }
    }
}
