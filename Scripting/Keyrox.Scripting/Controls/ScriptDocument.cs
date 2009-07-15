using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Keyrox.Scripting.AutoList;
using Keyrox.Scripting.Controls;
using Keyrox.SourceCode;
using Keyrox.Windows.Forms.SyntaxBox;

namespace Keyrox.Scripting {
    public class ScriptDocument : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptDocument"/> class.
        /// </summary>
        public ScriptDocument() {
        }

        /// <summary>
        /// Setups the specified scriptbox.
        /// </summary>
        /// <param name="scriptbox">The scriptbox.</param>
        public void Setup(ScriptBox scriptbox) {
            this.Editor = scriptbox.Editor;
        }

        #region "[rgn] Properties     "
        public ScriptEditor Editor { get; set; }
        public SyntaxDocument Document { get { return Editor.Document; } }
        public Caret Caret { get { return Editor.Caret; } }
        public Span CurrentSpan { get { return Caret.CurrentSegment(); } }
        public Row CurrentRow { get { return Caret.CurrentRow; } }
        public Word CurrentWord { get { return Caret.CurrentWord; } }
        #endregion

        #region "[rgn] Helper Methods "
        public Point GetCarretPosition() {
            var pt = Editor.Painter.GetTextPointPixelPos(new TextPoint(Caret.Position.X, Caret.Position.Y));
            var x = pt.X;
            var y = pt.Y + 18;
            return new Point(x, y);
        }
        public string[] GetOperators() {
            return new[] { "+", "*", "/", "-", "=", ">", "<", "," };
        }
        private string GetCurrentFuntionName() {
            if (CurrentWord != null && CurrentRow != null) {
                for (var i = CurrentWord.Index; i > 0; i--) {
                    if (CurrentRow[i].Text == "(") {
                        return CurrentRow[i - 1].Text;
                    }
                    else if (GetOperators().Contains(CurrentRow[i].Text)) { break; }
                    else if (CurrentRow[i - 1].Text != "(" && CurrentRow[i].Text == ")") { break; }
                }
            }
            return string.Empty;
        }
        private bool InSegment(string start, string end) {
            if (CurrentWord != null) {
                if (CurrentWord.Text.StartsWith(start) || CurrentWord.Text.StartsWith(end)) {
                    return true;
                }
                else if ((CurrentWord.Index - 1) > 0 && (CurrentWord.Index + 1) < CurrentRow.Count) {
                    var left = CurrentRow[CurrentWord.Index - 1];
                    var right = CurrentRow[CurrentWord.Index + 1];
                    if (left.Text == start || right.Text == end) { return true; }
                }
                else if ((CurrentWord.Index - 2) > 0 && (CurrentWord.Index + 2) < CurrentRow.Count) {
                    var left = CurrentRow[CurrentWord.Index - 2];
                    var right = CurrentRow[CurrentWord.Index + 2];
                    if (left.Text == start || right.Text == end) { return true; }
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Shows the auto list.
        /// </summary>
        public void ShowAutoList() {
            Editor.ShowAutoList(GetCurrentAutoListType());
        }

        /// <summary>
        /// Gets the type of the correct.
        /// </summary>
        /// <returns></returns>
        private AutoListType GetCurrentAutoListType() {
            if (CurrentWord == null || CurrentWord.Text == " ") { return AutoListType.General; }
            else if (InSegment("[", "]")) { return AutoListType.Player; }
            else if (InSegment("{", "}")) { return AutoListType.Item; }

            var function = GetCurrentFuntionName();
            if (function == AutoListFunction.countitem.ToString()) { return AutoListType.Item; }
            else if (function == AutoListFunction.setloot.ToString()) { return AutoListType.Item; }
            else if (function == AutoListFunction.setparam.ToString()) { return AutoListType.Params; }
            else if (function == AutoListFunction.getparam.ToString()) { return AutoListType.Params; }
            else if (function == AutoListFunction.checkvalue.ToString()) { return AutoListType.General; }
            else if (function == AutoListFunction.getvalue.ToString()) { return AutoListType.General; }
            else if (function == AutoListFunction.gotosection.ToString()) { return AutoListType.Sections; }

            return AutoListType.None;
        }

    }
}
