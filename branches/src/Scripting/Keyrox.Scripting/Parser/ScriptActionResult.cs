using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Parser {
    public class ScriptActionResult {

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ScriptActionResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the line to jump.
        /// </summary>
        /// <value>The line to jump.</value>
        public int LineToJump { get; set; }

        /// <summary>
        /// Gets or sets the section to jump.
        /// </summary>
        /// <value>The section to jump.</value>
        public string SectionToJump { get; set; }

        /// <summary>
        /// Gets or sets the return value.
        /// </summary>
        /// <value>The return value.</value>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [must pause].
        /// </summary>
        /// <value><c>true</c> if [must pause]; otherwise, <c>false</c>.</value>
        public bool MustPause { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [must stop].
        /// </summary>
        /// <value><c>true</c> if [must stop]; otherwise, <c>false</c>.</value>
        public bool MustStop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [must resume].
        /// </summary>
        /// <value><c>true</c> if [must resume]; otherwise, <c>false</c>.</value>
        public bool MustResume { get; set; }
    }
}
