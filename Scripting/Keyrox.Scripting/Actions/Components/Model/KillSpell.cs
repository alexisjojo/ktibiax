using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Keyrox.Scripting.Actions.Components.Model {
    [Serializable]
    public class KillSpell {

        [XmlAttribute]
        public string Creature { get; set; }

        [XmlAttribute]
        public string Spell { get; set; }

        [XmlAttribute]
        public int Delay { get; set; }

    }
}
