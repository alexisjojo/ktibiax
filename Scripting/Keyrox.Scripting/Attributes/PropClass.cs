using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true), Serializable]
    public class PropClass : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="PropClass"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public PropClass(string name, int imageIndex) {
            this.Name = name;
            this.ImageIndex = imageIndex;
        }
        
        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        public int ImageIndex { get; set; }

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
