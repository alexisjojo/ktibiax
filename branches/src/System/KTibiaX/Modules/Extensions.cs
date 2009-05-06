using System;
using DevExpress.XtraEditors;
using Keyrox.Shared.Objects;
using DevExpress.XtraEditors.Controls;
using System.Drawing;

namespace KTibiaX.Modules {
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
    }
}
