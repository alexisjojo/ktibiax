using System;
using Keyrox.Scripting.Actions;
using Keyrox.Scripting.Events;
using Tibia.Client;
using Keyrox.Scripting.Util;

namespace Keyrox.Scripting.Parser {
    public class ScriptLine {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLine"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="lineIndex">Index of the line.</param>
        /// <param name="text">The text.</param>
        /// <param name="file">The file.</param>
        public ScriptLine(ScriptAction action, int lineIndex, string text, ScriptFile file) {
            this.File = file;
            this.ScriptAction = action;
            this.ActionName = action != null ? action.Title.Title : string.Empty;
            this.LineIndex = lineIndex;
            this.Text = text;
        }

        #region "[rgn] Properties     "
        public TibiaClient TibiaClient { get; set; }

        public ScriptFile File { get; set; }
        public ScriptAction ScriptAction { get; private set; }
        public ScriptActionResult Result { get; private set; }

        public int LineIndex { get; private set; }
        public int NumberLine { get { return LineIndex + 1; } }

        public string Text { get; private set; }
        public string ActionName { get; private set; }
        public string[] Args { get; set; }

        private string functionText;
        public string FunctionText {
            get {
                if (string.IsNullOrEmpty(functionText) && !string.IsNullOrEmpty(Text) && Text.IndexOf("(") > 0) {
                    functionText = Text.Substring(0, Text.IndexOf("("));
                }
                return functionText;
            }
        }

        private bool SectionInitialized { get; set; }
        private ScriptSection section;
        public ScriptSection Section { get { if (section == null && !SectionInitialized) { GetParentSection(); } return section; } }

        public bool IsSection { get { return Text != null ? Text.StartsWith("#section") : false; } }
        public bool IsEndSection { get { return Text != null ? Text.StartsWith("#endsection") : false; } }
        #endregion

        /// <summary>
        /// Gets the parent section.
        /// </summary>
        private void GetParentSection() {
            SectionInitialized = true;
            ScriptLine endSection = null;
            for (int i = LineIndex + 1; i < File.Rows.Count; i++) {
                if (File.Rows[i].Text.StartsWith("#section")) { return; }
                else if (File.Rows[i].Text.StartsWith("#endsection")) { endSection = File.Rows[i]; break; }
            }
            if (endSection == null) { return; }
            ScriptLine startSection = null;
            for (int i = LineIndex; i > 0; i--) {
                if (File.Rows[i].Text.StartsWith("#endsection")) { return; }
                else if (File.Rows[i].Text.StartsWith("#section")) { startSection = File.Rows[i]; break; }
            }
            if (startSection == null) { return; }
            section = new ScriptSection(startSection.Text.Replace("#section", "").TrimStart(' ').TrimEnd(' '), startSection, endSection);
        }

        /// <summary>
        /// Occurs when [on action complete].
        /// </summary>
        public event EventHandler<ScriptLineEventArgs> OnActionComplete;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute(TibiaClient client) {
            try {
                if (this.ScriptAction != null) {
                    TibiaClient = client;
                    if (File == null) { throw new ScriptActionException(File.ScriptInfo, "Script File is null on line: " + this.NumberLine); }
                    if (TibiaClient == null) { throw new ScriptActionException(File.ScriptInfo, "Tibia client is null on line: " + this.NumberLine); }

                    var actionclass = Context.GetAction(this.ScriptAction.ActionClass, TibiaClient, File.ScriptInfo);
                    Args = File.ScriptInfo.NormalizeActionArguments(Args);
                    Result = (ScriptActionResult)ScriptAction.Method.Invoke(actionclass, new object[] { Args });
                }
                else {
                    Result = new ScriptActionResult() { Success = true };
                }
                ExecutionComplete(null);
            }
            catch (Exception ex) { throw new ExecutionEngineException("An error ocurred executing the line number: '" + NumberLine + "'\n\n" + ex.Message, ex); }
        }

        /// <summary>
        /// Occurs when [LineAction execution complete].
        /// </summary>
        /// <param name="result">The result.</param>
        private void ExecutionComplete(IAsyncResult result) {
            if (OnActionComplete != null) { OnActionComplete(this, new ScriptLineEventArgs(this)); }
        }

    }
}
