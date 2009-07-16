using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Parser {
    public class ScriptFile {

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the about.
        /// </summary>
        /// <value>The about.</value>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the tibia.
        /// </summary>
        /// <value>The tibia.</value>
        public string TibiaVersion { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public IList<ScriptLine> Rows { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public ScriptState State { get; set; }

        /// <summary>
        /// Gets or sets the script info.
        /// </summary>
        /// <value>The script info.</value>
        public ScriptInfo ScriptInfo { get; set; }

        /// <summary>
        /// Gets the next line.
        /// </summary>
        /// <param name="currentLine">The current line.</param>
        /// <returns></returns>
        public ScriptLine GetNextLine(int currentLine) {
            if (Rows.Count > (currentLine + 1)) {
                return Rows[currentLine + 1];
            }
            return null;
        }

    }
}
