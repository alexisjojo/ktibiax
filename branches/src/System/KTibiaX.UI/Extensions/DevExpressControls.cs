using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Keyrox.Shared.Objects;
using Keyrox.Shared.Enumerators;
using System;
using System.Collections.Specialized;

namespace KTibiaX.UI.Extensions {
    public static class DevExpressControls {

        /// <summary>
        /// Convert the list to ImageListBoxItem array.
        /// </summary>
        public static ImageListBoxItem[] ToImageListItems<T>(this IList<T> list) {
            var newList = new List<ImageListBoxItem>();
            foreach (var obj in list) {
                newList.Add(new ImageListBoxItem(obj, -1));
            }
            return newList.ToArray();
        }

        /// <summary>
        /// Convert the list to ImageListBoxItem array.
        /// </summary>
        public static ImageListBoxItem[] ToImageListItems<T>(this IEnumerable<T> list, int imageIndex) {
            var newList = new List<ImageListBoxItem>();
            foreach (var obj in list) {
                newList.Add(new ImageListBoxItem(obj, imageIndex));
            }
            return newList.ToArray();
        }

        /// <summary>
        /// Convert the list to ImageListBoxItem array.
        /// </summary>
        public static ImageListBoxItem[] ToImageListItems<T>(this IEnumerable<T> list) {
            return ToImageListItems(list, -1);
        }

        /// <summary>
        /// Convert the list to ImageComboBoxItem array.
        /// </summary>
        public static ImageComboBoxItem[] ToImageComboItems<T>(this IList<T> list) {
            var newList = new List<ImageComboBoxItem>();
            foreach (var obj in list) {
                newList.Add(new ImageComboBoxItem(obj, -1));
            }
            return newList.ToArray();
        }

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemCollection">The item collection.</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this BaseListBoxControl.SelectedItemCollection itemCollection) {
            var list = new List<T>();
            foreach (ImageListBoxItem item in itemCollection) {
                if (item.Value.GetType() == typeof(ImageListBoxItem)) {
                    // ReSharper disable PossibleNullReferenceException
                    list.Add((T)(item.Value as ImageListBoxItem).Value);
                    // ReSharper restore PossibleNullReferenceException
                }
                else { list.Add((T)item.Value); }
            }
            return list;
        }

