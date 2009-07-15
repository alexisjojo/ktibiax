using System;
using System.Linq;
using System.Windows.Forms;
using Keyrox.Scripting.Keywords;
using Keyrox.Shared.Objects;

namespace Keyrox.Builder.Features.Dialogs {
    public partial class frm_NewItem : DevExpress.XtraEditors.XtraForm {
        public frm_NewItem() {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when [item list changed].
        /// </summary>
        public event EventHandler ItemListChanged;

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e) {

            #region "[rgn] Validation "
            if (txtName.Text.Length < 2) { dxErrorProvider1.SetError(txtName, "Invalid item Name"); return; }
            else { dxErrorProvider1.SetError(txtName, ""); }
            if (txtID.Text.Length < 4) { dxErrorProvider1.SetError(txtID, "Invalid item ID"); return; }
            else { dxErrorProvider1.SetError(txtID, ""); }
            #endregion

            var item = new ItemKeyword() { Name = txtName.Text, ID = txtID.Text.ToInt32() };
            if ((ItemKeywordCollection.Current.Where(i => i.Name.ToLower() == item.Name.ToLower()).Count() > 0) ||
                (ItemKeywordCollection.Current.Where(i => i.ID == item.ID).Count() > 0)) {
                MessageBox.Show("A item with the same Name or ID already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ItemKeywordCollection.Current.Add(item);
            ItemKeywordCollection.Current.Save();
            if (ItemListChanged != null) { ItemListChanged(this, EventArgs.Empty); }
            Close();
        }
    }
}