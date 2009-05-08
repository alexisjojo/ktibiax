using System;
using DevExpress.XtraEditors;
using Keyrox.Shared.Objects;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using DevExpress.Utils;
using System.Windows.Forms;
using Keyrox.Scripting.Keywords;
using Keyrox.Builder.Properties;

namespace Keyrox.Builder.Modules {
    public static class Extensions {

        /// <summary>
        /// Adds the new item.
        /// </summary>
        /// <param name="combobox">The combobox.</param>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <param name="imageIndex">Index of the image.</param>
        public static void AddNewItem(this ImageComboBoxEdit combobox, string text, object value, int imageIndex) {
            combobox.Properties.Items.Add(
                new ImageComboBoxItem(text, value, imageIndex)
            );
        }
        /// <summary>
        /// Adds the new item.
        /// </summary>
        /// <param name="combobox">The combobox.</param>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public static void AddNewItem(this ImageComboBoxEdit combobox, string text, object value) {
            AddNewItem(combobox, text, value, -1);
        }

        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combobox">The combobox.</param>
        /// <returns></returns>
        public static T GetSelectedValue<T>(this ImageComboBoxEdit combobox) {
            return combobox.Properties.Items[combobox.SelectedIndex].Value.To<T>();
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        /// <param name="combobox">The combobox.</param>
        /// <returns></returns>
        public static object GetSelectedText(this ImageComboBoxEdit combobox) {
            return combobox.Properties.Items[combobox.SelectedIndex].Description;
        }

        /// <summary>
        /// Selects the combo item.
        /// </summary>
        /// <param name="combobox">The combobox.</param>
        /// <param name="description">The description.</param>
        public static void SelectComboItem(this ImageComboBoxEdit combobox, string description) {
            foreach (ImageComboBoxItem item in combobox.Properties.Items) {
                if (item.Description == description) {
                    combobox.SelectedItem = item;
                    return;
                }
            }
        }

        /// <summary>
        /// Sets the alpha.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns></returns>
        public static Color SetAlpha(this Color color, int alpha) {
            var newColor = Color.FromArgb(alpha, color);
            return newColor;
        }

        /// <summary>
        /// Gets the solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static SolidBrush GetSolidBrush(this Color color) {
            return new  SolidBrush(color);
        }

        /// <summary>
        /// Sets the off set.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static Point SetOffSet(this Point p, int x, int y) {
            return new Point(p.X + x, p.Y + y);
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static Font SetSize(this Font font, float size) {
            return new Font(font.FontFamily, size, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        }

        /// <summary>
        /// Sets the style.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public static Font SetStyle(this Font font, FontStyle style) {
            return new Font(font.FontFamily, font.Size, style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        }

        /// <summary>
        /// Gets the point.
        /// </summary>
        /// <param name="textpoint">The textpoint.</param>
        /// <returns></returns>
        public static Point GetPoint(this Keyrox.SourceCode.TextPoint textpoint) {
            return new Point(textpoint.X, textpoint.Y);
        }

        /// <summary>
        /// Gets the text point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Keyrox.SourceCode.TextPoint GetTextPoint(this Point point) {
            return new Keyrox.SourceCode.TextPoint(point.X, point.Y);
        }

