using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Keyrox.Builder.Modules;
using Keyrox.Scripting.Parser;

namespace Keyrox.Builder.Features.Controls {
    public partial class ErrorList : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorList"/> class.
        /// </summary>
        public ErrorList() {
            InitializeComponent();
        }

        #region "[rgn] Properties     "
        private List<ScriptLineError> errorSource;
        [Browsable(false)]
        protected List<ScriptLineError> ErrorSource {
            get {
                if (errorSource == null) {
                    errorSource = new List<ScriptLineError>();
                    gridControl1.DataSource = errorSource;
                }
                return errorSource;
            }
            set { errorSource = value; }
        }
        #endregion

        #region "[rgn] Event Handler  "
        private void gridView1_Click(object sender, EventArgs e) {
            GoToSelectedError();
        }
        private void gridControl1_Click(object sender, EventArgs e) {
            GoToSelectedError();
        }
        #endregion

        #region "[rgn] Helper Methods "
        public int Count() {
            return ErrorSource.Count;
        }
        public int CountError() {
            return ErrorSource.Where(er => er.ErrorType == ScriptLineErrorType.Error).Count();
        }
        public int CountWarning() {
            return ErrorSource.Where(er => er.ErrorType == ScriptLineErrorType.Warning).Count();
        }
        #endregion

        /// <summary>
        /// Adds the specified error.
        /// </summary>
        /// <param name="error">The error.</param>
        public void Add(ScriptLineError error) {
            this.Invoke(new Callback(delegate() {
                if (gridControl1.DataSource == null) { gridControl1.DataSource = ErrorSource; }
                ((List<ScriptLineError>)gridControl1.DataSource).Add(error);
                gridView1.RefreshData();
            }));
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear() {
            this.Invoke(new Callback(delegate() {
                if (gridControl1.DataSource == null) { gridControl1.DataSource = ErrorSource; }
                ((List<ScriptLineError>)gridControl1.DataSource).Clear();
                gridView1.RefreshData();
            }));
        }

        /// <summary>
        /// Goes to selected error.
        /// </summary>
        protected void GoToSelectedError() {
            this.Invoke(new Callback(delegate() {
                var editform = this.ParentForm as frm_Editor;
                if (editform != null && gridView1.FocusedRowHandle > -1) {
                    var error = ErrorSource[gridView1.FocusedRowHandle];
                    if (error.Word != null) { editform.GoToWord(error.Word); }
                    else if (error.Row != null) { editform.GoToRow(error.Row); }
                }
            }));
        }
    }
}
