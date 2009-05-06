using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KTibiaX.Modules {
    [Serializable]
    public class ScriptKeyword {

        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Text { get; set; }

        [XmlElement]
        public string Description { get; set; }

        [XmlElement]
        public string Parameters { get; set; }

        [XmlElement]
        public string Examples { get; set; }

        [XmlElement]
        public int ImageIndex { get; set; }

        [XmlElement]
        public bool PlayerAttribute { get; set; }

        [XmlElement]
        public bool ItemParam { get; set; }

        [XmlElement]
        public bool AddQuotes { get; set; }

        [XmlElement]
        public bool RegionName { get; set; }

        public override string ToString() {
            return Title != string.Empty ? Title : base.ToString();
        }
    }
    public class ScriptKeywordCollection : List<ScriptKeyword> {

        public ScriptKeywordCollection() { }

        public ScriptKeywordCollection(IEnumerable<ScriptKeyword> items) {
            this.AddRange(items);
        }

        public ScriptKeyword this[string text] {
            get {
                var res = this.Where(key => key.Text == text);
                return res.Count() > 0 ? res.First() : null;
            }
        }

        

    }
}