        /// <summary>
        /// Adds the specified listbox.
        /// </summary>
        /// <param name="listbox">The listbox.</param>
        /// <param name="items">The items.</param>
        public static void Add(this ListBoxControl listbox, StringCollection items) {
            if (items != null) {
                foreach (var item in items) listbox.Items.Add(item);
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="listbox">The listbox.</param>
        /// <returns></returns>
        public static StringCollection GetItems(this ListBoxControl listbox) {
            var result = new StringCollection();
            foreach (string item in listbox.Items) result.Add(listbox.GetItemText(listbox.Items.IndexOf(item)));
            return result;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="description">The description.</param>
        /// <param name="value">The value.</param>
        /// <param name="imageIndex">Index of the image.</param>
        public static void AddItem(this ImageComboBoxEdit comboBoxEdit, string description, object value, int imageIndex) {
            comboBoxEdit.Properties.Items.Add(new ImageComboBoxItem(description, value, imageIndex));
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="description">The description.</param>
        /// <param name="value">The value.</param>
        public static void AddItem(this ImageComboBoxEdit comboBoxEdit, string description, object value) {
            AddItem(comboBoxEdit, description, value, -1);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static ImageComboBoxItem GetItem(this ImageComboBoxEdit comboBoxEdit, int index) {
            return comboBoxEdit.Properties.Items[index];
        }

        /// <summary>
        /// Gets the selected item.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <returns></returns>
        public static ImageComboBoxItem GetSelectedItem(this ImageComboBoxEdit comboBoxEdit) {
            return comboBoxEdit.Properties.Items[comboBoxEdit.SelectedIndex];
        }

        /// <summary>
        /// Clears the items.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        public static void ClearItems(this ImageComboBoxEdit comboBoxEdit) {
            comboBoxEdit.Properties.Items.Clear();
        }

        /// <summary>
        /// Adds the range items.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="items">The items.</param>
        public static void AddRangeItems(this ImageComboBoxEdit comboBoxEdit, ImageComboBoxItem[] items) {
            if (!items.IsNull()) {
                comboBoxEdit.Properties.Items.AddRange(items);
            }
        }

        /// <summary>
        /// Adds the range items.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="items">The items.</param>
        /// <param name="imageIndex">Index of the image.</param>
        public static void AddRangeItems(this ImageComboBoxEdit comboBoxEdit, ImageComboBoxItem[] items, int imageIndex) {
            if (!items.IsNull()) {
                foreach (ImageComboBoxItem item in items) { item.ImageIndex = imageIndex; }
                comboBoxEdit.Properties.Items.AddRange(items);
            }
        }

        /// <summary>
        /// Adds if not exist.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="items">The items.</param>
        public static void AddIfNotExist(this ImageComboBoxEdit comboBoxEdit, ImageComboBoxItem[] items) {
            foreach (ImageComboBoxItem item in items) {
                if (!comboBoxEdit.Properties.Items.Contains(item)) {
                    comboBoxEdit.Properties.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Adds the new items.
        /// </summary>
        /// <param name="listBoxControl">The list box control.</param>
        /// <param name="items">The items.</param>
        /// <param name="clear">if set to <c>true</c> [clear].</param>
        public static void AddItems(this ImageListBoxControl listBoxControl, object[] items, bool clear) {
            listBoxControl.BeginUpdate();
            if (clear) listBoxControl.Items.Clear();
            listBoxControl.Items.AddRange(items);
            listBoxControl.EndUpdate();
        }

        /// <summary>
        /// Sets the index of the image.
        /// </summary>
        /// <param name="listbox">The listbox.</param>
        /// <param name="imageIndex">Index of the image.</param>
        public static void SetImageIndex(this ImageListBoxControl listbox, int imageIndex) {
            foreach (ImageListBoxItem item in listbox.Items) {
                item.ImageIndex = imageIndex;
            }
        }

        /// <summary>
        /// Adds the new items.
        /// </summary>
        /// <param name="listBoxControl">The list box control.</param>
        /// <param name="items">The items.</param>
        public static void AddItems(this ImageListBoxControl listBoxControl, object[] items) {
            AddItems(listBoxControl, items, true);
        }


        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <returns></returns>
        public static object GetSelectedValue(this ImageComboBoxEdit comboBoxEdit) {
            var item = comboBoxEdit.SelectedItem as ImageComboBoxItem;
            return item != null ? item.Value : null;
        }


        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listBoxEdit">The list box edit.</param>
        /// <returns></returns>
        public static T GetSelectedValue<T>(this ImageListBoxControl listBoxEdit) {
            var item = listBoxEdit.SelectedItem as ImageListBoxItem;
            return item != null ? (T)item.Value : default(T);
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="comboBoxEdit">The combo box edit.</param>
        /// <param name="value">The value.</param>
        public static void SelectItem<T>(this ImageComboBoxEdit comboBoxEdit, T value) {
            foreach (ImageComboBoxItem item in comboBoxEdit.Properties.Items) {
                if (item.Value.To<T>().Equals(value)) {
                    comboBoxEdit.SelectedIndex = comboBoxEdit.Properties.Items.IndexOf(item);
                    return;
                }
            }
        }

        public static List<T> GetItems<T>(this CheckedComboBoxEdit combo) {
            var result = new List<T>();
            foreach (CheckedListBoxItem item in combo.Properties.Items) {
                if (item.CheckState == System.Windows.Forms.CheckState.Checked) {
                    result.Add(item.Value.ToString().To<T>());
                }
            }
            return result;
        }

        /// <summary>
        /// Add All Enum Items on defined Image Combo Box.
        /// </summary>
        /// <param name="combo">Combo Box to add items.</param>
        /// <param name="enumType">Enum to get items.</param>
        public static void AddEnumItems(this ImageComboBoxEdit combo, Type enumType) {
            combo.Properties.Items.BeginUpdate();
            Array items = Enum.GetValues(enumType);
            foreach (Enum item in items) {
                string enumText = item.Description() != "" ? item.Description() : item.ToString();
                combo.Properties.Items.Add(new ImageComboBoxItem(enumText, item.GetHashCode(), -1));
            }
            combo.Properties.Items.EndUpdate();
        }

        /// <summary>
        /// Removes the enum item.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="item">The item.</param>
        public static void RemoveEnumItem(this ImageComboBoxEdit combo, Enum item) {
            foreach (ImageComboBoxItem ddlItem in combo.Properties.Items) {
                if (ddlItem.Value == item) {
                    combo.Properties.Items.Remove(ddlItem);
                }
            }
        }

        /// <summary>
        /// Add All Enum Items on defined Image Combo Box.
        /// </summary>
        /// <param name="combo">Combo Box to add items.</param>
        /// <param name="enumType">Enum to get items.</param>
        /// <param name="clear">Clear all combo items before.</param>
        public static void AddEnumItems(this ImageComboBoxEdit combo, Type enumType, bool clear) {
            if (clear) { combo.Properties.Items.Clear(); }
            AddEnumItems(combo, enumType);
        }

        public static void DisableButton(this ButtonEdit control, string caption) {
            foreach (EditorButton btn in control.Properties.Buttons) {
                if (btn.Caption.ToLower().Trim() != caption.ToLower().Trim()) continue;
                btn.Enabled = false; return;
            }
        }

        public static void DisableButton(this ButtonEdit control, object tag) {
            foreach (EditorButton btn in control.Properties.Buttons) {
                if (btn.Tag.Equals(tag)) continue;
                btn.Enabled = false; return;
            }
        }

        public static void DisableButton(this ButtonEdit control, ButtonPredefines kind) {
            foreach (EditorButton btn in control.Properties.Buttons) {
                if (btn.Kind == kind) continue;
                btn.Enabled = false; return;
            }
        }

        public static void EnableButton(this ButtonEdit control, string caption) {
            foreach (EditorButton btn in control.Properties.Buttons) {
                if (btn.Caption.ToLower().Trim() != caption.ToLower().Trim()) continue;
                btn.Enabled = true; return;
            }
        }

        public static void EnableButton(this ButtonEdit control, object tag) {
            foreach (EditorButton btn in control.Properties.Buttons) {
                if (btn.Tag.Equals(tag)) continue;
                btn.Enabled = true; return;
            }
        }

        public static void EnableButton(this ButtonEdit control, ButtonPredefines kind) {
            foreach (EditorButton btn in control.Properties.Buttons) {
                if (btn.Kind == kind) continue;
                btn.Enabled = true; return;
            }
        }
    }
}
