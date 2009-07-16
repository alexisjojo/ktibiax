using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Parser {
    public class ScriptSection {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptSection"/> class.
        /// </summary>
        public ScriptSection(string name, ScriptLine startLine, ScriptLine endLine) {
            this.Name = name;
            this.StartLine = startLine;
            this.EndLine = endLine;
        }

        #region "[rgn] Properties "
        public string Name { get; set; }
        public ScriptLine StartLine { get; set; }
        public ScriptLine EndLine { get; set; }
        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return !string.IsNullOrEmpty(Name) ? Name : base.ToString();
        }

    }
}
