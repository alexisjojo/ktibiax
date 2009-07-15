using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Events {
    public class NumberEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberEventArgs"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public NumberEventArgs(int value) {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; set; }

    }
}
