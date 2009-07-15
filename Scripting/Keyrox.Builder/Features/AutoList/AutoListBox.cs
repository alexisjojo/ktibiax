using System;
using System.Linq;
using System.Windows.Forms;
using Keyrox.Scripting;
using Keyrox.Scripting.Controls;
using Keyrox.Scripting.Keywords;
using Keyrox.Shared.Controls;
using Keyrox.SourceCode;
using Keyrox.Windows.Forms.SyntaxBox;

namespace Keyrox.Builder.Features.AutoList {
    public partial class AutoListBox : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoListForm"/> class.
        /// </summary>
        public AutoListBox() {
            InitializeComponent();
        }

        /// <summary>
        /// Setups the specified script box.
        /// </summary>
        /// <param name="scriptBox">The script box.</param>
        public void Setup(ScriptBox scriptBox) {
            ScriptBox = scriptBox;
            this.Visible = false;
            scriptBox.Editor.RowClick += syntaxBox_RowClick;
            scriptBox.Editor.MouseWheel += SyntaxBox_MouseWheel;
            scriptBox.Scroll += scriptBox_Scroll;
            scriptBox.Editor.KeyDown += scriptBox_KeyDown;
            scriptBox.Editor.KeyUp += Editor_KeyUp;
            scriptBox.Editor.MouseLeave += Editor_MouseLeave;
            scriptBox.Editor.Click += Editor_Click;
        }

        #region "[rgn] Properties     "
        public AutoListType AutoListType { get; private set; }
        public KeywordCollection KeySource { get; private set; }
        public Keyword SelectedKey {
            get {
                if (lstAutoList.SelectedIndex > -1) {
                    return KeySource[lstAutoList.SelectedIndex];
                }
                return null;
            }
        }
        public ScriptBox ScriptBox { get; set; }
        public ScriptDocument Document { get { return ScriptBox.Document; } }
        public ScriptEditor Editor { get { return ScriptBox.Editor; } }
        private bool SupressKeyUP { get; set; }
        #endregion

