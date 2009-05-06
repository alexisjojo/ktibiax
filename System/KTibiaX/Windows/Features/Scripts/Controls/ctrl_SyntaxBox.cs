using System;
using System.Drawing;
using System.Windows.Forms;
using Alsing.SourceCode;
using Alsing.Windows.Forms;
using DevExpress.Utils;
using Keyrox.Shared.Files;
using Keyrox.Shared.Objects;
using KTibiaX.Modules;
using Keyrox.Shared.Controls;

namespace KTibiaX.Windows.Features.Scripts.Controls {
    public partial class ctrl_SyntaxBox : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="ctrl_SyntaxBox"/> class.
        /// </summary>
        public ctrl_SyntaxBox() {
            InitializeComponent();
            LoadKeywords();
        }

        /// <summary>
        /// Loads the keyword file.
        /// </summary>
        public void LoadKeywords() {
            var fi = new System.IO.FileInfo(System.IO.Path.Combine(Application.StartupPath, "ScriptConfig\\keywords.xml"));
            if (fi.Exists) { Keywords = fi.Read().Deserialize<ScriptKeywordCollection>(); }
            ScriptAutoList.Setup(this, Keywords);
            ScriptAutoList.Hide();
        }

        #region "[rgn] Public Properties "
        public ScriptKeywordCollection Keywords { get; set; }
        public bool SupressToolTips { get; set; }
        public SyntaxBoxControl SyntaxBox {
            get { return ScriptBox; }
            set { ScriptBox = value; }
        }
        public SyntaxDocument SyntaxDocument {
            get { return ScriptDocument; }
            set { ScriptDocument = value; }
        }
        public bool SupressHideSuperTip { get; set; }
        #endregion

        #region "[rgn] ScriptBox Events  "
        private void ScriptBox_KeyPress(object sender, KeyPressEventArgs e) {
            Console.Write("");
        }
        private void ScriptBox_RowMouseMove(object sender, Alsing.Windows.Forms.SyntaxBox.RowMouseEventArgs e) {
            if (SupressToolTips) { return; }
            var x = (e.MouseX - 66);
            if (x >= 0) {
                x = Convert.ToInt32(x / 8);
                var word = SyntaxDocument.GetWordFromPos(e.Row, x, e.MouseY);
                if (word != null) {
                    var keyword = Keywords[word.Text];
                    if (keyword != null) {
                        ShowSuperTip(keyword);
                        return;
                    }
                }
            }
            if (!SupressHideSuperTip) { HideSuperTip(); }
        }
        #endregion

        /// <summary>
        /// Shows the super tip.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="location">The location.</param>
        public void ShowSuperTip(ScriptKeyword keyword, Point location) {
            if (keyword != null) {
                ScriptToolTipController.ShowHint(BuildSupertTip(keyword), location);
            }
        }

        /// <summary>
        /// Shows the super tip.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        public void ShowSuperTip(ScriptKeyword keyword) {
            var pos = Cursor.Position;
            if (ScriptAutoList.Visible) {
                var modx = ScriptAutoList.Location.X + ScriptAutoList.Width + 2;
                var mody = ScriptAutoList.Location.Y + 1;
                pos = ScriptBox.PointToScreen(new Point(modx, mody));
            }
            ShowSuperTip(keyword, pos);
        }

        /// <summary>
        /// Hides the super tip.
        /// </summary>
        public void HideSuperTip() {
            SupressHideSuperTip = false;
            ScriptToolTipController.HideHint();
        }

        public void SupressSuperTip(int delay) {
            HideSuperTip();
            SupressHideSuperTip = true;
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { SupressHideSuperTip = false; }), delay);
        }

        /// <summary>
        /// Builds the supert tip.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        private ToolTipControllerShowEventArgs BuildSupertTip(ScriptKeyword keyword) {
            var superToolTip1 = new DevExpress.Utils.SuperToolTip();

            var toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            toolTipTitleItem1.Appearance.Image = imgAutoList.Images[keyword.ImageIndex];
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = imgAutoList.Images[keyword.ImageIndex];
            toolTipTitleItem1.Text = keyword.Title;
            toolTipTitleItem1.ImageToTextDistance = 5;
            toolTipTitleItem1.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
            superToolTip1.Items.Add(toolTipTitleItem1);

            var toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Font = new Font("Consolas", 8, FontStyle.Bold);
            toolTipItem1.Text = keyword.Parameters;
            superToolTip1.Items.Add(toolTipItem1);

            var toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Appearance.ForeColor = Color.SlateGray;
            toolTipItem2.Font = new Font("Consolas", 8, FontStyle.Regular);
            toolTipItem2.Text = keyword.Examples;
            superToolTip1.Items.Add(toolTipItem2);
            superToolTip1.Items.Add(new DevExpress.Utils.ToolTipSeparatorItem());

            var toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            toolTipTitleItem2.LeftIndent = 6;
            toolTipTitleItem2.Text = keyword.Description;
            superToolTip1.Items.Add(toolTipTitleItem2);

            superToolTip1.MaxWidth = 500;
            superToolTip1.FixedTooltipWidth = false;
            return new ToolTipControllerShowEventArgs() { SuperTip = superToolTip1, ToolTipType = ToolTipType.SuperTip };
        }

    }
}
