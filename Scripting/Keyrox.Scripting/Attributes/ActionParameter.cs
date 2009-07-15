using System;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true), Serializable]
    public class ActionParameter : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="index">The index.</param>
        public ActionParameter(string name, string description, string defaultValue, int index) {
            this.Name = name;
            this.DefaultValue = defaultValue;
            this.Description = description;
            this.Index = index;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="index">The index.</param>
        public ActionParameter(string name, string description, int index)
            : this(name, description, null, index) {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [need quotes].
        /// </summary>
        /// <value><c>true</c> if [need quotes]; otherwise, <c>false</c>.</value>
        public bool NeedQuotes { get; set; }

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
