using System;
using Tibia.Client;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Actions {
    public interface ITibiaAction {

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        TibiaClient Client { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        ScriptInfo Script { get; set; }
    }
}
