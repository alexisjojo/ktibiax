using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Alsing.Windows.Forms;
using KTibiaX.Modules;
using KTibiaX.Properties;
using Keyrox.Shared.Controls;
using Alsing.SourceCode;
using Alsing.Windows.Forms.SyntaxBox;

namespace KTibiaX.Windows.Features.Scripts.Controls {
    public partial class ctrl_AutoList : DevExpress.XtraEditors.XtraUserControl {
        public ctrl_AutoList() {
            InitializeComponent();
        }

        /// <summary>
        /// Setups the specified syntax box.
        /// </summary>
        /// <param name="syntaxBox">The syntax box.</param>
        /// <param name="keywords">The keywords.</param>
        public void Setup(ctrl_SyntaxBox syntaxcontrol, ScriptKeywordCollection keywords) {
            SyntaxControl = syntaxcontrol; ;
            Keywords = keywords;
            LoadKeywords();
            SetupEvents();
        }

        #region "[rgn] Public Properties "
        public ctrl_SyntaxBox SyntaxControl { get; private set; }
        public SyntaxBoxControl SyntaxBox { get { return SyntaxControl.SyntaxBox; } }
        public SyntaxDocument Document { get { return SyntaxBox.Document; } }
        public Caret Caret { get { return SyntaxBox.Caret; } }

        public ScriptKeywordCollection Keywords { get; private set; }
        protected ScriptKeywordCollection Lang { get; private set; }
        protected ScriptKeywordCollection Items { get; private set; }
        protected ScriptKeywordCollection Player { get; private set; }
        protected ScriptKeywordCollection Sections { get; private set; }

        public enum ScriptTabPages { All = 0, Lang = 1, Player = 2, Items = 3, Sections = 4 }
        public ImageListBoxControl ActiveListBox {
            get {
                var page = autoListTab.SelectedTabPage;
                if (page != null) { return page.Controls[0] as ImageListBoxControl; }
                return null;
            }
        }
        public ScriptKeyword SelectedKeyword {
            get {
                var datasource = GetDataSource(ActiveListBox);
                if (ActiveListBox.SelectedIndex > -1) {
                    var res = datasource.Where(k => k.Title == ActiveListBox.SelectedItem.ToString());
                    return res.Count() > 0 ? res.First() : null;
                }
                return null;
            }
        }
        #endregion

        #region "[rgn] Control Events    "
        private void SetupEvents() {
            SyntaxBox.KeyDown += SyntaxBox_KeyDown;
            SyntaxBox.RowClick += syntaxBox_RowClick;
            SyntaxBox.MouseWheel += SyntaxBox_MouseWheel;
            SyntaxBox.KeyUp += new KeyEventHandler(SyntaxBox_KeyUp);

            foreach (XtraTabPage page in autoListTab.TabPages) {
                var lst = page.Controls[0] as ImageListBoxControl;
                if (lst != null) {
                    lst.SelectedIndexChanged += new EventHandler(lst_SelectedIndexChanged);
                    lst.KeyDown += new KeyEventHandler(lst_KeyDown);
                }
            }
        }
        private void lst_SelectedIndexChanged(object sender, EventArgs e) {
            if (SelectedKeyword != null) {
                SyntaxControl.SupressSuperTip(2000);
                SyntaxControl.ShowSuperTip(SelectedKeyword);
            }
        }
        private void lst_KeyDown(object sender, KeyEventArgs e) {
            ApplySelectedItem();
        }
        private void autoListTab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {
            foreach (XtraTabPage page in autoListTab.TabPages) {
                page.ImageIndex = -1;
            }
            if (autoListTab.SelectedTabPage != null) { autoListTab.SelectedTabPage.ImageIndex = 11; }
        }
        private void syntaxBox_RowClick(object sender, RowMouseEventArgs e) {
            if (this.Visible) { this.Hide(); }
        }
        private void SyntaxBox_MouseWheel(object sender, MouseEventArgs e) {
            if (this.Visible) { this.Hide(); }
        }
        private Point GetAbsolutePosition() {
            var pt = SyntaxBox.Painter.GetTextPointPixelPos(new TextPoint(Caret.Position.X, Caret.Position.Y));
            var x = pt.X;
            var y = pt.Y + 18;
            return new Point(x, y);
        }
        public ScriptKeywordCollection GetDataSource(ImageListBoxControl lst) {
            return lst.DataSource as ScriptKeywordCollection;
        }
        #endregion

