using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Infra.Transaction {
    public class TraceListner : System.Diagnostics.TraceListener {

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceListner"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public TraceListner(TraceListenerType type) {
            switch (type) {
                case TraceListenerType.Console: InitializeConsole(); break;
                case TraceListenerType.File: InitializeFile(); break;
            }
        }

        #region " Internal Properties "
        public System.Diagnostics.Process Console { get; set; }
        private System.IO.FileInfo File { get; set; }
        public bool ForceLogWindowToExist { get; set; }
        public TraceListenerType StorageType { get; set; }
        #endregion

        #region " Console Actions     "
        /// <summary>
        /// Initializes the trace console window.
        /// </summary>
        public void InitializeConsole() {
            var pi = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + Properties.Settings.Default.CMDFileName);
            pi.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
            pi.UseShellExecute = false;
            pi.RedirectStandardInput = true;
            pi.CreateNoWindow = false;
            pi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            Console = System.Diagnostics.Process.Start(pi);
            Console.Exited += new EventHandler(Console_Exited);
            StorageType = TraceListenerType.Console;
        }
        /// <summary>
        /// Handles the Exited event of the Console control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Console_Exited(object sender, EventArgs e) {
            if (ForceLogWindowToExist) { InitializeConsole(); }
        }
        #endregion

        #region " TraceFile Actions   "
        /// <summary>
        /// Initializes the trace log file.
        /// </summary>
        public void InitializeFile() {
            string traceFile = Environment.CurrentDirectory + "\\" + Properties.Settings.Default.TraceFileName;
            if (System.IO.File.Exists(traceFile)) System.IO.File.Delete(traceFile);
            System.IO.File.Create(traceFile);
            File = new System.IO.FileInfo(traceFile);
            StorageType = TraceListenerType.File;
        }
        #endregion

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void Write(string message) {
            switch (StorageType) {
                case TraceListenerType.Console:
                    Console.StandardInput.Write(message);
                    Console.StandardInput.Flush();
                    Console.Refresh();
                    break;
                case TraceListenerType.File:
                    var writer = File.AppendText();
                    try {
                        writer.Write(message);
                        writer.Flush();
                        Console.Refresh();
                    }
                    finally { writer.Close(); }
                    break;
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void WriteLine(string message) {
            switch (StorageType) {
                case TraceListenerType.Console:
                    if (Console != null && !Console.HasExited) {
                        Console.StandardInput.WriteLine(message);
                    }
                    break;
                case TraceListenerType.File:
                    var writer = File.AppendText();
                    try {
                        writer.WriteLine(message);
                    }
                    finally { writer.Close(); }
                    break;
            }
        }

    }
}
