using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Keywords;

namespace Keyrox.Scripting.Events {
    public class SelectedKeyChanged : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedKeyChanged"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public SelectedKeyChanged(Keyword key) {
            this.SelectedKeyword = key;
        }

        /// <summary>
        /// Gets or sets the selected keyword.
        /// </summary>
        /// <value>The selected keyword.</value>
        public Keyword SelectedKeyword { get; private set; }

    }
}
