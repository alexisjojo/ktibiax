using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Controls;
using Keyrox.Scripting.Keywords;

namespace Keyrox.Scripting {
    public sealed class ScriptKeywords {

        /// <summary>
        /// Gets the keywords.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static KeywordCollection GetKeywords(ScriptEditor editor, AutoListType listType) {
            switch (listType) {
                case AutoListType.General: return GetGeneral();
                case AutoListType.Item: return GetItems();
                case AutoListType.Player: return GetPlayer();
                case AutoListType.Params: return GetParams(editor);
                case AutoListType.Sections: return GetSections(editor);
            }
            return default(KeywordCollection);
        }

        #region "[rgn] Reflection "
        public static Keyword ParseMethodInfo(MethodInfo method) {
            var res = new Keyword();

            #region " Title    "
            var title = GetAttribute<ActionTitle>(method);
            if (title != null) {
                res.Title = title.Title;
                res.Description = title.Description;
            }
            else { return null; }
            #endregion

            #region " Config   "
            var config = GetAttribute<ActionConfig>(method);
            if (config != null) {
                res.InputText = config.InputText;
                res.ImageIndex = config.ImageIndex;
                res.CaretPosition = config.CarretPosition;
                res.AutoListType = config.AutoList;
            }
            #endregion

            #region " Params   "
            var param = GetAttributes<ActionParameter>(method);
            if (param != null) {

                param = (from pm in param orderby pm.Index ascending select pm).ToArray();
                var sp = new StringBuilder();
                foreach (var p in param) {
                    sp.Append(string.Concat(p.Name, ": ", p.Description));
                    sp.Append(Environment.NewLine);
                }
                res.Params = sp.ToString();
            }
            #endregion

            #region " Examples "
            var ex = GetAttribute<ActionExamples>(method);
            if (ex != null) {
                res.Examples = ex.GetFormatedExamples();
            }
            #endregion

            return res;
        }
        public static T GetAttribute<T>(MethodInfo method) where T : class {
            var atts = method.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.First() as T : default(T);
        }
        public static T[] GetAttributes<T>(MethodInfo method) where T : class {
            var atts = method.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.Cast<T>().ToArray() : null;
        }
        public static T GetPropAttribute<T>(PropertyInfo property) where T : class {
            var atts = property.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.First() as T : default(T);
        }
        public static T GetClassAttribute<T>(Type type) where T : class {
            var atts = type.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.First() as T : default(T);
        }
        #endregion

        /// <summary>
        /// Gets the general keywords.
        /// </summary>
        /// <returns></returns>
        public static KeywordCollection GetGeneral() {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var res = new KeywordCollection();
            foreach (var type in types) {
                if (type.GetCustomAttributes(typeof(ActionClass), true).Length > 0) {
                    var methods = type.GetMethods();
                    foreach (var method in methods) {
                        var key = ParseMethodInfo(method);
                        if (key != null) { res.Add(key); }
                    }
                }
            }
            res.CurrentType = AutoListType.General;
            return res;
        }

        /// <summary>
        /// Gets the player keywords.
        /// </summary>
        /// <returns></returns>
        public static KeywordCollection GetPlayer() {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var res = new KeywordCollection();
            foreach (var type in types) {
                if (GetClassAttribute<PropClass>(type) != null) {
                    var props = type.GetProperties();

                    foreach (var prop in props) {
                        var item = GetPropAttribute<PropItem>(prop);
                        if (item != null) {
                            res.Add(new Keyword() {
                                Title = item.Name,
                                Description = item.Description,
                                InputText = item.InputText,
                                ImageIndex = GetClassAttribute<PropClass>(type).ImageIndex
                            });

                        }
                    }

                }
            }
            res.CurrentType = AutoListType.Player;
            return res;
        }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <returns></returns>
        public static KeywordCollection GetSections(ScriptEditor editor) {
            var res = new KeywordCollection();

            var secrows = (from row in editor.Document.Lines where row.ToString().Contains("#section") select row);
            foreach (var row in secrows) {

                var name = row.Replace("#section ", "").Replace("\"", "").TrimEnd(new[] { ' ' }).TrimStart(new[] { ' ' }).Replace("\r", "");
                res.Add(new Keyword() {
                    Title = name,
                    Description = "Returns the section name",
                    InputText = name,
                    ImageIndex = 6
                });
            }
            res.CurrentType = AutoListType.Sections;
            return res;
        }

        /// <summary>
        /// Gets the params.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <returns></returns>
        public static KeywordCollection GetParams(ScriptEditor editor) {
            var res = new KeywordCollection();
            var script = editor.Document.Text;
            var funcindex = script.IndexOf("setparam(");
            var funclen = "setparam".Length + 1;
            while (funcindex > -1) {

                var funcend = script.IndexOf(")", funcindex);
                if (funcend > funcindex && (((funcend + funclen) - funcindex) < 200)) {
                    //var key = script.Substring(funcindex + funclen, (funcend - funcindex)).Replace("\"", "");
                    var key = script.Substring(funcindex + funclen, (funcend - funcindex - funclen)).Replace("\"", "");
                   
                    if (key.IndexOf(",") > -1) { key = key.Substring(0, +key.IndexOf(",")); }

                    if (!string.IsNullOrEmpty(key)) {
                        res.Add(new Keyword() {
                            Title = key,
                            Description = "Returns the parameter name",
                            //InputText = string.Concat("\"", key, "\""),
                            InputText = string.Concat(key),
                            ImageIndex = 18
                        });
                    }
                }
                funcindex = script.IndexOf("setparam(", funcindex + 1);
            }
            res.CurrentType = AutoListType.Params;
            return res;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public static KeywordCollection GetItems() {
            var res = new KeywordCollection();
            foreach (var item in ItemKeywordCollection.Current) {
                res.Add(new Keyword() {
                    Title = item.Name,
                    Description = item.Description,
                    InputText = item.InputText.ToUpper(),
                    ImageIndex = 17
                });
            }
            res.CurrentType = AutoListType.Item;
            return res;
        }

        /// <summary>
        /// Finds the keyword.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="editor">The editor.</param>
        /// <returns></returns>
        public static Keyword FindKeyword(string text, ScriptEditor editor) {

            var list = GetKeywords(editor, AutoListType.General);
            var key = list.FindByInputText(text);
            if (key != null) { return key; }

            list = GetKeywords(editor, AutoListType.Item);
            key = list.FindByInputText(text);
            if (key != null) { return key; }

            list = GetKeywords(editor, AutoListType.Params);
            key = list.FindByInputText(text);
            if (key != null) { return key; }

            list = GetKeywords(editor, AutoListType.Player);
            key = list.FindByInputText(text);
            if (key != null) { return key; }

            list = GetKeywords(editor, AutoListType.Sections);
            key = list.FindByInputText(text);
            if (key != null) { return key; }

            return null;
        }
    }
}
