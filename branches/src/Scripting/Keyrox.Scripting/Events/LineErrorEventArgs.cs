using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Events {
    public class LineErrorEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="LineErrorEventArgs"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public LineErrorEventArgs(ScriptLineError error) {
            this.Error = error;
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public ScriptLineError Error { get; private set; }

    }
}
