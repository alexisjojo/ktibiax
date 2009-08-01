using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Keyrox.Builder.Modules;
using Keyrox.Scripting;
using Keyrox.Scripting.Controls;
using Keyrox.Scripting.Events;
using Keyrox.Scripting.Keywords;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Util;
using Keyrox.Shared.Controls;
using Keyrox.Shared.Enumerators;
using Keyrox.Shared.Files;
using Tibia.Client;

namespace Keyrox.Builder.Features {
    public partial class frm_Editor : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Editor"/> class.
        /// </summary>
        public frm_Editor() {
            InitializeComponent();
            SetupEditor();
            dockPanelHeigth = dockOutput.Height;
        }

        /// <summary>
        /// Handles the Load event of the frm_Editor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Editor_Load(object sender, EventArgs e) {
            var fi = new FileInfo(Program.ExtractResource("Keyrox.Builder.ScriptConfig.DockLayout.xml", "DockLayout.xml", true));
            if (fi.Exists) { dockManager1.RestoreLayoutFromXml(fi.FullName); }

            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "MyScripts");
            ParseProgressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SetControlState(false);

            NewScript();
            TmState.Start();
            if (TibiaClient != null) { btnShowLoc.Checked = true; TMLocation.Start(); }
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { scriptBox1.Editor.Refresh(); }), 1000);
        }

        /// <summary>
        /// Loads the script.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadScript(string path) {
            IsLoading = true;
            CurrentScript = new FileInfo(path);
            if (CurrentScript.Exists) {

                this.CurrentScriptPath = path;
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

        #region "[rgn] Properties     "
        private TibiaClient tibiaClient;
        public TibiaClient TibiaClient {
            get { return tibiaClient; }
            set {
                var visibility = value != null ? BarItemVisibility.Always : BarItemVisibility.Never;
                btnRun.Visibility = visibility;
                gpLocation.Visibility = visibility;
                Invalidate();
                tibiaClient = value;
            }
        }

        public ScriptEditor Editor { get { return scriptBox1.Editor; } }
        public Keyrox.SourceCode.SyntaxDocument Document { get { return scriptBox1.Editor.Document; } }
        public Keyrox.Windows.Forms.SyntaxBox.Caret Caret { get { return scriptBox1.Editor.Caret; } }

        public FileInfo CurrentScript { get; set; }
        public ScriptFile CompiledScript { get; set; }
        public ScriptRunner Runner { get; set; }
        public ScriptParser Compiler { get; set; }

        public bool CompilerSupressWarnings { get; set; }
        public bool IsLoading { get; private set; }
        private int dockPanelHeigth { get; set; }
        public string CurrentScriptPath { get; set; }

        public bool ScriptReadOnly {
            get { return scriptBox1.Editor.ReadOnly; }
            set { scriptBox1.Editor.ReadOnly = value; }
        }

        public bool ShowSuperTipOnMouseMove {
            get { return btnInfoTips.Down; }
            set { btnInfoTips.Down = value; }
        }

        private bool ShoulRestoreALState { get; set; }
        private bool CompileForRun { get; set; }
        private bool CompileForDebug { get; set; }
        private bool InDebugMode { get; set; }

        private StepTo StopDebuggerIn { get; set; }

        private ScriptState scriptState;
        public ScriptState ScriptState {
            get { return scriptState; }
            set {
                scriptState = value;
                lblScriptState.Caption = value.Description();
                lblScriptState.ImageIndex = value.GetHashCode() + 2;
            }
        }
        public event EventHandler<TextEventArgs> StatusTextChanged;
        public event EventHandler OnCompileComplete;
        public event EventHandler OnInvalidateRequired;
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
            if (dockOutput.IsTab) {
                dockOutput.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockOutput.HideSliding();
            }
            if (dockErrors.IsTab) {
                dockErrors.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                dockErrors.HideSliding();
            }
        }
        public void NewScript() {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, "ScriptConfig\\ScriptInfo.txt"));
            if (fi.Exists) { scriptBox1.Editor.Document.Text = fi.Read(); }
            else { scriptBox1.Editor.Document.Text = string.Empty; }

            this.Text = string.Concat("CaveBot - New Script*");
            CurrentScript = null;
            btnSave.Enabled = false;
            HidePanels();

            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                scriptBox1.Editor.Document.ParseAll();
                if (CurrentScript == null) { scriptBox1.Editor.Caret.MoveDown(scriptBox1.Editor.Document.Lines.Length - 1, false); }
            }), 1000);
        }
        public bool SaveScript() {
            if (CurrentScript != null) {
                CurrentScript.Write(scriptBox1.Editor.Document.Text);
            }
            else {
                saveFileDialog1.FileName = string.Empty;
                saveFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "MyScripts");
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != string.Empty) {
                    CurrentScript = new FileInfo(saveFileDialog1.FileName);
                    CurrentScript.Write(scriptBox1.Editor.Document.Text);
                }
                else { SetStatusText("Save operation canceled by user!"); return false; }
            }
            this.Text = string.Concat("CaveBot - ", CurrentScript.Name);
            btnSave.Enabled = false;
            SetStatusText("Script saved!");
            scriptBox1.Editor.Document.SaveRevisionMark();
            scriptBox1.Editor.Update();
            return true;
        }
        public void SaveScriptAs() {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != string.Empty) {
                CurrentScript = new FileInfo(saveFileDialog1.FileName);
                CurrentScript.Write(scriptBox1.Editor.Document.Text);
            }
            else { return; }
            this.Text = string.Concat("CaveBot - ", CurrentScript.Name);
            btnSave.Enabled = false;
            SetStatusText("Script saved!");
            scriptBox1.Editor.Document.SaveRevisionMark();
            scriptBox1.Editor.Update();
        }
        private void SetupEditor() {
            autoListBox1.Setup(scriptBox1);
            scriptBox1.Editor.Document.SyntaxFile = string.Empty;

            var synfile = Program.ExtractResource("Keyrox.Builder.ScriptConfig.Script.syn", @"ScriptConfig\Script.syn");
            ItemKeywordCollection.Current.UpdateSynFile(synfile);
            UpdateEditor(synfile);

            scriptBox1.Editor.HighLightedLineColor = Color.WhiteSmoke;
            scriptBox1.Editor.SupressAutoComplete = true;
            scriptBox1.Editor.SupressInfoTips = true;
            scriptBox1.Editor.AutoListAutoSelect = false;

            scriptBox1.Editor.Document.BreakPointAdded += Document_BreakPointAdded;
            scriptBox1.Editor.Document.BreakPointRemoved += Document_BreakPointRemoved;
            scriptBox1.Editor.Document.RowParsed += Document_RowParsed;
            scriptBox1.Editor.Document.Change += syntaxDocument1_Change;
            scriptBox1.Editor.RowMouseMove += Editor_RowMouseMove;
            scriptBox1.MouseLeave += scriptBox1_MouseLeave;
            scriptBox1.Editor.RowMouseDown += Editor_RowMouseDown;
            scriptBox1.Editor.AutoListRequested += Editor_AutoListRequested;
            autoListBox1.VisibleChanged += autoListBox1_VisibleChanged;
        }
        private void UpdateEditor(string synfile) {
            scriptBox1.Editor.Document.SyntaxFile = synfile;
            scriptBox1.Editor.Document.ParseAll();
            scriptBox1.Editor.Document.ParseAll(true);
        }
        public void GoToRow(Keyrox.SourceCode.Row row) {
            scriptBox1.Editor.GotoLine(row.Index);
        }
        public void GoToWord(Keyrox.SourceCode.Word word) {
            scriptBox1.Editor.GotoLine(word.Row.Index);
        }
        private void AskToSave() {
            var result = MessageBox.Show("Do you want save the current changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                SaveScript();
            }
        }
        public void ShowOutputWindow() {
            dockOutput.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            dockOutput.Height = dockPanelHeigth;
        }
        public void ShowIssuesWindow() {
            dockErrors.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            dockErrors.Height = dockPanelHeigth;
        }
        public void HideRibbon() {
            this.ribbonControl1.Hide();
            this.ribbonControl1.Visible = false;
        }
        public void RestoreRows() {
            Document.Cast<Keyrox.SourceCode.Row>().ToList().ForEach(r => new Callback(delegate() {
                r.BackColor = Color.White;
                r.Images.Clear();
                Document.ParseRow(r);
            }).Invoke());
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
        private void TMLocation_Tick(object sender, EventArgs e) {
            if (TibiaClient != null) {
                var location = TibiaClient.Features.Player.Location;
                var locline = string.Format("{0}, {1}, {2}", location.X, location.Y, location.Z);
                if (lblLocation.Caption != locline) {
                    lblLocation.Caption = locline;
                    this.Invalidate(new Rectangle(ribbonStatusBar1.Location, ribbonStatusBar1.Size), true);
                    if (OnInvalidateRequired != null) { OnInvalidateRequired(this, EventArgs.Empty); }
                }
            }
        }
        private void Document_BreakPointRemoved(object sender, Keyrox.SourceCode.RowEventArgs e) {
            scriptBox1.Editor.Document.ParseRow(e.Row);
        }
        private void Document_BreakPointAdded(object sender, Keyrox.SourceCode.RowEventArgs e) {
            if (!e.Row.Text.Contains("(")) { Editor.Document[e.Row.Index].Breakpoint = false; ReparseRow(e.Row.Index); return; }
            if (!e.Row.Text.EndsWith(")")) { Editor.Document[e.Row.Index].Breakpoint = false; ReparseRow(e.Row.Index); return; }
            if (e.Row.Text.Trim().Length == 0) { Editor.Document[e.Row.Index].Breakpoint = false; ReparseRow(e.Row.Index); return; }
            if (e.Row.Text.StartsWith(@"\")) { Editor.Document[e.Row.Index].Breakpoint = false; ReparseRow(e.Row.Index); return; }
            if (e.Row.Text.StartsWith(" ")) { Editor.Document[e.Row.Index].Breakpoint = false; ReparseRow(e.Row.Index); return; }
            if (e.Row.Text.StartsWith("#")) { Editor.Document[e.Row.Index].Breakpoint = false; ReparseRow(e.Row.Index); return; }
            e.Row.Images.Clear();
        }
        private void ReparseRow(int index) {
            Editor.Document.ParseRow(Editor.Document[index], true);
            Editor.Refresh();
        }
        private void Document_RowParsed(object sender, Keyrox.SourceCode.RowEventArgs e) {
            if (Runner == null || Runner.State == Keyrox.Scripting.Util.RunnerState.Stoped) {
                if (!e.Row.Breakpoint) {
                    if (e.Row.Text.StartsWith("///") && e.Row.Images.Count == 0) { e.Row.Images.Add(10); }
                    else if (e.Row.Text.StartsWith("#section") && e.Row.Images.Count == 0) { e.Row.Images.Add(36); }
                    else if (e.Row.Text.Contains("checkvalue") && e.Row.Images.Count == 0) { e.Row.Images.Add(33); }
                    else if (e.Row.Text.Contains("setparam") && e.Row.Images.Count == 0) { e.Row.Images.Add(28); }
                    else if (e.Row.Text.StartsWith("log") && e.Row.Images.Count == 0) { e.Row.Images.Add(35); }

                    else if (e.Row.Text.StartsWith("setloot") && e.Row.Images.Count == 0) { e.Row.Images.Add(37); }
                    else if (e.Row.Text.StartsWith("gotoline") && e.Row.Images.Count == 0) { e.Row.Images.Add(3); }
                    else if (e.Row.Text.StartsWith("gotosection") && e.Row.Images.Count == 0) { e.Row.Images.Add(3); }
                }
            }
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
            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "MyScripts");
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
            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "MyScripts");
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty) {
                LoadScript(openFileDialog1.FileName);
            }
        }
        private void btnAutoList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            scriptBox1.Document.ShowAutoList();
        }
        private void btnOutput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowOutputWindow();
        }
        private void btnErrors_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowIssuesWindow();
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
        private void btnWarnings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CompilerSupressWarnings = !btnWarnings.Down;
            errorList1.Clear();
            errorList1.Add(new ScriptLineError() { Message = "You must compile the script to reflect the changes.", ErrorType = ScriptLineErrorType.Error });
            ShowIssuesWindow();
        }
        private void btnRun_ItemClick(object sender, ItemClickEventArgs e) {
            if (btnRun.LargeImageIndex == 32) {
                StopScript();
            }
            else if (Runner == null || Runner.State == Keyrox.Scripting.Util.RunnerState.Stoped) {
                CompileForRun = true; CompileScript();
            }
        }
        private void btnDebug_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner == null || Runner.State == Keyrox.Scripting.Util.RunnerState.Stoped) {
                CompileForDebug = true;
                CompileForRun = false;
                CompileScript();
            }
            barDebugger.Visible = true;
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
        private void btnInsertGoto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (TibiaClient != null) {
                var index = scriptBox1.Editor.Caret.CurrentWord != null ? scriptBox1.Editor.Caret.CurrentWord.Index : 0;
                var location = TibiaClient.Features.Player.Location;
                var locline = string.Format("goto({0}, {1}, {2})", location.X, location.Y, location.Z);
                scriptBox1.Editor.Document.InsertText(locline, index, scriptBox1.Editor.Caret.CurrentRow.Index, true);
                scriptBox1.Editor.Caret.MoveEnd(false);
            }
        }
        private void btnShowLoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (TibiaClient != null && btnShowLoc.Checked) {
                TMLocation.Start();
            }
            else { TMLocation.Stop(); }
        }
        #endregion

        #region "[rng] Debug ToolBar  "
        private void btnBarRun_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner == null || Runner.State == Keyrox.Scripting.Util.RunnerState.Stoped) {
                CompileForDebug = false;
                CompileForRun = true;
                CompileScript();
            }
            else if (Runner != null && Runner.State == RunnerState.Paused) {
                RestoreRows();
                Editor.Refresh();
                Runner.ResumeExecution();
            }
        }
        private void btnBarDebug_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner == null || Runner.State == Keyrox.Scripting.Util.RunnerState.Stoped) {
                CompileForDebug = true;
                CompileForRun = false;
                CompileScript();
            }
        }
        private void btnBarNext_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner != null && Runner.State == Keyrox.Scripting.Util.RunnerState.Running && Runner.InDebugMode) {
                RestoreRows();
                Editor.Refresh();
                StopDebuggerIn = StepTo.NextRow;
                Runner.ResumeExecution();
            }
        }
        private void btnBarResume_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner != null && Runner.State == Keyrox.Scripting.Util.RunnerState.Running && Runner.InDebugMode) {
                RestoreRows();
                Editor.Refresh();
                StopDebuggerIn = StepTo.None;
                Editor.Document.ClearBreakpoints();
                Runner.ResumeExecution();
            }
        }
        private void btnBarNextBreakPoint_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner != null && Runner.State == Keyrox.Scripting.Util.RunnerState.Running && Runner.InDebugMode) {
                RestoreRows();
                Editor.Refresh();
                StopDebuggerIn = StepTo.NextBreakPoint;
                Runner.ResumeExecution();
            }
        }
        private void btnBarStop_ItemClick(object sender, ItemClickEventArgs e) {
            if (Runner == null) { return; }
            if (Runner.State != Keyrox.Scripting.Util.RunnerState.Running) { return; }
            StopScript();
        }
        #endregion

        #region "[rgn] Toltip Handler "
        public void ShowErrorTip(Keyrox.SourceCode.Word word) {
            this.BeginInvoke(new Callback(delegate() {
                ScriptToolTipController.ShowHint(word.BuildErrorTip(), Cursor.Position);
            }));
        }
        public void ShowWarningTip(Keyrox.SourceCode.Word word) {
            this.BeginInvoke(new Callback(delegate() {
                ScriptToolTipController.ShowHint(word.BuildWarningTip(), Cursor.Position);
            }));
        }
        public void ShowSuperTip(Keyword keyword) {
            this.BeginInvoke(new Callback(delegate() {
                var pos = Cursor.Position;
                if (autoListBox1.Visible) {
                    var modx = autoListBox1.Location.X + autoListBox1.Width + 2;
                    var mody = autoListBox1.Location.Y - 24;
                    pos = scriptBox1.PointToScreen(new Point(modx, mody));
                }
                ShowSuperTip(keyword, pos);
            }));
        }
        public void ShowSuperTip(Keyword keyword, Point location) {
            if (keyword != null) {
                this.BeginInvoke(new Callback(delegate() {
                    ScriptToolTipController.ShowHint(keyword.BuildSupertTip(autoListBox1.imgAutoList), location);
                }));
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

        #region "[rgn] Script Compile "
        public void CompileScript() {
            outputView1.Clear();
            this.BeginInvoke(new Callback(Compilation));
        }
        private void Compilation() {
            ScriptReadOnly = true;
            HideSuperTip();
            autoListBox1.Hide();
            ShowOutputWindow();
            lblErrors.Caption = "0"; lblWarnings.Caption = "0";

            ParseProgressBar.BeginUpdate();
            repositoryItemProgressBar1.Maximum = scriptBox1.Editor.Document.Lines.Length;
            ParseProgressBar.EditValue = 0;
            ParseProgressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ParseProgressBar.EndUpdate();
            ParseProgressBar.Refresh();
            System.Threading.Thread.Sleep(500);

            errorList1.Clear();
            SetStatusText("Saving the script...");

            if (SaveScript()) {

                Compiler = new ScriptParser(scriptBox1.Editor, TibiaClient);
                Compiler.OnScriptError += Compiler_OnScriptError;
                Compiler.OnOutputChange += Compiler_OnOutputChange;
                Compiler.OnParseComplete += Compiler_OnParseComplete;
                Compiler.OnProgressReport += Compiler_OnProgressReport;

                Compiler.SupressWarnings = CompilerSupressWarnings;
                System.Threading.Thread.Sleep(1000);
                CompiledScript = null;
                Compiler.Parse();

            }
        }
        private void Compiler_OnParseComplete(object sender, ScriptFileEventArgs e) {
            this.Invoke(new Callback(delegate() {

                CompiledScript = e.Script;
                Compiler.Dispose();
                Compiler = null;

                ScriptState = CompiledScript == null ? ScriptState.HasErrors : ScriptState.ValidScript;
                MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                    ParseProgressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }), 5000);

                lblErrors.Caption = errorList1.CountError().ToString();
                lblWarnings.Caption = errorList1.CountWarning().ToString();
                if (errorList1.Count() > 0) {
                    System.Threading.Thread.Sleep(500);
                    ShowIssuesWindow();
                }

                ScriptReadOnly = false;
                if (OnCompileComplete != null) { OnCompileComplete(this, EventArgs.Empty); }
                if (CompileForDebug) { CompileForDebug = false; RunScript(true); }
                else if (CompileForRun) { CompileForRun = false; RunScript(false); }

                SetStatusText("Ready!");
                System.Threading.Thread.Sleep(1000);
            }));
        }
        private void Compiler_OnOutputChange(object sender, Keyrox.Scripting.Events.TextEventArgs e) {
            SetStatusText(e.Text);
        }
        private void Compiler_OnScriptError(object sender, Keyrox.Scripting.Events.LineErrorEventArgs e) {
            errorList1.Add(e.Error);
        }
        private void Compiler_OnProgressReport(object sender, Keyrox.Scripting.Events.NumberEventArgs e) {
            this.BeginInvoke(new Callback(delegate() {
                ParseProgressBar.BeginUpdate();
                ParseProgressBar.EditValue = Convert.ToInt32(ParseProgressBar.EditValue) + 1;
                ParseProgressBar.EndUpdate();
                this.Invalidate(new Rectangle(ribbonStatusBar1.Location, ribbonStatusBar1.Size), true);
                if (OnInvalidateRequired != null) { OnInvalidateRequired(this, EventArgs.Empty); }
            }));
        }
        #endregion

        #region "[rgn] Script Runner  "
        private void RunScript(bool debug) {
            if (TibiaClient != null) {
                SetControlState(true);
                InDebugMode = debug;

                this.BeginInvoke(new Callback(delegate() {

                    if (CompiledScript != null) {
                        RestoreRows();
                        HidePanels();

                        Runner = new ScriptRunner(CompiledScript, TibiaClient);
                        Runner.OnScriptStop += Runner_OnScriptStop;
                        Runner.OnException += Runner_OnException;
                        Runner.OnScriptException += Runner_OnScriptException;
                        Runner.OnRowBeginExecute += new EventHandler<Keyrox.Scripting.Events.ScriptLineEventArgs>(Runner_OnRowBeginExecute);
                        Runner.OnRowEndExecute += new EventHandler<Keyrox.Scripting.Events.ScriptLineEventArgs>(Runner_OnRowEndExecute);
                        Runner.Start(debug);
                    }
                    else { SetControlState(false); }
                }));
            }
        }
        private void Runner_OnRowBeginExecute(object sender, Keyrox.Scripting.Events.ScriptLineEventArgs e) {
            if (InDebugMode) {
                this.Invoke(new Callback(delegate() {
                    RestoreRows();
                    var row = Editor.Document[e.Line.LineIndex];
                    row.BackColor = Color.LightBlue;
                    row.Images.Clear();
                    row.Images.Add(18);
                    btnBarNext.Enabled = false;
                    btnBarNextBreakPoint.Enabled = false;
                    row.EnsureVisible();
                    Document.ParseRow(row);
                    Editor.Refresh();
                }));
            }
        }
        private void Runner_OnRowEndExecute(object sender, Keyrox.Scripting.Events.ScriptLineEventArgs e) {
            if (InDebugMode) {
                this.Invoke(new Callback(delegate() {
                    RestoreRows();
                    var row = Editor.Document[e.Line.LineIndex];
                    if (StopDebuggerIn == StepTo.NextRow || row.Breakpoint) {
                        row.BackColor = Color.Yellow;
                        row.Images.Clear();
                        row.Images.Add(22);
                        btnBarNext.Enabled = true;
                        btnBarNextBreakPoint.Enabled = true;
                    }
                    else {
                        Runner.ResumeExecution();
                    }
                    row.EnsureVisible();
                    Document.ParseRow(row);
                    Editor.Refresh();
                }));
            }
        }
        private void StopScript() {
            if (Runner != null) {
                Runner.Stop();
                SetControlState(false);
                this.Invoke(new Callback(delegate() {
                    RestoreRows();
                    Editor.Refresh();
                }));
                MethodCall.ExecuteSafeThreadIn(delegate() {
                    RestoreRows();
                    Editor.Refresh();
                    Document.ParseAll(true);
                }, 1000);
            }
        }
        private void Runner_OnScriptException(object sender, Keyrox.Scripting.Events.ScriptExceptionEventArgs e) {
            errorList1.Add(new ScriptLineError(e.Exception.Message, scriptBox1.Editor.Document[e.RowIndex], ScriptLineErrorType.Error));
            AddInnerExceptions(e);
        }
        private void Runner_OnException(object sender, Keyrox.Scripting.Events.ExceptionEventArgs e) {
            errorList1.Add(new ScriptLineError(e.Exception.Message, (Keyrox.SourceCode.Row)null, ScriptLineErrorType.Error));
            AddInnerExceptions(e);
        }
        private void Runner_OnScriptStop(object sender, EventArgs e) {
            SetControlState(false);
            this.Invoke(new Callback(delegate() {
                RestoreRows();
                Editor.Refresh();
            }));
            MethodCall.ExecuteSafeThreadIn(delegate() {
                RestoreRows();
                Editor.Refresh();
                Document.ParseAll(true);
            }, 1000);
            SetStatusText("Script execution has stoped");
        }
        private void AddInnerExceptions(Keyrox.Scripting.Events.ScriptExceptionEventArgs e) {
            this.BeginInvoke(new Callback(delegate() {
                var innerException = e.Exception.InnerException;
                while (innerException != null) {
                    errorList1.Add(new ScriptLineError(innerException.Message, scriptBox1.Editor.Document[e.RowIndex], ScriptLineErrorType.Error));
                    innerException = innerException.InnerException;
                }
            }));
        }
        private void AddInnerExceptions(Keyrox.Scripting.Events.ExceptionEventArgs e) {
            this.BeginInvoke(new Callback(delegate() {
                var innerException = e.Exception.InnerException;
                while (innerException != null) {
                    errorList1.Add(new ScriptLineError(e.Exception.Message, (Keyrox.SourceCode.Row)null, ScriptLineErrorType.Error));
                    innerException = innerException.InnerException;
                }
            }));
        }
        private void SetControlState(bool running) {
            scriptBox1.Editor.ReadOnly = running;
            foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbonControl1.Pages) {
                foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup group in page.Groups) {
                    foreach (BarItemLink item in group.ItemLinks) {
                        if (item.Item.Name != btnRun.Name) {
                            item.Item.Enabled = !running;
                        }
                    }
                }
            }
            btnRun.LargeImageIndex = running ? 32 : 30;
            btnRun.Caption = running ? "  Stop Execution  " : "  Run Script  ";
            btnDebug.Enabled = !running;

            btnBarDebug.Enabled = !running;
            //btnBarRun.Enabled = !running;
            btnBarStop.Enabled = running;

            if (running) {
                btnResume.Enabled = false;
                btnNextRow.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// Handles the FormClosing event of the frm_Editor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Editor_FormClosing(object sender, FormClosingEventArgs e) {
            if (scriptBox1.Editor.Document.Modified) { AskToSave(); }
            dockManager1.SaveLayoutToXml(Path.Combine(Environment.CurrentDirectory, "docklayout.xml"));
        }

    }
}