        #region "[rgn] Helper Methods    "
        private void UpSelection() {
            var max = GetDataSource(ActiveListBox).Count;
            var sel = ActiveListBox.SelectedIndex - 1;
            if (sel > -1 && sel < max) {
                ActiveListBox.SelectedIndex = sel;
            }
        }
        private void DownSelection() {
            var max = GetDataSource(ActiveListBox).Count;
            var sel = ActiveListBox.SelectedIndex + 1;
            if (sel > -1 && sel < max) {
                ActiveListBox.SelectedIndex = sel;
            }
        }
        private void NextPage() {
            var index = autoListTab.SelectedTabPageIndex + 1;
            if (index >= autoListTab.TabPages.Count) { index = 0; }
            autoListTab.SelectedTabPageIndex = index;
        }
        private void PreviousPage() {
            var index = autoListTab.SelectedTabPageIndex - 1;
            if (index < 0) { index = (autoListTab.TabPages.Count - 1); }
            autoListTab.SelectedTabPageIndex = index;
        }
        private bool InSegment(string start, string end) {
            var segment = Caret.CurrentSegment();
            if (segment != null) {
                if (segment.StartWord != null && segment.EndWord != null) {
                    if (segment.StartWord.Text == start && segment.EndWord.Text == end) {
                        return true;
                    }
                }
            }
            return false;
        }
        private string GetCurrentMethodName() {
            if (Caret.CurrentWord != null && Caret.CurrentWord.Text == ")" && Caret.CurrentWord.Index > 1
                && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 1].Text == "(") {
                return Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 2].Text;
            }
            else if (Caret.CurrentWord != null && Caret.CurrentWord.Index > 1
                && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 1].Text == "("
                && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index + 1].Text == ")") {
                return Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 2].Text;
            }
            else if (Caret.CurrentWord != null && Caret.CurrentWord.Index > 1
                && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 1].Text == "{"
                && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index + 1].Text == "}") {
                return "Items";
            }
            else if (Caret.CurrentWord != null && Caret.CurrentWord.Text == "}" && Caret.CurrentWord.Index > 1
            && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 1].Text == "{") {
                return "Items";
            }
            else if (Caret.CurrentWord != null && Caret.CurrentWord.Text.StartsWith("[")) {
                return "Player";
            }
            else if (Caret.CurrentWord != null && Caret.CurrentWord.Text == "]" && Caret.CurrentWord.Index > 0
            && Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 1].Text == "[") {
                return "Player";
            }
            return string.Empty;
        }
        private void SupressKeyHandler(ref KeyEventArgs e) {
            e.Handled = false;
            e.SuppressKeyPress = true;
        }
        private bool IsPropertyList() {
            return autoListTab.SelectedTabPage == pagePlayer
                || autoListTab.SelectedTabPage == pageItems
                || autoListTab.SelectedTabPage == pageSections;
        }
        private bool IsEncapsulateProperty(string text) {
            return new string[] { "[", "{", "(" }.Contains(text);
        }
        private bool IsEndEncapsulateProperty(string text) {
            return new string[] { "]", "}", ")" }.Contains(text);
        }
        private void FillSectionsKeyword() {
            var sectionrows = (from row in Document.Lines where row.ToLower().StartsWith("#section") select row);
            Sections = new ScriptKeywordCollection();
            foreach (var row in sectionrows) {
                var name = row.Replace("#section", "").Trim();
                Sections.Add(new ScriptKeyword() {
                    Title = name,
                    Text = name,
                    Description = "Returns the region name.",
                    ImageIndex = 9,
                    RegionName = true
                });
            }
        }
        #endregion

        /// <summary>
        /// Loads the keywords.
        /// </summary>
        protected void LoadKeywords() {
            Player = new ScriptKeywordCollection(Keywords.Where(key => key.PlayerAttribute));
            Lang = new ScriptKeywordCollection(Keywords.Where(key => !key.PlayerAttribute));

            if (Settings.Default.ScriptItems == null) { Settings.Default.ScriptItems = new ScriptItemKeywordCollection(); }
            Items = new ScriptKeywordCollection();
            Settings.Default.ScriptItems.ForEach(item => Items.Add(new ScriptKeyword() {
                Title = item.Name,
                Text = string.Concat("{", item.Name, "}"),
                Description = "Returns the ID of this item.",
                ImageIndex = 0
            }));

            lstPlayer.DataSource = Player;
            lstLang.DataSource = Lang;
            lstItems.DataSource = Items;
        }

        /// <summary>
        /// Shows the auto list.
        /// </summary>
        /// <param name="pages">The pages.</param>
        public void ShowAutoListPopup(ScriptTabPages pages) {
            autoListTab.BeginUpdate();
            switch (pages) {
                case ScriptTabPages.All:
                    foreach (XtraTabPage page in autoListTab.TabPages) { page.PageVisible = true; }
                    pageSections.PageVisible = false;
                    autoListTab.SelectedTabPage = pageLang;
                    lstLang.SelectedIndex = -1;
                    break;
                case ScriptTabPages.Items:
                    foreach (XtraTabPage page in autoListTab.TabPages) { page.PageVisible = false; }
                    pageItems.PageVisible = true;
                    autoListTab.SelectedTabPage = pageItems;
                    lstItems.SelectedIndex = -1;
                    break;
                case ScriptTabPages.Player:
                    foreach (XtraTabPage page in autoListTab.TabPages) { page.PageVisible = false; }
                    pagePlayer.PageVisible = true;
                    autoListTab.SelectedTabPage = pagePlayer;
                    lstPlayer.SelectedIndex = -1;
                    break;
                case ScriptTabPages.Sections:
                    this.BeginInvoke(new Callback(delegate() { ShowSectionsAutoListPopup(); }));
                    autoListTab.EndUpdate(); return;
            }
            autoListTab.EndUpdate();
            this.Location = GetAbsolutePosition();
            this.Show();
        }

        /// <summary>
        /// Shows the sections auto list.
        /// </summary>
        private void ShowSectionsAutoListPopup() {
            FillSectionsKeyword();
            lstSections.DataSource = Sections;

            foreach (XtraTabPage page in autoListTab.TabPages) { page.PageVisible = false; }
            pageSections.PageVisible = true;
            autoListTab.SelectedTabPage = pageSections;
            lstSections.SelectedIndex = -1;
            this.Location = GetAbsolutePosition();
            this.Show();
        }

        /// <summary>
        /// Handles the KeyDown event of the SyntaxBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SyntaxBox_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Space:
                    if (e.Control) { SupressKeyHandler(ref e); if (GetCurrentMethodName() != string.Empty) { ShowForCustomFunction(); return; } }
                    if (InSegment("{", "}")) { SupressKeyHandler(ref e); ShowAutoListPopup(ScriptTabPages.Items); return; }
                    if (InSegment("[", "]")) { SupressKeyHandler(ref e); ShowAutoListPopup(ScriptTabPages.Player); return; }
                    if (InSegment("(", ")")) { SupressKeyHandler(ref e); ShowForCustomFunction(); return; }
                    if (InSegment("\"", "\"")) { this.Hide(); return; }
                    ShowAutoListPopup(ScriptTabPages.All);
                    return;
                case Keys.Escape: this.Hide(); SyntaxControl.HideSuperTip(); return;
                case Keys.Up: if (this.Visible) { UpSelection(); e.Handled = false; e.SuppressKeyPress = true; } return;
                case Keys.Down: if (this.Visible) { DownSelection(); e.Handled = false; e.SuppressKeyPress = true; } return;
                case Keys.Left: if (this.Visible) { PreviousPage(); SupressKeyHandler(ref e); } return;
                case Keys.Right: if (this.Visible) { NextPage(); SupressKeyHandler(ref e); } return;
                case Keys.Tab: if (this.Visible) { ApplySelectedItem(); SupressKeyHandler(ref e); } return;
                case Keys.Enter: if (this.Visible) { ApplySelectedItem(); SupressKeyHandler(ref e); } return;
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the SyntaxBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SyntaxBox_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Space: return;
                case Keys.Escape: return;
                case Keys.Up: return;
                case Keys.Down: return;
                case Keys.Left: return;
                case Keys.Right: return;
                case Keys.Tab: return;
                case Keys.Enter: return;
                case Keys.Home: return;
                case Keys.End: return;
                case Keys.PageDown: return;
                case Keys.PageUp: return;
                case Keys.Back: if (this.Visible && Caret.CurrentWord == null) { this.Hide(); SyntaxControl.HideSuperTip(); return; } break;
                case Keys.Delete: if (this.Visible && Caret.CurrentWord == null) { this.Hide(); SyntaxControl.HideSuperTip(); return; } break;
                case (Keys)221: // { [
                    if (!e.Shift) ShowAutoListPopup(ScriptTabPages.Player);
                    else ShowAutoListPopup(ScriptTabPages.Items);
                    return;
                case (Keys)57: // (
                    if (GetCurrentMethodName() != string.Empty) { ShowForCustomFunction(); }
                    return;
                case (Keys)17: return; // Ctrl + Space
            }
            if (this.Visible) { FilterActiveList(); }
            else if (Caret.CurrentWord != null) {
                if (Caret.CurrentWord.Column == 0) {
                    ShowAutoListPopup(ScriptTabPages.All);
                    FilterActiveList();
                }
            }
        }

        /// <summary>
        /// Applies the selected item.
        /// </summary>
        private void ApplySelectedItem() {
            if (ActiveListBox.SelectedIndex > -1) {
                InsertKeyword(SelectedKeyword);
                return;
            }
            this.Hide();
        }

        /// <summary>
        /// Inserts the keyword.
        /// </summary>
        /// <param name="key">The key.</param>
        private void InsertKeyword(ScriptKeyword key) {
            this.Hide();
            var column = Caret.LogicalPosition.X;
            var row = Caret.LogicalPosition.Y;
            var inputkey = key.Text;
            var caretident = 0;

            if (!IsPropertyList() && key.AddQuotes) { inputkey += "(\"\")"; caretident = 2; }
            else if (!IsPropertyList()) { inputkey += "()"; caretident = 1; }

            if (Caret.CurrentWord != null && Caret.CurrentWord.Text != "(") {
                column = Caret.CurrentWord.Column;
                var range = Document.GetRangeFromText(Caret.CurrentWord.Text, Caret.CurrentWord.Column, row);
                Document.DeleteRange(range);

                if (IsPropertyList() && Caret.Position.X > 0) {
                    var prevword = Caret.CurrentRow[Caret.CurrentWord.Column - 1];
                    if (prevword != null && IsEncapsulateProperty(prevword.Text)) {
                        range = Document.GetRangeFromText(prevword.Text, prevword.Column, row);
                        Document.DeleteRange(range);
                        column = column - 1;
                    }
                    else if (IsEncapsulateProperty(Caret.CurrentWord.Text)) {
                        range = Document.GetRangeFromText(Caret.CurrentWord.Text, Caret.CurrentWord.Column, row);
                        Document.DeleteRange(range);
                        column = column - 1;
                    }
                    else if (IsEndEncapsulateProperty(Caret.CurrentWord.Text)) {
                        inputkey += Caret.CurrentWord.Text;
                    }
                }
            }
            SyntaxBox.Document.InsertText(inputkey, column, row, true);
            Caret.SetPos(new TextPoint((column + inputkey.Length) - caretident, row));

            var tippos = Parent.PointToScreen(SyntaxBox.Painter.GetTextPointPixelPos(new TextPoint(Caret.Position.X, Caret.Position.Y)));
            var csp = new Point(autoListTab.Width + 2, 19);
            if (key.ItemParam) { ShowAutoListPopup(ScriptTabPages.Items); tippos = tippos.Add(csp); }
            else if (key.Text == "gotosection") { ShowAutoListPopup(ScriptTabPages.Sections); tippos = tippos.Add(csp); }

            if (!IsPropertyList()) {
                SyntaxControl.SupressSuperTip(5000);
                SyntaxControl.ShowSuperTip(key, tippos);
            }
        }

        /// <summary>
        /// Filters the active list.
        /// </summary>
        private void FilterActiveList() {
            if (AutoComplete()) { return; }
            if (Caret.CurrentWord == null || ActiveListBox == null) {
                ActiveListBox.SelectedIndex = -1; return;
            }
            var word = Caret.CurrentWord;
            if (IsEndEncapsulateProperty(Caret.CurrentWord.Text) && Caret.CurrentWord.Index > 0) {
                word = Caret.CurrentRow.FormattedWords[Caret.CurrentWord.Index - 1];
            }
            var text = word.Text;
            if (IsEndEncapsulateProperty(text.Substring(text.Length - 1, 1)) && text.Length > 1) {
                text = text.Substring(0, text.Length - 2);
            }
            var source = GetDataSource(ActiveListBox);
            IList<ScriptKeyword> key = null;
            if (autoListTab.SelectedTabPage == pagePlayer || autoListTab.SelectedTabPage == pageItems) {
                key = source.Where(sk => sk.Title.Trim().ToLower().StartsWith(text)).ToList();
            }
            else { key = source.Where(sk => sk.Text.ToLower().StartsWith(text)).ToList(); }

            if (key.Count() > 0) {
                var firstkey = key.First();
                ActiveListBox.SelectedIndex = source.IndexOf(firstkey);
                ActiveListBox.MakeItemVisible(ActiveListBox.SelectedIndex);
                return;
            }
        }

        /// <summary>
        /// Autoes the complete.
        /// </summary>
        private bool AutoComplete() {
            if (ActiveListBox != null && Caret.CurrentWord != null) {
                var text = Caret.CurrentWord.Text;
                IList<ScriptKeyword> key = null;
                var source = GetDataSource(ActiveListBox);
                if (autoListTab.SelectedTabPage == pagePlayer || autoListTab.SelectedTabPage == pageItems) {
                    key = source.Where(sk => sk.Title.ToLower().StartsWith(text.ToLower())).ToList();
                }
                else {
                    key = source.Where(sk => sk.Title.ToLower().StartsWith(text.ToLower())).ToList();
                }
                if (key.Count() == 1) { InsertKeyword(key.First()); return true; }
            }
            return false;
        }

        /// <summary>
        /// Shows for custom function.
        /// </summary>
        private void ShowForCustomFunction() {
            var methodname = GetCurrentMethodName();
            if (methodname == "gotosection") {
                ShowAutoListPopup(ScriptTabPages.Sections);
                return;
            }
            else if (methodname == "Items") {
                ShowAutoListPopup(ScriptTabPages.Items);
                return;
            }
            else if (methodname == "Player") {
                ShowAutoListPopup(ScriptTabPages.Player);
                return;
            }
        }

    }
}
