using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Attributes {
    public class ActionConfig : Attribute {

        /// <summary>
        /// Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        public int ImageIndex { get; set; }

        /// <summary>
        /// Gets or sets the input text.
        /// </summary>
        /// <value>The input text.</value>
        public string InputText { get; set; }

        /// <summary>
        /// Gets or sets the auto list.
        /// </summary>
        /// <value>The auto list.</value>
        public AutoListType AutoList { get; set; }

        /// <summary>
        /// Gets or sets the carret identation.
        /// </summary>
        /// <value>The carret identation.</value>
        public int CarretPosition { get; set; }
    }
}
