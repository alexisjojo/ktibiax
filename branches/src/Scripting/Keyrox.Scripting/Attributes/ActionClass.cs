using System;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true), Serializable]
    public class ActionClass : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionClass"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        public ActionClass(string Name) {
            this.ActionName = Name;
        }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        public string ActionName { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return !string.IsNullOrEmpty(ActionName) ? ActionName : base.ToString();
        }
    }
}
