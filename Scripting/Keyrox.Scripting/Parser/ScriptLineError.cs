using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Properties;
using System.Drawing;
using Keyrox.SourceCode;
using System.Xml.Serialization;

namespace Keyrox.Scripting.Parser {
    public class ScriptLineError {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLineError"/> class.
        /// </summary>
        public ScriptLineError() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLineError"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="word">The word.</param>
        /// <param name="type">The type.</param>
        public ScriptLineError(string message, Word word, ScriptLineErrorType type) {
            this.Message = message;
            this.ErrorType = type;
            this.Word = word;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLineError"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="row">The row.</param>
        /// <param name="type">The type.</param>
        public ScriptLineError(string message, Row row, ScriptLineErrorType type) {
            this.Message = message;
            this.ErrorType = type;
            this.Row = row;
        }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        /// <value>The type of the error.</value>
        public ScriptLineErrorType ErrorType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the word.
        /// </summary>
        /// <value>The word.</value>
        public Word Word { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public Row Row { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber {
            get {
                if (Word != null) { return Word.Row.Index + 1; }
                else if (Row != null) { return Row.Index + 1; }
                return -1;
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Image Icon {
            get { return ErrorType == ScriptLineErrorType.Error ? Resources.error : Resources.warning; }
            set { return; }
        }

    }
}

