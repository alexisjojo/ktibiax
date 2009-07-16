using System.Collections.Generic;
using System.Linq;

namespace Keyrox.Scripting.Keywords {
    public class KeywordCollection : List<Keyword> {

        /// <summary>
        /// Gets or sets the type of the current.
        /// </summary>
        /// <value>The type of the current.</value>
        public AutoListType CurrentType { get; set; }

        /// <summary>
        /// Finds the by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public Keyword FindByTitle(string title) {
            var res = this.Where(k => k.Title == title);
            return res.Count() > 0 ? res.First() : null;
        }

        /// <summary>
        /// Sort by the title.
        /// </summary>
        /// <returns>A sorted KeywordCollection.</returns>
        public KeywordCollection SortbyTitle() {
            var res = new KeywordCollection();
            res.AddRange((from key in this orderby key.Title ascending select key).ToList());
            return res;
        }

        /// <summary>
        /// Finds the by input text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public Keyword FindByInputText(string text) {
            var res = this.Where(k => k.InputText
                .Replace(", 0", "")
                .Replace("\"", "")
                .Replace("()","")
                .Replace("[", "")
                .Replace("]","")
                .Replace("{", "")
                .Replace("}", "")
                == text);
            return res.Count() > 0 ? res.First() : null;
        }

    }
}
