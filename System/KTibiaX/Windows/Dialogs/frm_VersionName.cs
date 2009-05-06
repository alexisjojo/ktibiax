using System;
using System.Windows.Forms;

namespace KTibiaX.Windows.Dialogs {
    public partial class frm_VersionName : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_VersionName"/> class.
        /// </summary>
        public frm_VersionName() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the new version.
        /// </summary>
        /// <value>The new version.</value>
        public string NewVersion { get; set; }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (txtVersion.Text == "" || txtVersion.Text.Length != 4) {
                MessageBox.Show("Invalid client version!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NewVersion = txtVersion.Text;
            Close();
        }
    }
}