using System;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Parser;
using Keyrox.Shared.Objects;
using Keyrox.Scripting.Actions.Contracts;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Kill Configuration")]
    public class KillActions : IKillActions {

        #region ITibiaAction Members
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public Tibia.Client.TibiaClient Client { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public Keyrox.Scripting.Parser.ScriptInfo Script { get; set; }
        #endregion

        [ActionTitle("Kill", "Determines which monster will be attacked.\nYou can also use 'Kill Priority', 'Kill Spell' or 'Kill Rune' instead of.")]
        [ActionConfig(ImageIndex = 0, InputText = "kill(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionExamples("kill(\"Rat\")", "kill(\"Toad\")", "kill(\"Troll\")")]
        public ScriptActionResult Kill(string[] args) {
            Script.SetKillPriority(args[0], 0);
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Kill Priority", "Sets the priority attack of a creature.\nThis determines which monster will be attacked first.")]
        [ActionConfig(ImageIndex = 0, InputText = "killprior(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionParameter("Priority", "The value of the priority.", 1)]
        [ActionExamples("killprior(\"Hydra\", +5)", "killprior(\"Snake\", -1)")]
        public ScriptActionResult KillPrior(string[] args) {
            Script.SetKillPriority(args[0], args[1].ToInt32());
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Kill Spell", "Sets the spell to kill a particular creature.")]
        [ActionConfig(ImageIndex = 0, InputText = "killspell(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionParameter("Spell", "The spell words.", 1, NeedQuotes = true)]
        [ActionParameter("Delay", "The delay in seconds between the invocations.", 2)]
        [ActionExamples("killspell(\"Scorpion\", \"exori vis\", 2)")]
        public ScriptActionResult KillSpell(string[] args) {
            Script.SetKillSpell(new Keyrox.Scripting.Actions.Components.Model.KillSpell(){
               Creature = args[0], 
               Spell = args[1], 
               Delay = args[2].ToInt32()
            });
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Kill Rune", "Sets the rune to kill a particular creature.")]
        [ActionConfig(ImageIndex = 0, InputText = "killrune(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionParameter("Rune ID", "The id of the rune.", 1)]
        [ActionParameter("Delay", "The delay in seconds between the shoot attacks.", 2)]
        [ActionParameter("Min HP %", "The minimun hp percentage to continue shooting against the creature.", 3)]
        [ActionExamples("killrune(\"Hydra\", 3155, 2, 100)", "killrune(\"Hydra\", {SD}, 2, 100)")]
        public ScriptActionResult KillRune(string[] args) {
            Script.SetKillRune(new Keyrox.Scripting.Actions.Components.Model.KillRune() {
                Creature = args[0],
                RuneID = args[1].ToUInt32(),
                Delay = args[2].ToInt32(),
                MinHP = args[3].ToInt32()
            });
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Kill Only My Creatures", "Determines the attack only against creatures that are attacking you.")]
        [ActionConfig(ImageIndex = 9, InputText = "killonlymycreatures()", CarretPosition = -1)]
        [ActionParameter("true or false", "True to kill only creatures attaking you, otherwise false.", 0)]
        [ActionExamples("killonlymycreatures(true)", "killonlymycreatures(false)")]
        public ScriptActionResult KillOnlyMyCreatures(string[] args) {
            Script.SetParam("KillOnlyMyCreatures", "true");
            return new ScriptActionResult() { Success = true };
        }
        
        [ActionTitle("Ignore Creature", "Sets a creature that will not be attacked.")]
        [ActionConfig(ImageIndex = 2, InputText = "ignore(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of  the creature.", 0, NeedQuotes = true)]
        [ActionExamples("ignore(\"Rabbit\")", "ignore(\"deer\")")]
        public ScriptActionResult Ignore(string[] args) {
            Script.SetIgnore(args[0]);
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Avoid Front", "Sets a creature to be not attacked by front.\n	The attacks will be granted only on the diagonal.")]
        [ActionConfig(ImageIndex = 15, InputText = "avoidfront(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of  the creature.", 0, NeedQuotes = true)]
        [ActionExamples("avoidfront(\"Dragon\")")]
        public ScriptActionResult AvoidFront(string[] args) {
            Script.SetAvoidFront(args[0]);
            return new ScriptActionResult() { Success = true };
        }
    }
}
