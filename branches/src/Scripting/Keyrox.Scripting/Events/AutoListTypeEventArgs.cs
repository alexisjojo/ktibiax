using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Events {
    public class AutoListTypeEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoListTypeEventArgs"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AutoListTypeEventArgs(AutoListType type) {
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public AutoListType Type { get; set; }

    }
}
