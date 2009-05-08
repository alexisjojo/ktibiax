using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Windows.Forms;
using Keyrox.Scripting.Events;

namespace Keyrox.Scripting.Controls {
    public class ScriptEditor : SyntaxBoxControl {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptEditor"/> class.
        /// </summary>
        public ScriptEditor() {

        }

        #region "[rgn] Properties   "
        public bool SupressAutoComplete { get; set; }
        public bool SupressInfoTips { get; set; }
        #endregion

        /// <summary>
        /// Occurs when [auto list requested].
        /// </summary>
        public event EventHandler<AutoListTypeEventArgs> AutoListRequested;

        /// <summary>
        /// Shows the auto list.
        /// </summary>
        public void ShowAutoList(AutoListType type) {
            if (AutoListRequested != null) {
                AutoListRequested(this, new AutoListTypeEventArgs(type));
            }
        }

        private void InitializeComponent() {
            this.splitView.SuspendLayout();
            this.SuspendLayout();
            this.splitView.Controls.SetChildIndex(this._ActiveView, 0);
            // 
            // ScriptEditor
            // 
            this.SyntaxToolTipController.SetSuperTip(this, null);
            this.splitView.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
