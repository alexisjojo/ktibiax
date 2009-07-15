using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Events {
    public class ScriptLineEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLineEventArgs"/> class.
        /// </summary>
        /// <param name="line">The line.</param>
        public ScriptLineEventArgs(ScriptLine line) {
            this.Line = line;
        }

        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public ScriptLine Line { get; private set; }

    }
}
