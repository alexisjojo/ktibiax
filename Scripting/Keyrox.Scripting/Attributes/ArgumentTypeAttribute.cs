using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ArgumentTypeAttribute : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentType"/> class.
        /// </summary>
        public ArgumentTypeAttribute() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentType"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="type">The type.</param>
        public ArgumentTypeAttribute(int index, TypeCode type) {
            this.Index = index;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public TypeCode Type { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return string.Format("({0}) {1}", Index, Type);
        }

    }
}
