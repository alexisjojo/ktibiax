using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Windows.Forms;
using System.ComponentModel;
using Keyrox.Scripting.Controls;

namespace Keyrox.Scripting.Controls {
    public class ScriptToolTipProvider : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptToolTipProvider"/> class.
        /// </summary>
        /// <param name="editor">The editor.</param>
         public ScriptToolTipProvider(ScriptEditor editor) {
            this.Editor = editor;
        }

        #region "[rgn] Properties "
         public ScriptEditor Editor { get; set; }
        #endregion

    }
}