        /// <summary>
        /// Adds the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static Point Add(this Point point, int x, int y) {
            return new Point(point.X + x, point.Y + y);
        }

        /// <summary>
        /// Adds the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static Point Add(this Point point, Point values) {
            return new Point(point.X + values.X, point.Y + values.Y);
        }

        /// <summary>
        /// Builds the supert tip.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="imgAutoList">The img auto list.</param>
        /// <returns></returns>
        public static ToolTipControllerShowEventArgs BuildSupertTip(this Keyword keyword, ImageCollection imgAutoList) {
            var superToolTip1 = new DevExpress.Utils.SuperToolTip();

            var toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            toolTipTitleItem1.Appearance.Image = imgAutoList.Images[keyword.ImageIndex];
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = imgAutoList.Images[keyword.ImageIndex];
            toolTipTitleItem1.Text = keyword.Title;
            toolTipTitleItem1.ImageToTextDistance = 5;
            toolTipTitleItem1.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
            superToolTip1.Items.Add(toolTipTitleItem1);

            if (!string.IsNullOrEmpty(keyword.Params)) {
                var toolTipItem1 = new DevExpress.Utils.ToolTipItem();
                toolTipItem1.LeftIndent = 6;
                toolTipItem1.Appearance.ForeColor = Color.DarkSlateGray;
                toolTipItem1.Font = new Font("Consolas", 8, FontStyle.Regular);
                toolTipItem1.Text = "Params";
                toolTipItem1.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                toolTipItem1.Appearance.Options.UseTextOptions = true;
                superToolTip1.Items.Add(toolTipItem1);

                var toolTipItem11 = new DevExpress.Utils.ToolTipItem();
                toolTipItem11.LeftIndent = 6;
                toolTipItem11.Font = new Font("Consolas", 8, FontStyle.Bold);
                toolTipItem11.Text = keyword.Params;
                superToolTip1.Items.Add(toolTipItem11);
            }
            if (!string.IsNullOrEmpty(keyword.Examples)) {
                var toolTipItem2 = new DevExpress.Utils.ToolTipItem();
                toolTipItem2.LeftIndent = 6;
                toolTipItem2.Appearance.ForeColor = Color.SlateGray;
                toolTipItem2.Font = new Font("Consolas", 8, FontStyle.Regular);
                toolTipItem2.Text = keyword.Examples;
                superToolTip1.Items.Add(toolTipItem2);
            }

            superToolTip1.Items.Add(new DevExpress.Utils.ToolTipSeparatorItem());
            var toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            toolTipTitleItem2.LeftIndent = 6;
            toolTipTitleItem2.Text = keyword.Description;
            superToolTip1.Items.Add(toolTipTitleItem2);

            superToolTip1.MaxWidth = 500;
            superToolTip1.FixedTooltipWidth = false;
            return new ToolTipControllerShowEventArgs() { SuperTip = superToolTip1, ToolTipType = ToolTipType.SuperTip };
        }

        /// <summary>
        /// Builds the error tip.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static ToolTipControllerShowEventArgs BuildErrorTip(this Keyrox.SourceCode.Word word) {
            var superToolTip1 = new DevExpress.Utils.SuperToolTip();
            var toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            toolTipTitleItem1.Appearance.Image = Resources.error_big;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = Resources.error_big;
            toolTipTitleItem1.Text = string.Concat("[Error]", Environment.NewLine, word.ErrorTip);
            toolTipTitleItem1.ImageToTextDistance = 5;
            toolTipTitleItem1.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.MaxWidth = 300;
            superToolTip1.FixedTooltipWidth = false;
            return new ToolTipControllerShowEventArgs() { SuperTip = superToolTip1, ToolTipType = ToolTipType.SuperTip };
        }

        /// <summary>
        /// Builds the warning tip.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static ToolTipControllerShowEventArgs BuildWarningTip(this Keyrox.SourceCode.Word word) {
            var superToolTip1 = new DevExpress.Utils.SuperToolTip();
            var toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            toolTipTitleItem1.Appearance.Image = Resources.warning_big;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = Resources.warning_big;
            toolTipTitleItem1.Text = string.Concat("[Warning]", Environment.NewLine, word.WarningTip);
            toolTipTitleItem1.ImageToTextDistance = 5;
            toolTipTitleItem1.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.MaxWidth = 300;
            superToolTip1.FixedTooltipWidth = false;
            return new ToolTipControllerShowEventArgs() { SuperTip = superToolTip1, ToolTipType = ToolTipType.SuperTip };
        }
    }
}
