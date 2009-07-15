using System;
using System.ComponentModel;
using Keyrox.Scripting.Events;
using Keyrox.Scripting.Parser;
using Keyrox.Scripting.Util;
using Tibia.Client;

namespace Keyrox.Scripting.Controls {
    public class ScriptRunner : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptRunner"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="client">The client.</param>
        public ScriptRunner(ScriptFile file, TibiaClient tibiaClient) {
            this.File = file;
            this.TibiaClient = tibiaClient;
        }

        #region "[rgn] Properties  "
        public TibiaClient TibiaClient { get; set; }
        public ScriptFile File { get; private set; }
        public ScriptLine CurrentRow { get { return File.Rows[CurrentLineIndex]; } }
        public RunnerState State { get; private set; }

        public int CurrentLineIndex { get; private set; }
        public int CurrentLineIndexToGoBack { get; private set; }
        public bool InDebugMode { get; set; }
        #endregion

        #region "[rgn] Ctrl Events "
        public event EventHandler<ExceptionEventArgs> OnException;
        public event EventHandler<ScriptExceptionEventArgs> OnScriptException;
        public event EventHandler OnScriptStop;
        public event EventHandler<ScriptLineEventArgs> OnRowBeginExecute;
        public event EventHandler<ScriptLineEventArgs> OnRowEndExecute;
        #endregion

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Start(bool debug) {
            this.CurrentLineIndex = 0;
            this.InDebugMode = debug;
            this.State = RunnerState.Running;
            new Callback(ExecuteCurrentLine).BeginInvoke(null, this);
        }

        /// <summary>
        /// Resumes the current line.
        /// </summary>
        public void ResumeExecution() {
            new Callback(ExecuteCurrentLine).BeginInvoke(null, this);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        protected void ExecuteCurrentLine() {
            try {
                if (State == RunnerState.Running) {
                    var row = File.Rows[CurrentLineIndex];
                    if (OnRowBeginExecute != null) { OnRowBeginExecute(this, new ScriptLineEventArgs(row)); }

                    row.OnActionComplete += row_OnActionComplete;
                    row.Execute(TibiaClient);
                }
            }
            catch (ScriptActionException ex) { if (OnScriptException != null) { OnScriptException(this, new ScriptExceptionEventArgs(ex, CurrentLineIndex)); Stop(); return; } }
            catch (Exception ex) { if (OnScriptException != null) { OnException(this, new ExceptionEventArgs(ex)); Stop(); return; } }
        }

        /// <summary>
        /// Handles the OnActionComplete event of the row control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Keyrox.Scripting.Events.ScriptLineEventArgs"/> instance containing the event data.</param>
        public void row_OnActionComplete(object sender, Keyrox.Scripting.Events.ScriptLineEventArgs e) {
            if (e.Line.Result.MustReExecute) {
                if (InDebugMode) { if (OnRowEndExecute != null) { OnRowEndExecute(this, new ScriptLineEventArgs(e.Line)); } return; }
                else { ExecuteCurrentLine(); return; }
            }
            if (!e.Line.Result.Success) { Stop(); return; }

            if (e.Line.IsSection) { CurrentLineIndexToGoBack = 0; }
            if (e.Line.IsEndSection && CurrentLineIndexToGoBack > 0) {
                CurrentLineIndex = CurrentLineIndexToGoBack;
                CurrentLineIndexToGoBack = 0;
                return;
            }
            if (e.Line.Result.MustPause) { Pause(); return; }
            if (e.Line.Result.MustStop) { Stop(); return; }

            if (e.Line.Result.SectionToJump != null) { CurrentLineIndex = e.Line.Result.SectionToJump.StartLine.LineIndex; return; }
            if (e.Line.Result.SectionToExecute != null) { CurrentLineIndexToGoBack = CurrentLineIndex; CurrentLineIndex = e.Line.Result.SectionToExecute.StartLine.LineIndex + 1; return; }
            if (e.Line.Result.LineToJump != null) { CurrentLineIndex = e.Line.Result.LineToJump.LineIndex; return; }

            var row = File.GetNextLine(CurrentLineIndex);
            if (row != null) { CurrentLineIndex = (row.LineIndex - 1); }
            else { Stop(); } //End of script.

            if (InDebugMode) { if (OnRowEndExecute != null) { OnRowEndExecute(this, new ScriptLineEventArgs(e.Line)); } return; }
            else { ExecuteCurrentLine(); return; }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop() {
            this.State = RunnerState.Stoped;
            this.CurrentLineIndex = 0;
            if (OnScriptStop != null) { OnScriptStop(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause() {
            this.State = RunnerState.Paused;
        }
    }
}
