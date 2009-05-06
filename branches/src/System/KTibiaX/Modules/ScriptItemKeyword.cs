using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KTibiaX.Modules {
    [Serializable]
    public class ScriptItemKeyword {

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int ID { get; set; }

        public override string ToString() {
            return Name != string.Empty ? Name : base.ToString();
        }
    }
    public class ScriptItemKeywordCollection : List<ScriptItemKeyword> {
        
        public void Add(string name, int id) {
            var res = this.Where(item => item.ID == id && item.Name.ToLower() == name.ToLower());
            if (res.Count() == 0) {
                this.Add(new ScriptItemKeyword(){ Name = name, ID = id });
            }
        }
    }
}
