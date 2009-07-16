using System;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Events {
    public class ScriptFileEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptFileEventArgs"/> class.
        /// </summary>
        /// <param name="script">The script.</param>
        public ScriptFileEventArgs(ScriptFile script) {
            this.Script = script;
        }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public ScriptFile Script { get; set; }

    }
}
