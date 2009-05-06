using System;
using System.Linq;
using System.Windows.Forms;
using KTibiaX.Modules;
using KTibiaX.Windows.Dialogs;
using Tibia.Memory;

namespace KTibiaX.Windows.Configuration {
    public partial class frm_Address : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Address"/> class.
        /// </summary>
        public frm_Address() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Address control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Address_Load(object sender, EventArgs e) {
            LoadCollection(true);
        }

        #region "[rgn] Public Properties   "
        private bool AllowChangeAddress { get; set; }
        public AddressDTO CurrentAddress { get; set; }
        public AddressDTOCollection CurrentCollection { get; set; }
        #endregion

        #region "[rgn] Form Ctrl Handlers  "
        private void ddlVersion_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlVersion.SelectedIndex > -1) {                
                var addr = ddlVersion.GetSelectedValue<AddressDTO>();
                CurrentAddress = addr;
                gridAddress.SelectedObject = addr;
                AllowChangeAddress = true;
            }
        }
        private void ddlVersion_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e) {
            if (!AllowChangeAddress && ddlVersion.SelectedIndex > -1) {
                var res = MessageBox.Show("Save current changes?", "Changing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes) {
                    Save(false);
                }
                else if (res == DialogResult.Cancel) {
                    e.Cancel = true;
                }
            }
            AllowChangeAddress = false;
        }
        private void btnDel_Click(object sender, EventArgs e) {
            Delete();
        }
        private void btnNew_Click(object sender, EventArgs e) {
            AddNew();
        }
        private void gridAddress_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e) {
            AllowChangeAddress = false;
        }
        #endregion

        /// <summary>
        /// Loads the collection.
        /// </summary>
        /// <param name="selectFirstItem">if set to <c>true</c> [select first item].</param>
        private void LoadCollection(bool selectFirstItem) {
            CurrentCollection = AddressFileProvider.GetAddressess();
            ddlVersion.Properties.Items.Clear();
            gridAddress.SelectedObject = null;

            if (CurrentCollection != null) {
                foreach (var addr in CurrentCollection) {
                    ddlVersion.AddNewItem(addr.ClientVersion, addr);
                }
                if (selectFirstItem) ddlVersion.SelectedIndex = 0;
            }
         }

        /// <summary>
        /// Adds the new.
        /// </summary>
        private void AddNew() {
            var frmVersion = new frm_VersionName();
            frmVersion.ShowDialog();
            if (frmVersion.NewVersion != string.Empty) {
                if (CurrentCollection == null) {
                    CurrentCollection = new AddressDTOCollection();
                }
                var res = (from addr in CurrentCollection where addr.ClientVersion == frmVersion.NewVersion select addr);
                if (res.Count() == 0) {
                    CurrentAddress = new AddressDTO() { ClientVersion = frmVersion.NewVersion };
                    CurrentCollection.Add(CurrentAddress);
                    Save(false);
                    LoadCollection(false);
                    AllowChangeAddress = true;
                    ddlVersion.SelectComboItem(CurrentAddress.ClientVersion);
                }
                else {
                    MessageBox.Show("This version already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AllowChangeAddress = true;
                    ddlVersion.SelectComboItem(res.First().ClientVersion);
                }
            }
        }


        /// <summary>
        /// Deletes this instance.
        /// </summary>
        private void Delete() {
            var res = MessageBox.Show("Are you shure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) {
                foreach (var addr in CurrentCollection) {
                    if (addr.ClientVersion == CurrentAddress.ClientVersion) {
                        CurrentCollection.Remove(addr);
                        CurrentAddress = null;
                        AddressFileProvider.SaveAddress(CurrentCollection);
                        LoadCollection(true);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save(bool showMessage) {
            if (CurrentCollection != null && CurrentAddress != null) {
                foreach (var addr in CurrentCollection) {
                    if (addr.ClientVersion != CurrentAddress.ClientVersion)
                        continue;

                    CurrentCollection.Remove(addr);
                    break;
                }
                CurrentCollection.Add(CurrentAddress);
                AddressFileProvider.SaveAddress(CurrentCollection);
            }
            if (showMessage) { MessageBox.Show("Address sucefull saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        /// <summary>
        /// Handles the FormClosing event of the frm_Address control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Address_FormClosing(object sender, FormClosingEventArgs e) {
            var res = MessageBox.Show("Save current changes?", "Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) {
                Save(false);
            }
            else if (res == DialogResult.Cancel) {
                e.Cancel = true;
            }
        }


    }
}