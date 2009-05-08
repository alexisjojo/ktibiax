using System.Xml.Serialization;

namespace Keyrox.Scripting.Keywords {
    public class ItemKeyword {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlElement]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [XmlElement]
        public int ID { get; set; }

        /// <summary>
        /// Gets the input text.
        /// </summary>
        /// <value>The input text.</value>
        [XmlIgnore]
        public string InputText { get { return string.Concat("{", Name.ToUpper().Trim(), "}"); } }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlElement]
        public string Description { get { return string.Concat("Returns the ID of the item ", Name); } }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return !string.IsNullOrEmpty(Name) ? Name : base.ToString();
        }
    }

}
