using System;
using System.Windows.Forms;
using Keyrox.Scripting.Keywords;
using Keyrox.Builder.Features.Dialogs;
using Keyrox.Shared.Objects;

namespace Keyrox.Builder.Features {
    public partial class frm_Items : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Items"/> class.
        /// </summary>
        public frm_Items() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the item source.
        /// </summary>
        /// <value>The item source.</value>
        public ItemKeywordCollection ItemSource { get; set; }

        /// <summary>
        /// Handles the Load event of the frm_Items control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Items_Load(object sender, EventArgs e) {
            UpdateDataSource();
        }

        /// <summary>
        /// Handles the FormClosing event of the frm_Items control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Items_FormClosing(object sender, FormClosingEventArgs e) {
            ItemKeywordCollection.Current = ItemSource;
            ItemKeywordCollection.Save();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmNew = new frm_NewItem();
            frmNew.ShowDialog();
            UpdateDataSource();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (gridView1.FocusedRowHandle > -1) {
                var res = MessageBox.Show("Are you shure?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes) {
                    ItemKeywordCollection.Remove(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToInt32());
                    UpdateDataSource();
                }
            }
        }

        /// <summary>
        /// Updates the data source.
        /// </summary>
        private void UpdateDataSource() {
            ItemSource = ItemKeywordCollection.Current;
            gridControl1.DataSource = ItemSource;
            gridView1.RefreshData();
        }
    }
}