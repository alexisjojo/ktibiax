using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KTibiaX.Core.Model.DTO {
    [Serializable]
    public class CustomListItem {

        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string Description { get; set; }
    }

    [Serializable]
    public class CustomListItemCollection : List<CustomListItem> {

    }
}