        #region "[rgn] Helper Methods "
        private void UpSelection() {
            var max = KeySource.Count;
            var sel = lstAutoList.SelectedIndex - 1;
            if (sel > -1 && sel < max) {
                lstAutoList.SelectedIndex = sel;
            }
        }
        private void DownSelection() {
            var max = KeySource.Count;
            var sel = lstAutoList.SelectedIndex + 1;
            if (sel > -1 && sel < max) {
                lstAutoList.SelectedIndex = sel;
            }
        }
        private void FixTagImageIndex() {
            switch (AutoListType) {
                case AutoListType.None: return;
                case AutoListType.General: xtraPageAutoList.ImageIndex = 8; break;
                case AutoListType.Item: xtraPageAutoList.ImageIndex = 4; break;
                case AutoListType.Params: xtraPageAutoList.ImageIndex = 7; break;
                case AutoListType.Player: xtraPageAutoList.ImageIndex = 5; break;
                case AutoListType.Sections: xtraPageAutoList.ImageIndex = 6; break;
            }
        }
        private void SupressKeyDown(ref KeyEventArgs e) {
            e.Handled = false;
            e.SuppressKeyPress = true;
            SupressKeyUP = true;
        }
        private string RemoveEnclosure(string value) {
            return value
                .Replace("{", "").Replace("}", "")
                .Replace("[", "").Replace("]", "")
                .Replace("(", "").Replace(")", "")
                .Replace("\"", "");
        }
        public void RemoveDuplicatedBrackets() {
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                var text = Editor.Caret.CurrentRow.Text;
                text = text
                    .Replace("{{", "{")
                    .Replace("}}", "}")
                    .Replace("[[", "[")
                    .Replace("]]", "]");
                Editor.Caret.CurrentRow.Text = text;
                Editor.Document.ResetVisibleRows();
                Editor.Document.ParseRow(Editor.Caret.CurrentRow);
            }), 500);
        }
        private bool VerifyEndOfRow() {
            if (Document.CurrentWord != null) {
                if (new[] { "}", "]", "\"" }.Contains(Document.CurrentWord.Text)) {
                    return true;
                }
                if (Document.CurrentRow.Count > (Document.CurrentWord.Index + 1)) {
                    var nextw = Document.CurrentRow[Document.CurrentWord.Index + 1];
                    if (new[] { "}", "]", "\"" }.Contains(nextw.Text)) {
                        return true;
                    }
                }
            }
            return false;
        }
        private void AddSpace() {
            if (Document.CurrentWord != null) {
                Editor.Document.InsertText(" ", Document.CurrentWord.Column, Document.CurrentRow.Index);
                Editor.Caret.SetPos(new TextPoint(Editor.Caret.Position.X + 1, Editor.Caret.Position.Y));
            }
        }
        #endregion

        #region "[rgn] Control Events "
        private void AutoListBox_VisibleChanged(object sender, EventArgs e) {
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                AutoListToolTip.HideHint();
            }), 500);
        }
        private void lstAutoList_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) { if (SelectedKey != null) { InsertSelectedKey(); } }
            else if (e.KeyCode == Keys.Tab) { if (SelectedKey != null) { InsertSelectedKey(); } }
            else if (e.KeyCode == Keys.Escape) { this.Hide(); }
        }
        private void lstAutoList_DoubleClick(object sender, EventArgs e) {
            if (SelectedKey != null) { InsertSelectedKey(); }
        }
        private void lstAutoList_SelectedIndexChanged(object sender, EventArgs e) {
            if (SelectedKey == null) { (this.ParentForm as frm_Editor).HideSuperTip(); }
            else { (this.ParentForm as frm_Editor).ShowAutoListTip(SelectedKey, 3000); }
        }
        private void scriptBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode.GetHashCode() == 221 || e.KeyCode.GetHashCode() == 57) { return; }
            else if (e.KeyCode == Keys.Enter) { if (this.Visible) { InsertSelectedKey(); SupressKeyDown(ref e); return; } }
            else if (e.KeyCode == Keys.Tab) { if (this.Visible) { InsertSelectedKey(); SupressKeyDown(ref e); return; } }
            else if (e.KeyCode == Keys.Escape) { this.Hide(); return; }
            else if (e.KeyCode == Keys.Space && e.Control) { SupressKeyDown(ref e); Document.ShowAutoList(); return; }
            else if (e.KeyCode == Keys.Up) { if (this.Visible) { UpSelection(); SupressKeyDown(ref e); return; } else { return; } }
            else if (e.KeyCode == Keys.Down) { if (this.Visible) { DownSelection(); SupressKeyDown(ref e); return; } else { return; } }
            else if (e.KeyCode == Keys.Left) { if (this.Visible) { DownSelection(); SupressKeyDown(ref e); return; } else { return; } }
            else if (e.KeyCode == Keys.Right) { if (this.Visible) { DownSelection(); SupressKeyDown(ref e); return; } else { return; } }
            else if (e.KeyCode.GetHashCode() > 186 && e.KeyCode.GetHashCode() < 191) { Show(AutoListType.General); return; }

            if (this.Visible) { this.BeginInvoke(new Callback(delegate() { FilterListBox(); })); }
        }
        private void Editor_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Back) { return; }
            if (e.KeyCode == Keys.Escape) { Hide(); return; }
            if (e.KeyCode.GetHashCode() == 221) { if (e.Shift) { Show(AutoListType.Item); return; } else { Show(AutoListType.Player); return; } }
            if (e.KeyCode.GetHashCode() == 57 && e.Shift) { Document.ShowAutoList(); return; }
            if (e.KeyCode.GetHashCode() > 48 && e.KeyCode.GetHashCode() < 58 && !e.Shift) { this.Hide(); return; }
            if (e.KeyCode == Keys.Space && !e.Control) { Document.ShowAutoList(); return; }
            if (Document.CurrentWord != null && Document.CurrentWord.Index == 0 && Document.CurrentRow.Count == 1) { Show(AutoListType.General); }
            else if (e.KeyCode.GetHashCode() > 186 && e.KeyCode.GetHashCode() < 191) { AddSpace(); return; }
        }
        private void syntaxBox_RowClick(object sender, RowMouseEventArgs e) {
            if (this.Visible) { this.Hide(); }
        }
        private void SyntaxBox_MouseWheel(object sender, MouseEventArgs e) {
            if (this.Visible) { this.Hide(); }
        }
        private void scriptBox_Scroll(object sender, ScrollEventArgs e) {
            if (this.Visible) { this.Hide(); }
        }
        private void Editor_Click(object sender, EventArgs e) {
            Hide();
        }
        private void Editor_MouseLeave(object sender, EventArgs e) {
            //Hide();
        }
        #endregion

        /// <summary>
        /// Shows the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void Show(AutoListType type) {
            this.AutoListType = type;
            SetupAndShow();
        }

        /// <summary>
        /// Setups the and show.
        /// </summary>
        private void SetupAndShow() {
            this.BeginInvoke(new Callback(delegate() {
                var tmpeditor = ScriptBox.Editor;
                var newsource = ScriptKeywords.GetKeywords(tmpeditor, AutoListType);
                if (KeySource == null || KeySource.CurrentType != AutoListType) {

                    this.Visible = false;
                    FixTagImageIndex();
                    KeySource = ScriptKeywords.GetKeywords(tmpeditor, AutoListType);
                    Editor.Document.ParseAll();
                    Editor.Document.ParseAll(true);

                    lstAutoList.DataSource = KeySource;
                    lstAutoList.DisplayMember = "Title";
                    lstAutoList.ValueMember = "InputText";
                    lstAutoList.ImageIndexMember = "ImageIndex";
                    lstAutoList.Refresh();
                }
                this.Location = Document.GetCarretPosition();
                this.Visible = true;
            }));
        }

        /// <summary>
        /// Filters the list box.
        /// </summary>
        public void FilterListBox() {
            if (this.Visible && Document.CurrentWord != null) {
                this.BeginInvoke(new Callback(delegate() {
                    var editkey = Document.CurrentWord.Text;
                    //editkey = RemoveEnclosure(editkey).Trim();
                    if (new[] { "\"", "]", ")", "}" }.Contains(editkey) && Document.CurrentRow.Count > Document.CurrentWord.Index && Document.CurrentWord.Index > 0) {
                        editkey = Document.CurrentRow[Document.CurrentWord.Index - 1].Text;
                    }
                    var math = from key in KeySource where RemoveEnclosure(key.InputText).ToLower().StartsWith(editkey.ToLower().Trim()) select key;
                    if (math.Count() > 0) {
                        lstAutoList.SelectedIndex = KeySource.IndexOf(math.First());
                        lstAutoList.Update();
                        return;
                    }
                    lstAutoList.SelectedIndex = -1;
                }));
            }
        }

        /// <summary>
        /// Inserts the selected key.
        /// </summary>
        public void InsertSelectedKey() {
            if (this.Visible && this.SelectedKey != null) {
                this.BeginInvoke(new Callback(delegate() {
                    var caret = Document.Caret;
                    if (caret.CurrentWord != null) {
                        var col = CheckSearchAndBrackets();
                        Editor.Document.InsertText(SelectedKey.InputText, col, Document.Caret.CurrentRow.Index);
                        caret.SetPos(new TextPoint((col + SelectedKey.InputText.Length) + SelectedKey.CaretPosition, caret.CurrentRow.Index));
                    }
                    else {
                        Editor.Document.InsertText(SelectedKey.InputText, 0, Document.Caret.CurrentRow.Index);
                        caret.SetPos(new TextPoint((SelectedKey.InputText.Length) + SelectedKey.CaretPosition, caret.CurrentRow.Index));
                    }
                    this.Hide();
                    RemoveDuplicatedBrackets();
                    if (!VerifyEndOfRow()) { MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { Document.ShowAutoList(); }), 500); }
                }));
            }
        }

        /// <summary>
        /// Checks the search and brackets.
        /// </summary>
        /// <returns>The column to insert the new keyword.</returns>
        private int CheckSearchAndBrackets() {
            var caret = Document.Caret;
            var column = caret.CurrentWord.Column;
            if (!new[] { "\"", "]", ")", "}" }.Contains(caret.CurrentWord.Text)) {
                var range = Editor.Document.GetRangeFromText(caret.CurrentWord.Text, caret.CurrentWord.Column, caret.CurrentRow.Index);
                Editor.Document.DeleteRange(range);
            }
            if (caret.CurrentWord.Index > 0 && !new[] { "(", ",", "\"" }.Contains(caret.CurrentRow[caret.CurrentWord.Index - 1].Text)) {
                var word = caret.CurrentRow[caret.CurrentWord.Index - 1];
                var ident = caret.Position.X - word.Text.Length;
                var range = Editor.Document.GetRangeFromText(word.Text, word.Column, caret.CurrentRow.Index);
                Editor.Document.DeleteRange(range);
                Document.Caret.SetPos(new TextPoint(ident, caret.Position.Y));
                return ident;
            }
            return column;
        }

        /// <summary>
        /// Conceals the control from the user.
        /// </summary>
        public new void Hide() {
            this.Visible = false;
        }

    }
}