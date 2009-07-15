using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Events {
    public class ExceptionEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public ExceptionEventArgs(Exception ex) {
            this.Exception = ex;
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; private set; }

    }
}
