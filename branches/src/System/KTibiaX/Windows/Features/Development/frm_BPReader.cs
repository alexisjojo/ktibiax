using System;
using KTibiaX.UI.Controls;
using KTibiaX.UI.Extensions;
using Keyrox.Shared.Objects;
using Tibia.Features.Model.Items;
using Tibia.Features.Model;
using DevExpress.XtraEditors.Controls;

namespace KTibiaX.Windows.Features.Development {
    public partial class frm_BPReader : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_BPReader"/> class.
        /// </summary>
        public frm_BPReader() {
            InitializeComponent();
        }

        #region "[rgn] Events "
        private void frm_BPReader_Load(object sender, EventArgs e) {
            LoadContainers();
        }
        private void ddlContainers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Undo) {
                LoadContainers();
            }
        }
        private void ddlContainers_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlContainers.SelectedIndex > -1) {
                LoadItems((Container)ddlContainers.GetSelectedValue());
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e) {
            if (ddlContainers.SelectedIndex > -1) {
                LoadItems((Container)ddlContainers.GetSelectedValue());
            }
        }
        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }
        #endregion


        /// <summary>
        /// Loads the containers.
        /// </summary>
        protected void LoadContainers() {
            var bps = TibiaClient.Features.Player.Containers;
            ddlContainers.ClearItems();
            lstItems.Items.Clear();
            foreach (var item in bps) {
                ddlContainers.AddItem(string.Concat("Container ", item.Position), item);
            }
        }

        /// <summary>
        /// Loads the items.
        /// </summary>
        /// <param name="container">The container.</param>
        protected void LoadItems(Container container) {
            var slots = container.Slots;
            lstItems.Items.Clear();
            lstItems.Tag = slots;
            foreach (var slot in slots) {
                if (slot.Item.Id > 0) {
                    lstItems.Items.Add(string.Concat(slot.Item.Id, " [", slot.Item.Count, "]"));
                }
            }
        }

    }
}