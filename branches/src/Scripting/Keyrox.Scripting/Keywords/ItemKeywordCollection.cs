using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Keyrox.Scripting.Properties;
using Keyrox.Shared.Files;
using Keyrox.Shared.Objects;

namespace Keyrox.Scripting.Keywords {
    public class ItemKeywordCollection : List<ItemKeyword> {

        private static ItemKeywordCollection current;

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static ItemKeywordCollection Current {
            get {
                if (current == null) { current = new ItemKeywordCollection(); current.Load(); }
                return current;
            }
            set {
                current = value;
                current.Save();
                Settings.Default.KeyItems = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save() {
            Settings.Default.Save();
            var fi = new FileInfo(Path.Combine(Application.StartupPath, "ScriptConfig\\CustomItems.xml"));
            fi.Write(Settings.Default.KeyItems.Serialize());
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load() {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, "ScriptConfig\\CustomItems.xml"));
            if (fi.Exists) {
                Settings.Default.KeyItems = fi.Read().Deserialize<ItemKeywordCollection>();
                Settings.Default.Save();
            }
            else {
                Settings.Default.KeyItems = new ItemKeywordCollection();
                Save();
            }
            this.Clear();
            this.AddRange(Settings.Default.KeyItems);
        }

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Remove(int id) {
            var items = this.Where(i => i.ID == id);
            if (items.Count() > 0) {
                this.Remove(items.First());
                Save();
            }
        }

        /// <summary>
        /// Updates the syn file.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public void UpdateSynFile(string synpath) {
            var syn = new FileInfo(synpath);
            var res = "(--%ItemsComeHere%--)";            
            if (syn.Exists) {
                var doc = syn.Read();
                doc = doc.Replace(res, GetItemsPatterns());
                syn.Write(doc);
            }            
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ItemKeyword GetItem(string name) {
            var res = from i in this where i.Name == name select i;
            return res.Count() > 0 ? res.First() : null;
        }

        /// <summary>
        /// Gets the item by input text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public ItemKeyword GetItemByInputText(string text) {
            var res = from i in this where i.InputText == text select i;
            return res.Count() > 0 ? res.First() : null;
        }

        /// <summary>
        /// Gets the items patterns.
        /// </summary>
        /// <returns></returns>
        private string GetItemsPatterns() {
            var sb = new StringBuilder();
            this.ForEach(i => sb.Append(string.Concat("          ", i.Name.Trim().ToUpper(), " ", Environment.NewLine)));
            return sb.ToString();
        }
    }
}
