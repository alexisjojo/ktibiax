using System;

namespace Keyrox.Builder.Features.Controls {
    public partial class OutputView : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputView"/> class.
        /// </summary>
        public OutputView() {
            InitializeComponent();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear() {
            this.Invoke(new Callback(delegate() {
                memoEdit1.Text = string.Empty;
            }));
        }

        /// <summary>
        /// Adds the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Add(string text) {
            this.Invoke(new Callback(delegate() {
                memoEdit1.Text += string.Concat("[", DateTime.Now.ToString("hh:mm:ss"), "] - ", text, Environment.NewLine);
                memoEdit1.SelectionStart = memoEdit1.Text.Length - 1;
                memoEdit1.SelectionLength = 1;
                memoEdit1.ScrollToCaret();
            }));
        }

    }
}
