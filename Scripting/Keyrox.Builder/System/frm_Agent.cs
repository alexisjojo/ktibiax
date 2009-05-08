using System;
using System.Windows.Forms;
using Keyrox.Builder.Features;

namespace Keyrox.Builder {
    public partial class frm_Agent : Form {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Agent"/> class.
        /// </summary>
        public frm_Agent() { InitializeComponent(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Agent"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public frm_Agent(string args) : this() { FilePath = args; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        private string FilePath { get; set; }

        /// <summary>
        /// Handles the Load event of the frm_Agent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Agent_Load(object sender, EventArgs e) {
            var frmEditor = new frm_Editor();
            frmEditor.Show();

            try {
                var fi = new System.IO.FileInfo(FilePath);
                if (fi.Exists) { frmEditor.LoadScript(fi.FullName); }
            }
            catch (ArgumentException) { }

            TmForms.Start();
        }

        /// <summary>
        /// Handles the Tick event of the TmForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TmForms_Tick(object sender, EventArgs e) {
            if (Application.OpenForms.Count == 1) {
                Application.ExitThread();
            }
        }

    }
}
