using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Client;
using Keyrox.Scripting.Actions.Components.Model;

namespace Keyrox.Scripting.Parser {
    public class ScriptInfo {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInfo"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public ScriptInfo(TibiaClient client, ScriptFile script) {
            this.TibiaClient = client;
            this.Script = script;
        }

        #region "[rgn] Properties     "
        public TibiaClient TibiaClient { get; private set; }
        public ScriptFile Script { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }
        public Dictionary<string, ScriptSection> Sections { get; private set; }

        public Dictionary<string, int> KillPriority { get; private set; }
        public Dictionary<string, KillRune> KillRunes { get; private set; }
        public Dictionary<string, KillSpell> KillSpells { get; private set; }

        public Dictionary<string, string> Player { get; private set; }
        public Dictionary<string, string> Items { get; private set; }
        #endregion

        /// <summary>
        /// Initializes the script info.
        /// </summary>
        public void InitializeScriptInfo() {
            //TODO: Load all script information.
        }

        /// <summary>
        /// Normalizes the action arguments.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public string[] NormalizeActionArguments(string[] args) {
            return null;
        }

        /// <summary>
        /// Gets the param.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetParam(string name) {
            return string.Empty;
        }

        /// <summary>
        /// Sets the param.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void SetParam(string name, string value) {
        }

        /// <summary>
        /// Sets the kill priority.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="priority">The priority.</param>
        public void SetKillPriority(string creature, int priority) {
        }

        /// <summary>
        /// Sets the kill rune.
        /// </summary>
        /// <param name="rune">The rune.</param>
        public void SetKillRune(KillRune rune) {
        }

        /// <summary>
        /// Sets the kill spell.
        /// </summary>
        /// <param name="spell">The spell.</param>
        public void SetKillSpell(KillSpell spell) {
        }

    }
}
