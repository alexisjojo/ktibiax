using System;
using DevExpress.XtraBars;
using Keyrox.Builder.Features;
using Keyrox.Scripting.Controls;
using Keyrox.Scripting.Parser;
using KTibiaX.UI.Controls;
using System.Windows.Forms;

namespace KTibiaX.Windows.Features.Hunt {
    public partial class frm_Cavebot : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Cavebot"/> class.
        /// </summary>
        public frm_Cavebot() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Cavebot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Cavebot_Load(object sender, EventArgs e) {
            openScriptDialog.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, "MyScripts");
        }

        #region "[rgn] Form Properties "
        public frm_Editor Editor { get; set; }
        public ScriptRunner ScriptRunner { get; set; }

        private ScriptFile script;
        public ScriptFile Script {
            get { return script; }
            set { btnRun.Enabled = value != null; script = value; }
        }
        #endregion

        #region "[rgn] Control Handler "
        private void Editor_OnCompileComplete(object sender, EventArgs e) {
            Script = Editor.CompiledScript;
        }
        private void Editor_OnInvalidateRequired(object sender, EventArgs e) {
            //this.Invalidate(true);
        }
        #endregion

        #region "[rgn] Ribbon Handler  "
        private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ExecuteScript();
        }
        private void btnPause_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ScriptRunner.Pause();
        }
        private void btnStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ScriptRunner.Stop();
        }
        private void btnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OpenScript();
        }
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmEditor = new frm_Editor();
            frmEditor.TibiaClient = TibiaClient;
            frmEditor.Show();
        }
        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmEditor = new frm_Editor();
            frmEditor.TibiaClient = TibiaClient;
            frmEditor.Show();
            frmEditor.LoadScript(Editor.CurrentScriptPath);
        }
        private void btnCompile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Editor.CompileScript();
        }
        private void btnOutput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Editor.ShowOutputWindow();
        }
        private void btnIssues_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Editor.ShowIssuesWindow();
        }
        #endregion

        #region "[rgn] Helper Methods  "
        private void OpenScript() {
            openScriptDialog.FileName = string.Empty;
            openScriptDialog.ShowDialog();
            if (openScriptDialog.FileName != string.Empty) {
                if (Editor != null) { Editor.Close(); Editor = null; }
                Editor = new frm_Editor();
                Editor.TibiaClient = TibiaClient;
                Editor.HideRibbon();
                Editor.MdiParent = this;
                Editor.Show();
                Editor.LoadScript(openScriptDialog.FileName);
                Editor.OnCompileComplete += Editor_OnCompileComplete;
                Editor.OnInvalidateRequired += Editor_OnInvalidateRequired;
                SetOpenedScriptState();
            }
        }
        private void SetRunningState() {
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnRun.Enabled = false;
            foreach (BarItemLink btn in rgFile.ItemLinks) {
                btn.Item.Enabled = false;
            }
            foreach (BarItemLink btn in rgCode.ItemLinks) {
                btn.Item.Enabled = false;
            }
        }
        private void SetStopedState() {
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRun.Enabled = true;
            foreach (BarItemLink btn in rgFile.ItemLinks) {
                btn.Item.Enabled = true;
            }
            foreach (BarItemLink btn in rgCode.ItemLinks) {
                btn.Item.Enabled = true;
            }
        }
        private void SetOpenedScriptState() {
            btnRun.Enabled = false;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            foreach (BarItemLink btn in rgFile.ItemLinks) {
                btn.Item.Enabled = true;
            }
            foreach (BarItemLink btn in rgCode.ItemLinks) {
                btn.Item.Enabled = true;
            }
        }
        #endregion

        /// <summary>
        /// Executes the script.
        /// </summary>
        public void ExecuteScript() {
            if (Script != null) {
                SetRunningState();
                ScriptRunner = new ScriptRunner(Script, TibiaClient);
                ScriptRunner.Start(false);
            }
        }
    }
}