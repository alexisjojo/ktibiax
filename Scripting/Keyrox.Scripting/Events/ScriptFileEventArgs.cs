using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Events {
    public class ScriptFileEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptFileEventArgs"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public ScriptFileEventArgs(ScriptFile file) {
            this.Script = file;
        }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        public ScriptFile Script { get; private set; }


    }
}
