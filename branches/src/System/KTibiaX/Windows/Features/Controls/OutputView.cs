using System;
using System.Text;
using Keyrox.Shared.Controls;
using KTibiaX.UI.Controls;
using KTibiaX.UI.Components;
using System.Windows.Forms;
using Keyrox.Shared.Files;

namespace KTibiaX.Windows.Features.Controls {
    public partial class OutputView : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputView"/> class.
        /// </summary>
        public OutputView() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the OutputView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OutputView_Load(object sender, EventArgs e) {
            TMOutput.Start();
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
        }

        /// <summary>
        /// Handles the ThreadExit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Application_ThreadExit(object sender, EventArgs e) {
            var fi = new System.IO.FileInfo(System.IO.Path.Combine(Application.StartupPath, Properties.Settings.Default.OutputFilePath));
            if (fi.Exists) { fi.Delete(); }
            fi.Write(txtOutput.Text);
        }

        /// <summary>
        /// Scrolls the output.
        /// </summary>
        public void ScrollOutput() {
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.SelectionLength = 0;
            txtOutput.ScrollToCaret();
        }

        /// <summary>
        /// Handles the Tick event of the TMOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TMOutput_Tick(object sender, EventArgs e) {
            var line = Output.Read();
            if (line != null && line.Type == Keyrox.Shared.Messaging.MessageType.Normal) {

                if (line != null) {
                    var sb = new StringBuilder();
                    if (!line.Content.StartsWith("-")) {
                        sb.Append(line.SentDate.ToString("hh:mm:ss"));
                        sb.Append(" - ");
                    }
                    sb.Append(line.Content);
                    sb.Append(Environment.NewLine);
                    txtOutput.Text += sb.ToString();
                    MethodCall.ExecuteSafeThreadIn(ScrollOutput, 100);
                }
            }
        }
    }
}
