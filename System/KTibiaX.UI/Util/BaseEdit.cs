using System;

namespace KTibiaX.UI.Util {
    public class BaseEdit {

        public DevExpress.XtraEditors.BaseEdit Control { get; set; }

        public bool Enabled {
            get { return Control != null ? Control.Enabled : false; }
            set { if (Control != null) { Control.Enabled = value; } }
        }

        public string Name {
            get { return Control != null ? Control.Name : string.Empty; }
            set { if (Control != null) { Control.Name = value; } }
        }

        public object Value {
            get { return Control != null ? Control.EditValue : null; }
            set { if (Control != null) { Control.EditValue = value; } }
        }

        public override string ToString() {
            return Control != null ? Control.Name : base.ToString();
        }
    }
}
