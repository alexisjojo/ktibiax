using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Actions;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Player {
    [PropClass("Player Properties", 7)]
    public class PlayerProperties : ITibiaAction {

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
        public ScriptInfo Script { get; set; }
        #endregion

        [PropItem("Name", "[NAME]", "Returns the Player Name.")]
        public string Name { get { return Client.Features.Player.Name; } }

        [PropItem("Hit points", "[HP]", "Returns the Player Hit points.")]
        public string HP { get { return Client.Features.Player.Hp.Quantity.ToString(); } }

        [PropItem("Mana points", "[MP]", "Returns the Player Mana points.")]
        public string Mana { get { return Client.Features.Player.Mana.Quantity.ToString(); } }

        [PropItem("Level", "[LVL]", "Returns the Player Level.")]
        public string Level { get { return Client.Features.Player.Level.Value.ToString(); } }

        [PropItem("Magic Level", "[ML]", "Returns the Player Magic Level.")]
        public string MagicLevel { get { return Client.Features.Player.MagicLevel.ToString(); } }

        [PropItem("Soul points", "[SOUL]", "Returns the Player Soul points.")]
        public string Soul { get { return Client.Features.Player.Soul.ToString(); } }

        [PropItem("Cap points", "[CAP]", "Returns the Player Capacity points.")]
        public string CAP { get { return Client.Features.Player.Cap.ToString(); } }

        [PropItem("Stamina", "[STAMINA]", "Returns the Player Stamina.")]
        public string Stamina { get { return Client.Features.Player.Stamina.ToString(); } }
    }
}
