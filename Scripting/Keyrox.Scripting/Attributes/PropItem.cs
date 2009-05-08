using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true), Serializable]
    public class PropItem : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="PropItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="inputText">The input text.</param>
        /// <param name="description">The description.</param>
        public PropItem(string name, string inputText, string description) {
            this.Name = name;
            this.InputText = inputText;
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the input text.
        /// </summary>
        /// <value>The input text.</value>
        public string InputText { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        
    }
}
