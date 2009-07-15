using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Keyrox.Scripting.Actions;
using Keyrox.Scripting.Actions.Components.Model;
using Keyrox.Scripting.Keywords;
using Keyrox.Scripting.Player;
using Keyrox.Scripting.Util;
using Tibia.Client;

namespace Keyrox.Scripting.Parser {
    public class ScriptInfo {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInfo"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public ScriptInfo(TibiaClient client, ScriptFile script) {
            this.TibiaClient = client;
            this.Script = script;
            InitializeScriptInfo();
        }

        #region "[rgn] Properties     "
        public TibiaClient TibiaClient { get; private set; }
        public ScriptFile Script { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }
        public Dictionary<string, ScriptSection> Sections { get; private set; }

        public Dictionary<string, int> KillPriority { get; private set; }
        public Dictionary<string, KillRune> KillRunes { get; private set; }
        public Dictionary<string, KillSpell> KillSpells { get; private set; }

        public List<uint> Loot { get; private set; }
        public StringCollection Ignore { get; private set; }
        public StringCollection AvoidFront { get; set; }

        public PlayerProperties PlayerProperties { get; private set; }
        public ItemKeywordCollection CustomItems { get; private set; }
        #endregion

        #region "[rgn] Helper Methods "
        public T GetAction<T>() where T : ITibiaAction {
            return (T)Context.GetAction(typeof(T), TibiaClient, this);
        }
        public string[] GetFunctionArguments(string text) {
            var sindex = text.IndexOf("(");
            if (sindex > -1) {
                return text.TrimEnd(' ').Substring(sindex, text.Length - 1).Split(new[] { ',' });
            }
            return null;
        }
        public string RemoveArgumentOperators(string text) {
            return text
                .Replace("{", "").Replace("}", "")
                .Replace("[", "").Replace("]", "")
                .Replace("\"", "");
        }
        public string[] NormalizeActionArguments(string[] args) {
            var res = args;
            for (int i = 0; i < res.Length; i++) {
                var arg = res[i];

                //Check Custom Items.
                if (arg.StartsWith("{")) {
                    if (CustomItems.GetItemByInputText(arg) != null) {
                        arg = CustomItems.GetItemByInputText(arg).ID.ToString();
                    }
                    else { throw new ScriptActionException(this, string.Format("Invalid custom item: {0}", arg)); }
                }

                //Check Player Properties.
                if (arg.StartsWith("[")) {
                    arg = RemoveArgumentOperators(arg);
                    if (!string.IsNullOrEmpty(PlayerProperties.GetProperty(arg))) {
                        arg = PlayerProperties.GetProperty(arg);
                    }
                    else { throw new ScriptActionException(this, string.Format("Invalid player property: {0}", arg)); }
                }

                //Check Get Param
                if (arg.StartsWith("getparam")) {
                    var fargs = GetFunctionArguments(arg);
                    var fres = GetAction<ParamActions>().GetParam(fargs);
                    if (fres.Success) { arg = fres.ReturnValue.ToString(); }
                }

                //Check Count Item
                if (arg.StartsWith("countitem")) {
                    var fargs = GetFunctionArguments(arg);
                    var fres = GetAction<ParamActions>().CountItem(fargs);
                    if (fres.Success) { arg = fres.ReturnValue.ToString(); }
                }

                //Check Get Value
                if (arg.StartsWith("getvalue")) {
                    var fargs = GetFunctionArguments(arg);
                    var fres = GetAction<EvalActions>().GetValue(fargs);
                    if (fres.Success) { arg = fres.ReturnValue.ToString(); }
                }
                res[i] = arg;
            }
            return res;
        }
        #endregion

        /// <summary>
        /// Initializes the script info.
        /// </summary>
        public void InitializeScriptInfo() {
            Parameters = new Dictionary<string, string>();
            Sections = new Dictionary<string, ScriptSection>();

            KillPriority = new Dictionary<string, int>();
            KillRunes = new Dictionary<string, KillRune>();
            KillSpells = new Dictionary<string, KillSpell>();

            Loot = new List<uint>();
            Ignore = new StringCollection();
            AvoidFront = new StringCollection();

            PlayerProperties = new PlayerProperties();
            CustomItems = ItemKeywordCollection.Current;

            foreach (var row in Script.Rows) {
                if (row.IsSection && !Sections.ContainsKey(row.Section.Name)) {
                    Sections.Add(row.Section.Name, row.Section);
                }
            }
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ScriptLine GetLine(int index) {
            var res = Script.Rows.Where(r => r.LineIndex == index);
            return res.Count() > 0 ? res.First() : null;
        }

        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ScriptSection GetSection(string name) {
            if (Sections.ContainsKey(name)) {
                return Sections[name];
            }
            return null;
        }

        /// <summary>
        /// Gets the param.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetParam(string name) {
            if (Parameters.ContainsKey(name)) {
                return Parameters[name];
            }
            return string.Empty;
        }

        /// <summary>
        /// Sets the param.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void SetParam(string name, string value) {
            if (Parameters.ContainsKey(name)) {
                Parameters[name] = value;
            }
            else { Parameters.Add(name, value); }
        }

        /// <summary>
        /// Sets the kill priority.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="priority">The priority.</param>
        public void SetKillPriority(string creature, int priority) {
            if (KillPriority.ContainsKey(creature)) {
                KillPriority[creature] = priority;
            }
            else { KillPriority.Add(creature, priority); }
        }

        /// <summary>
        /// Sets the kill rune.
        /// </summary>
        /// <param name="rune">The rune.</param>
        public void SetKillRune(KillRune rune) {
            if (KillRunes.ContainsKey(rune.Creature)) {
                KillRunes[rune.Creature] = rune;
            }
            else { KillRunes.Add(rune.Creature, rune); }
        }

        /// <summary>
        /// Sets the kill spell.
        /// </summary>
        /// <param name="spell">The spell.</param>
        public void SetKillSpell(KillSpell spell) {
            if (KillSpells.ContainsKey(spell.Creature)) {
                KillSpells[spell.Creature] = spell;
            }
            else { KillSpells.Add(spell.Creature, spell); }
        }

        /// <summary>
        /// Sets the loot.
        /// </summary>
        /// <param name="itemID">The item ID.</param>
        public void SetLoot(uint itemID) {
            if (!Loot.Contains(itemID)) {
                Loot.Add(itemID);
            }
        }

        /// <summary>
        /// Sets the ignore.
        /// </summary>
        /// <param name="creature">The creature.</param>
        public void SetIgnore(string creature) {
            if (!Ignore.Contains(creature)) {
                Ignore.Add(creature);
            }
        }

        /// <summary>
        /// Sets the avoid front.
        /// </summary>
        /// <param name="creature">The creature.</param>
        public void SetAvoidFront(string creature) {
            if (!AvoidFront.Contains(creature)) {
                AvoidFront.Add(creature);
            }
        }

    }
}
