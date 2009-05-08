namespace Keyrox.Scripting.Keywords {
    public class Keyword {

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the input text.
        /// </summary>
        /// <value>The input text.</value>
        public string InputText { get; set; }

        /// <summary>
        /// Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        public int ImageIndex { get; set; }

        /// <summary>
        /// Gets or sets the params.
        /// </summary>
        /// <value>The params.</value>
        public string Params { get; set; }

        /// <summary>
        /// Gets or sets the examples.
        /// </summary>
        /// <value>The examples.</value>
        public string Examples { get; set; }

        /// <summary>
        /// Gets or sets the caret position.
        /// </summary>
        /// <value>The caret position.</value>
        public int CaretPosition { get; set; }

        /// <summary>
        /// Gets or sets the type of the auto list.
        /// </summary>
        /// <value>The type of the auto list.</value>
        public AutoListType AutoListType { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return !string.IsNullOrEmpty(Title)? Title : base.ToString();
        }
    }
}
