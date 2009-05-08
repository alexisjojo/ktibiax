using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Kill Configuration")]
    public class KillActions : ITibiaAction {

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

        [ActionTitle("Kill Priority", "Sets the priority attack of a creature.\nThis determines which monster will be attacked first.")]
        [ActionConfig(ImageIndex = 0, InputText = "killprior(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionParameter("Priority", "The value of the priority.", 1)]
        [ActionExamples("killprior(\"Hydra\", +5)", "killprior(\"Snake\", -1)")]
        public void KillPrior(string[] args) {
        }

        [ActionTitle("Kill Spell", "Sets the spell to kill a particular creature.")]
        [ActionConfig(ImageIndex = 0, InputText = "killspell(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionParameter("Spell", "The spell words.", 1, NeedQuotes = true)]
        [ActionParameter("Delay", "The delay in seconds between the invocations.", 2)]
        [ActionExamples("killspell(\"Scorpion\", \"exori vis\", 2)")]
        public void KillSpell(string[] args) {
        }

        [ActionTitle("Kill Rune", "Sets the rune to kill a particular creature.")]
        [ActionConfig(ImageIndex = 0, InputText = "killrune(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of the creature.", 0, NeedQuotes = true)]
        [ActionParameter("Rune ID", "The id of the rune.", 1)]
        [ActionParameter("Delay", "The delay in seconds between the shoot attacks.", 2)]
        [ActionParameter("Min HP %", "The minimun hp percentage to continue shooting against the creature.", 3)]
        [ActionExamples("killrune(\"Hydra\", 3155, 2, 100)", "killrune(\"Hydra\", {SD}, 2, 100)")]
        public void KillRune(string[] args) {
        }

        [ActionTitle("Kill Only My Creatures", "Determines the attack only against creatures that are attacking you.")]
        [ActionConfig(ImageIndex = 9, InputText = "killonlymycreatures()", CarretPosition = -1)]
        [ActionParameter("true or false", "True to kill only creatures attaking you, otherwise false.", 0)]
        [ActionExamples("killonlymycreatures(true)", "killonlymycreatures(false)")]
        public void KillOnlyMyCreatures(string[] args) {
        }
        
        [ActionTitle("Ignore Creature", "Sets a creature that will not be attacked.")]
        [ActionConfig(ImageIndex = 2, InputText = "ignore(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of  the creature.", 0, NeedQuotes = true)]
        [ActionExamples("ignore(\"Rabbit\")", "ignore(\"deer\")")]
        public void Ignore(string[] args) {
        }

        [ActionTitle("Avoid Front", "Sets a creature to be not attacked by front.\n	The attacks will be granted only on the diagonal.")]
        [ActionConfig(ImageIndex = 15, InputText = "avoidfront(\"\")", CarretPosition = -2)]
        [ActionParameter("Creature", "The name of  the creature.", 0, NeedQuotes = true)]
        [ActionExamples("avoidfront(\"Dragon\")")]
        public void AvoidFront(string[] args) {
        }
    }
}
