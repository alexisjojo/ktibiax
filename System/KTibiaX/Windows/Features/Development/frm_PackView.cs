using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace KTibiaX.Windows.Features.Development {
    public partial class frm_PackView : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_PackView"/> class.
        /// </summary>
        public frm_PackView() {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetText(string text) {
            memoEdit1.Text = text;
        }
    }
}