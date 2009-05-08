using System;
using Keyrox.Scripting.Actions;
using Keyrox.Scripting.Events;
using Tibia.Client;

namespace Keyrox.Scripting.Parser {
    public class ScriptLine {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLine"/> class.
        /// </summary>
        public ScriptLine(ScriptFile file, TibiaClient client) {
            this.TibiaClient = client;
            this.File = file;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLine"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="text">The text.</param>
        public ScriptLine(ScriptAction action, int lineNumber, string text, ScriptFile file, TibiaClient client)
            : this(file, client) {
            this.ScriptAction = action;
            this.ActionName = action.Title.Title;
            this.LineNumber = lineNumber;
            this.Text = text;
        }

        #region "[rgn] Properties "
        public TibiaClient TibiaClient { get; set; }
        public ScriptFile File { get; set; }
        public ScriptAction ScriptAction { get; private set; }

        public string Text { get; private set; }
        public string SectionName { get; set; }
        public string ActionName { get; private set; }
        public string[] Args { get; set; }

        public bool IgnoreRow { get; set; }
        public int LineNumber { get; private set; }
        #endregion

        /// <summary>
        /// Occurs when [on action complete].
        /// </summary>
        public event EventHandler<ScriptLineEventArgs> OnActionComplete;
        
        /// <summary>
        /// Begins the execute.
        /// </summary>
        public void BeginExecute(TibiaClient client) {
            if (ScriptAction.Method != null) {
                Callback action = Execute;
                action.BeginInvoke(ExecutionComplete, action);
            }
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        protected void Execute() {
            try {
                if (TibiaClient == null) { throw new ArgumentException("Tibia client is null!"); }
                if (File == null) { throw new ArgumentException("Script file is null!"); }

                var actionclass = Activator.CreateInstance(this.ScriptAction.GetType());
                ((ITibiaAction)actionclass).Client = TibiaClient;
                ((ITibiaAction)actionclass).Script = File.ScriptInfo;
                ScriptAction.Method.Invoke(actionclass, Args);
            }
            catch (Exception ex) { throw new ExecutionEngineException("An error ocurred executing the line number: '" + LineNumber + "'\n\n" + ex.Message); }
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
