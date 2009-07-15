using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Keyrox.Scripting.Actions.Components.Model {
    [Serializable]
    public class KillRune {

        [XmlAttribute]
        public string Creature { get; set; }

        [XmlAttribute]
        public uint RuneID { get; set; }

        [XmlAttribute]
        public int Delay { get; set; }

        [XmlAttribute]
        public int MinHP { get; set; }
    }
}
