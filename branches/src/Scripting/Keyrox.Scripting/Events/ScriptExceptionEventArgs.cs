using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Util;
using Keyrox.SourceCode;

namespace Keyrox.Scripting.Events {
    public class ScriptExceptionEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="rowIndex">Index of the row.</param>
        public ScriptExceptionEventArgs(ScriptActionException ex, int rowIndex) {
            this.Exception = ex;
            this.RowIndex = rowIndex;
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public ScriptActionException Exception { get; private set; }

        /// <summary>
        /// Gets or sets the index of the row.
        /// </summary>
        /// <value>The index of the row.</value>
        public int RowIndex { get; private set; }

    }
}
