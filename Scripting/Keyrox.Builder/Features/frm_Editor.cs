using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Keyrox.Builder.Events;
using Keyrox.Builder.Modules;
using Keyrox.Scripting;
using Keyrox.Scripting.Keywords;
using Keyrox.Scripting.Parser;
using Keyrox.Shared.Controls;
using Keyrox.Shared.Enumerators;
using Keyrox.Shared.Files;

namespace Keyrox.Builder.Features {
    public partial class frm_Editor : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Editor"/> class.
        /// </summary>
        public frm_Editor() {
            InitializeComponent();
            SetupEditor();
        }

        /// <summary>
        /// Handles the Load event of the frm_Editor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Editor_Load(object sender, EventArgs e) {
            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "MyScripts");
            ParseProgressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            NewScript();
            TmState.Start();
        }

        #region "[rgn] Properties     "
        public FileInfo CurrentScript { get; set; }
        public ScriptParser Compiler { get; set; }
        public ScriptFile CompiledScript { get; set; }
        public event EventHandler<TextEventArgs> StatusTextChanged;
        public bool IsLoading { get; private set; }
        public bool ShowSuperTipOnMouseMove {
            get { return btnInfoTips.Down; }
            set { btnInfoTips.Down = value; }
        }
        private bool ShoulRestoreALState { get; set; }
        private ScriptState scriptState;
        public ScriptState ScriptState {
            get { return scriptState; }
            set {
                scriptState = value;
                lblScriptState.Caption = value.Description();
                lblScriptState.ImageIndex = value.GetHashCode() + 2;
            }
        }
        #endregion

        #region "[rgn] Helper Methods "
        public void SetStatusText(string text) {
            outputView1.Add(text);
            if (!text.StartsWith("-")) { lblLastOutput.Caption = text; }
            if (StatusTextChanged != null) { StatusTextChanged(this, new TextEventArgs(text)); }
        }
        public void SetStatusText(params string[] text) {
            var fulltext = string.Empty;
            text.ToList().ForEach(t => fulltext += t);
            SetStatusText(fulltext);
        }
        public void HidePanels() {
            dockOutput.HideSliding();
            dockErrors.HideSliding();
        }
        public void NewScript() {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, "ScriptConfig\\ScriptInfo.txt"));
            if (fi.Exists) { scriptBox1.Editor.Document.Text = fi.Read(); }
            else { scriptBox1.Editor.Document.Text = string.Empty; }

            Text = "New Script*";
            CurrentScript = null;
            btnSave.Enabled = false;
            HidePanels();

            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                scriptBox1.Editor.Document.ParseAll();
                scriptBox1.Editor.Caret.MoveDown(scriptBox1.Editor.Document.Lines.Length - 1, false);
            }), 1000);
        }
        public void SaveScript() {
            if (CurrentScript != null) {
                CurrentScript.Write(scriptBox1.Editor.Document.Text);
            }
            else {
                saveFileDialog1.FileName = string.Empty;
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != string.Empty) {
                    CurrentScript = new FileInfo(saveFileDialog1.FileName);
                    CurrentScript.Write(scriptBox1.Editor.Document.Text);
                }
                else { return; }
            }
            this.Text = CurrentScript.Name;
            btnSave.Enabled = false;
            SetStatusText("Script saved!");
            scriptBox1.Editor.Document.SaveRevisionMark();
            scriptBox1.Editor.Update();
        }
        public void SaveScriptAs() {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != string.Empty) {
                CurrentScript = new FileInfo(saveFileDialog1.FileName);
                CurrentScript.Write(scriptBox1.Editor.Document.Text);
            }
            else { return; }
            this.Text = CurrentScript.Name;
            btnSave.Enabled = false;
            SetStatusText("Script saved!");
            scriptBox1.Editor.Document.SaveRevisionMark();
            scriptBox1.Editor.Update();
        }
        private void SetupEditor() {
            autoListBox1.Setup(scriptBox1);
            scriptBox1.Editor.Document.SyntaxFile = string.Empty;

            var synfile = Program.ExtractSyntaxFile();
            ItemKeywordCollection.Current.UpdateSynFile(synfile);
            UpdateEditor(synfile);

            scriptBox1.Editor.HighLightedLineColor = Color.WhiteSmoke;
            scriptBox1.Editor.SupressAutoComplete = true;
            scriptBox1.Editor.SupressInfoTips = true;
            scriptBox1.Editor.AutoListAutoSelect = false;

            scriptBox1.Editor.Document.Change += syntaxDocument1_Change;
            scriptBox1.Editor.RowMouseMove += Editor_RowMouseMove;
            scriptBox1.MouseLeave += scriptBox1_MouseLeave;
            scriptBox1.Editor.RowMouseDown += Editor_RowMouseDown;
            scriptBox1.Editor.AutoListRequested += Editor_AutoListRequested;
            autoListBox1.VisibleChanged += autoListBox1_VisibleChanged;

            Compiler = new ScriptParser(scriptBox1.Editor, null); //TODO: Tibia Client
            Compiler.OnScriptError += Compiler_OnScriptError;
            Compiler.OnOutputChange += Compiler_OnOutputChange;
            Compiler.OnParseComplete += Compiler_OnParseComplete;
            Compiler.OnProgressReport += Compiler_OnProgressReport;
        }
        private void UpdateEditor(string synfile) {
            scriptBox1.Editor.Document.SyntaxFile = synfile;
            scriptBox1.Editor.Document.ParseAll();
            scriptBox1.Editor.Document.ParseAll(true);
        }
        public void GoToRow(Keyrox.SourceCode.Row row) {
            //if (row.Count > 0) {
            //    scriptBox1.Editor.Selection.LogicalBounds.SetBounds(row[0].Column, row.Index, row[0].Column, row.Index);
            //    scriptBox1.Editor.Selection.MakeSelection();
            //}
            scriptBox1.Editor.GotoLine(row.Index);
        }
        public void GoToWord(Keyrox.SourceCode.Word word) {
            //scriptBox1.Editor.Selection.LogicalBounds.SetBounds(word.Column, word.Row.Index, word.Column, word.Index);
            //scriptBox1.Editor.Selection.MakeSelection();
            scriptBox1.Editor.GotoLine(word.Row.Index);
        }
        private void AskToSave() {
            var result = MessageBox.Show("Do you want save the current changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                SaveScript();
            }
        }
        #endregion

        #region "[rgn] Event Handler  "
        private void autoListBox1_VisibleChanged(object sender, EventArgs e) {
            if (ShoulRestoreALState) {
                ShowSuperTipOnMouseMove = true;
                ShoulRestoreALState = false;
            }
        }
        private void syntaxDocument1_Change(object sender, EventArgs e) {
            if (!IsLoading) {
                if (!this.Text.EndsWith("*")) {
                    this.Text += "*";
                }
                btnSave.Enabled = true;
            }
        }
        private void Editor_AutoListRequested(object sender, Keyrox.Scripting.Events.AutoListTypeEventArgs e) {
            if (e.Type == Keyrox.Scripting.AutoListType.None) {
                autoListBox1.Hide();
            }
            else { autoListBox1.Show(e.Type); }
        }
        private void Editor_RowMouseMove(object sender, Keyrox.Windows.Forms.SyntaxBox.RowMouseEventArgs e) {
            this.BeginInvoke(new Callback(delegate() {
                var x = (e.MouseX - 66);
                if (x >= 0) {
                    x = Convert.ToInt32(x / 8);
                    var word = scriptBox1.Editor.Document.GetWordFromPos(e.Row, x, e.MouseY);
                    if (word != null) {
                        if (word.HasError) {
                            if (!string.IsNullOrEmpty(word.ErrorTip)) { ShowErrorTip(word); return; }
                            if (!string.IsNullOrEmpty(word.WarningTip)) { ShowWarningTip(word); return; }
                        }
                        if (!ShowSuperTipOnMouseMove) { return; }

                        if (word.Row.Text.StartsWith("#section")) { return; }
                        var key = ScriptKeywords.FindKeyword(word.Text, scriptBox1.Editor);
                        if (key != null) { ShowSuperTip(key); return; }
                    }
                }
                HideSuperTip();
            }));
        }
        private void scriptBox1_MouseLeave(object sender, EventArgs e) {
            ScriptToolTipController.HideHint();
            autoListBox1.Hide();
        }
        private void Compiler_OnScriptError(object sender, Keyrox.Scripting.Events.LineErrorEventArgs e) {
            errorList1.Add(e.Error);
        }
        private void Compiler_OnProgressReport(object sender, Keyrox.Scripting.Events.NumberEventArgs e) {
            this.Invoke(new Callback(delegate() {
                ParseProgressBar.EditValue = Convert.ToInt32(ParseProgressBar.EditValue) + 1;
            }));
        }
        private void ribbonControl1_MouseHover(object sender, EventArgs e) {
            HideSuperTip();
            autoListBox1.Hide();
        }
        private void ribbonControl1_MouseMove(object sender, MouseEventArgs e) {
            HideSuperTip();
            autoListBox1.Hide();
        }
        private void repositoryItemProgressBar1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            double v = Convert.ToDouble(e.Value);
            e.DisplayText = v.ToString("n0");
        }
        private void Editor_RowMouseDown(object sender, Keyrox.Windows.Forms.SyntaxBox.RowMouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                editorContextMenu.ShowPopup(Cursor.Position);
            }
            HideSuperTip();
            autoListBox1.Hide();
        }
        private void frm_Editor_Leave(object sender, EventArgs e) {
            HideSuperTip();
        }
        private void TmState_Tick(object sender, EventArgs e) {
            btnCopy.Enabled = scriptBox1.Editor.CanCopy;
            btnPaste.Enabled = scriptBox1.Editor.CanPaste;
            btnCut.Enabled = scriptBox1.Editor.CanCopy;
            btnUndo.Enabled = scriptBox1.Editor.CanUndo;
            btnRedo.Enabled = scriptBox1.Editor.CanRedo;
        }
        #endregion

        #region "[rgn] Ribbon Handler "
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveScript();
        }
        private void btnSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveScriptAs();
        }
        private void btnNewWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmEditor = new frm_Editor();
            frmEditor.Show();
        }
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.Document.Modified) { AskToSave(); }
            NewScript();
        }
        private void btnOpenWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty) {
                var frmEditor = new frm_Editor();
                frmEditor.Show();
                frmEditor.LoadScript(openFileDialog1.FileName);
            }
        }
        private void btnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.Document.Modified) { AskToSave(); }
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty) {
                LoadScript(openFileDialog1.FileName);
            }
        }
        private void btnReadOnly_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (btnReadOnly.Down) {
                scriptBox1.Editor.ReadOnly = true;
                btnReadOnly.LargeImageIndex = 12;
                SetStatusText("This script is now read only!");
            }
            else {
                scriptBox1.Editor.ReadOnly = false;
                btnReadOnly.LargeImageIndex = 13;
                SetStatusText("This script is now editable!");
            }
        }
        private void btnAutoList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Document.ShowAutoList();
        }
        private void btnOutput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockOutput.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }
        private void btnErrors_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockErrors.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }
        private void btnInfoTips_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.SupressInfoTips = btnInfoTips.Down;
        }
        private void btnItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmItems = new frm_Items();
            frmItems.ShowDialog();
            SetupEditor();
        }
        private void btnBuild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CompileScript();
        }
        private void Compiler_OnOutputChange(object sender, Keyrox.Scripting.Events.TextEventArgs e) {
            SetStatusText(e.Text);
        }
        private void btnWarnings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Compiler.SupressWarnings = !btnWarnings.Down;
            errorList1.Clear();
            errorList1.Add(new ScriptLineError() { Message = "You must compile the script to reflect the changes.", ErrorType = ScriptLineErrorType.Error });
            dockErrors.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }
        #endregion

        #region "[rgn] Editor Handler "
        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var pd = new Keyrox.SourceCode.SourceCodePrintDocument(scriptBox1.Editor.Document);
            dlgPrintPreview.Document = pd;
            dlgPrintPreview.ShowDialog(this);
        }
        private void btnUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.CanUndo) {
                scriptBox1.Editor.Undo();
            }
        }
        private void btnRedo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.CanRedo) {
                scriptBox1.Editor.Redo();
            }
        }
        private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.CanCopy) {
                scriptBox1.Editor.Copy();
            }
        }
        private void btnCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.CanCopy) {
                scriptBox1.Editor.Cut();
            }
        }
        private void btnPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (scriptBox1.Editor.CanPaste) {
                scriptBox1.Editor.Paste();
            }
        }
        private void btnFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.ShowFind();
        }
        private void btnFindNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.FindNext();
        }
        private void btnReplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.ShowReplace();
        }
        private void btnAddBM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.ToggleBookmark();
        }
        private void btnNextB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.GotoNextBookmark();
        }
        private void btnPreviousB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Editor.GotoPreviousBookmark();
        }
        #endregion

        #region "[rgn] Toltip Handler "
        public void ShowErrorTip(Keyrox.SourceCode.Word word) {
            ScriptToolTipController.ShowHint(word.BuildErrorTip(), Cursor.Position);
        }
        public void ShowWarningTip(Keyrox.SourceCode.Word word) {
            ScriptToolTipController.ShowHint(word.BuildWarningTip(), Cursor.Position);
        }
        public void ShowSuperTip(Keyword keyword) {
            var pos = Cursor.Position;
            if (autoListBox1.Visible) {
                var modx = autoListBox1.Location.X + autoListBox1.Width + 2;
                var mody = autoListBox1.Location.Y - 24;
                pos = scriptBox1.PointToScreen(new Point(modx, mody));
            }
            ShowSuperTip(keyword, pos);
        }
        public void ShowSuperTip(Keyword keyword, Point location) {
            if (keyword != null) {
                ScriptToolTipController.ShowHint(keyword.BuildSupertTip(autoListBox1.imgAutoList), location);
            }
        }
        public void ShowAutoListTip(Keyword keyword, int delay) {
            if (ShowSuperTipOnMouseMove) { ShoulRestoreALState = true; ShowSuperTipOnMouseMove = false; }
            ShowSuperTip(keyword);
        }
        public void HideSuperTip() {
            ScriptToolTipController.HideHint();
        }
        #endregion

        /// <summary>
        /// Loads the script.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadScript(string path) {
            IsLoading = true;
            CurrentScript = new FileInfo(path);
            if (CurrentScript.Exists) {

                this.Text = CurrentScript.Name;
                scriptBox1.Editor.Document.Text = CurrentScript.Read();
                btnSave.Enabled = false;

                MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                    scriptBox1.Editor.Document.ParseAll();
                    IsLoading = false;
                }), 500);
            }
            else { CurrentScript = null; return; }
            SetStatusText("Script '", CurrentScript.FullName, "' is opened!");
        }

        /// <summary>
        /// Builds the script.
        /// </summary>
        public void CompileScript() {
            btnReadOnly.Down = true;
            dockOutput.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            lblErrors.Caption = "0"; lblWarnings.Caption = "0";

            ParseProgressBar.BeginUpdate();
            repositoryItemProgressBar1.Maximum = scriptBox1.Editor.Document.Lines.Length;
            ParseProgressBar.EditValue = 0;
            ParseProgressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ParseProgressBar.EndUpdate();
            ParseProgressBar.Refresh();
            System.Threading.Thread.Sleep(500);

            errorList1.Clear();
            this.BeginInvoke(new Callback(Compilation));
        }

        /// <summary>
        /// Compilations this instance.
        /// </summary>
        private void Compilation() {
            SetStatusText("Saving the script...");
            SaveScript();

            System.Threading.Thread.Sleep(1000);
            CompiledScript = null;
            Compiler.Parse();
        }

        /// <summary>
        /// Handles the OnParseComplete event of the Compiler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Compiler_OnParseComplete(object sender, EventArgs e) {
            this.Invoke(new Callback(delegate() {
                CompiledScript = Compiler.ParsedScript;
                ScriptState = CompiledScript == null ? ScriptState.HasErrors : ScriptState.ValidScript;

                MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                    ParseProgressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }), 5000);

                if (errorList1.Count() > 0) {
                    lblErrors.Caption = errorList1.CountError().ToString();
                    lblWarnings.Caption = errorList1.CountWarning().ToString();
                    System.Threading.Thread.Sleep(500);
                    dockErrors.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                }
                btnReadOnly.Down = false;
            }));
        }

        /// <summary>
        /// Handles the FormClosing event of the frm_Editor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Editor_FormClosing(object sender, FormClosingEventArgs e) {
            if (scriptBox1.Editor.Document.Modified) { AskToSave(); }
        }

    }
}