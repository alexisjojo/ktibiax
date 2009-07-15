using System;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Attributes {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true), Serializable]
    public class ActionExamples : Attribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionExamples"/> class.
        /// </summary>
        /// <param name="examples">The examples.</param>
        public ActionExamples(params string[] examples) {
            this.Examples = examples;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionExamples"/> class.
        /// </summary>
        /// <param name="example">The example.</param>
        public ActionExamples(string example)
            : this(new[] { example }) {
        }

        /// <summary>
        /// Gets or sets the examples.
        /// </summary>
        /// <value>The examples.</value>
        public string[] Examples { get; set; }

        /// <summary>
        /// Gets the formated examples.
        /// </summary>
        /// <returns></returns>
        public string GetFormatedExamples() {
            var ex = new StringBuilder();
            ex.Append("Ex.\n");
            Examples.ToList().ForEach(i => ex.Append(string.Concat(i, "\n")));
            return ex.ToString();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return Examples != null ? string.Format("Count {0}", Examples.Length) : base.ToString();
        }

    }
}
