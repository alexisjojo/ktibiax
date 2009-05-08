using System.Collections.Generic;
using Keyrox.Scripting.Properties;
using System.IO;
using System;
using System.Linq;
using System.Windows.Forms;
using Keyrox.Shared.Files;
using Keyrox.Shared.Objects;
using Keyrox.Scripting.Controls;
using System.Text;
using System.ComponentModel;

namespace Keyrox.Scripting.Keywords {
    public class ItemKeywordCollection : List<ItemKeyword> {

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static ItemKeywordCollection Current {
            get {
                if (Settings.Default.KeyItems == null) { Load(); }
                return Settings.Default.KeyItems;
            }
            set { 
                Settings.Default.KeyItems = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public static void Save() {
            Settings.Default.Save();
            var fi = new FileInfo(Path.Combine(Application.StartupPath, "ScriptConfig\\CustomItems.xml"));
            fi.Write(Settings.Default.KeyItems.Serialize());
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load() {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, "ScriptConfig\\CustomItems.xml"));
            if (fi.Exists) {
                Settings.Default.KeyItems = fi.Read().Deserialize<ItemKeywordCollection>();
                Settings.Default.Save();
            }
            else {
                Settings.Default.KeyItems = new ItemKeywordCollection();
                Save();
            }
        }

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Remove(int id) {
            var items = Current.Where(i => i.ID == id);
            if (items.Count() > 0) {
                Current.Remove(items.First());
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
