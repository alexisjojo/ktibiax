using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true), Serializable]
    public class ActionTitle : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTitle"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        public ActionTitle(string title, string description) {
            this.Title = title;
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return !string.IsNullOrEmpty(Title) ? Title : base.ToString();
        }

    }
}
