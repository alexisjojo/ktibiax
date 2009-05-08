using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Builder.Events {
    public class TextEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEventArgs"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextEventArgs(string text) {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        
    }
}
