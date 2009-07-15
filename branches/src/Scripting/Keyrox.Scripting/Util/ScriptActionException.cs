using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Util {
    public class ScriptActionException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptActionException"/> class.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="message">The message.</param>
        public ScriptActionException(ScriptInfo script, string message) {
            this.Script = script;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public ScriptInfo Script { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        public new string Message { get; private set; }
    }
}
