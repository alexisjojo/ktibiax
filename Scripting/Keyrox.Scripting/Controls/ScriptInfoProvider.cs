using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Actions.Components.Model;
using Keyrox.Scripting.Events;
using Keyrox.Scripting.Keywords;

namespace Keyrox.Scripting.Controls {
    public class ScriptInfoProvider : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInfoProvider"/> class.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public ScriptInfoProvider(ScriptFile script) {
            this.Script = script;
            InitializeScriptInformation();
        }

        /// <summary>
        /// Gets or sets the editor.
        /// </summary>
        /// <value>The editor.</value>
        public ScriptFile Script { get; private set; }

        #region "[rgn] Properties    "
        public bool KillOnlyMyCreatuers { get; private set; }
        public Dictionary<string, ScriptLine> Sections { get; private set; }

        public Dictionary<string, KillSpell> KillSpell { get; private set; }
        public Dictionary<string, KillRune> KillRune { get; private set; }

        public Dictionary<string, string> ScriptParams { get; private set; }
        public Dictionary<string, int> KillPriority { get; private set; }

        public List<uint> Loot { get; private set; }
        public List<string> Ignored { get; private set; }
        public List<string> FrontAvoided { get; private set; }

        public Dictionary<string, string> CustomItems { get; private set; }
        public Dictionary<string, string> PlayerProperties { get; private set; }
        #endregion

        #region "[rgn] Public Events "
        public event EventHandler<ScriptLineEventArgs> OnMoveToLine;
        #endregion

        /// <summary>
        /// Initializes the script information.
        /// </summary>
        public void InitializeScriptInformation() {

        }

        /// <summary>
        /// Jumps to section.
        /// </summary>
        /// <param name="name">The name.</param>
        public void JumpToSection(string name) {
            if (Sections.ContainsKey(name)) {
                if (OnMoveToLine != null) { OnMoveToLine(this, new ScriptLineEventArgs(Sections[name])); }
            }
            else { throw new ArgumentException("Section not found: '" + name + "'!"); }
        }

        /// <summary>
        /// Jumps to line.
        /// </summary>
        /// <param name="lineNumber">The line number.</param>
        public void JumpToLine(int lineNumber) {
            if (Script.Rows.Count > lineNumber) {
                if (OnMoveToLine != null) { OnMoveToLine(this, new ScriptLineEventArgs(Script.Rows[lineNumber])); }
            }
            else { throw new ArgumentException("Line not found: '" + lineNumber + "'!"); }
        }
        
        /// <summary>
        /// Normalizes the row arguments.
        /// </summary>
        public void NormalizeRowArguments() {
            foreach (var row in Script.Rows) {
                foreach (var argument in row.Args) {

                    if (argument.StartsWith("{")) {
                        //TODO:
                    }
                    else if (argument.StartsWith("[")) {
                        //TODO:
                    }
                    else if (argument.StartsWith("getparam")) {
                        //TODO:
                    }
                    else if (argument.StartsWith("getvalue")) {
                        //TODO:
                    }
                    else if (argument.StartsWith("countitem")) {
                        //TODO:
                    }

                }
            }
        }

    }
}
