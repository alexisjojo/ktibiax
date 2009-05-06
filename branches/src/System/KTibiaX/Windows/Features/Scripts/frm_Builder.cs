using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Keyrox.Shared.Files;

namespace KTibiaX.Windows.Features.Scripts {
    public partial class frm_Builder : DevExpress.XtraBars.Ribbon.RibbonForm {
        /// <summary>
        /// Initializ]es a new instance of the <see cref="frm_Builder"/> class.
        /// </summary>
        public frm_Builder() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Builder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Builder_Load(object sender, EventArgs e) {
            propertyGridControl1.SelectedObject = SyntaxBox1.SyntaxBox;
            SyntaxBox1.SyntaxDocument.SyntaxFile = System.IO.Path.Combine(Application.StartupPath, "ScriptConfig\\csharp.syn");
         }

        /// <summary>
        /// Gets or sets the current script.
        /// </summary>
        /// <value>The current script.</value>
        public string CurrentScript { get; set; }

        #region "[rgn] Menu Items "
        private void btnNew_ItemClick(object sender, ItemClickEventArgs e) {
            CurrentScript = string.Empty;
            SyntaxBox1.SyntaxDocument.Text = GetScriptTemplate();
            SyntaxBox1.Refresh();
        }
        private void btnOpen_ItemClick(object sender, ItemClickEventArgs e) {
            OpenScript();
        }

        private void btnValidate_ItemClick(object sender, ItemClickEventArgs e) {

        }
        private void btnCompile_ItemClick(object sender, ItemClickEventArgs e) {

        }
        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e) {

        }
        private void btnSave_ItemClick(object sender, ItemClickEventArgs e) {
            SaveScript();
        }
        #endregion

        #region "[rgn] Default Template "
        public string GetScriptTemplate() {
            var fi = new System.IO.FileInfo(System.IO.Path.Combine(Application.StartupPath, "ScriptConfig\\template.txt"));
            if (fi.Exists) {
                return fi.Read();
            }
            return string.Empty;
        }
        #endregion

        /// <summary>
        /// Saves the script.
        /// </summary>
        public void SaveScript() {
            System.IO.FileInfo fi = null;
            if (CurrentScript != string.Empty) {
                fi = new System.IO.FileInfo(CurrentScript);
            }
            else {
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != string.Empty) {
                    fi = new System.IO.FileInfo(saveFileDialog1.FileName);
                }
                else { return; }
            }
            fi.Write(SyntaxBox1.SyntaxDocument.Text);
            CurrentScript = fi.FullName;
            MessageBox.Show("Script was saved successfully!");
        }

        /// <summary>
        /// Opens the script.
        /// </summary>
        public void OpenScript() {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty) {
                var fi = new System.IO.FileInfo(openFileDialog1.FileName);
                if (fi.Exists) {
                    var content = fi.Read();
                    SyntaxBox1.SyntaxDocument.Text = content;
                    SyntaxBox1.Refresh();
                    CurrentScript = openFileDialog1.FileName;
                }
            }
        }

    }
}