using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Keyrox.Scripting.Controls {
    public partial class ScriptBox : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptBox"/> class.
        /// </summary>
        public ScriptBox() {
            InitializeComponent();
            Document = new ScriptDocument();
            Document.Setup(this);

            scriptEditor1.ScrollBars = ScrollBars.Both;
            scriptEditor1.HighLightedLineColor = Color.WhiteSmoke;
            scriptEditor1.SupressAutoComplete = true;
            scriptEditor1.SupressInfoTips = true;
            scriptEditor1.AutoListAutoSelect = false;
        }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>The document.</value>
        public ScriptDocument Document { get; set; }

        /// <summary>
        /// Gets the editor.
        /// </summary>
        /// <value>The editor.</value>
        public ScriptEditor Editor { get { return scriptEditor1; } }

    }
}
