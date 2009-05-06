using System;

namespace KTibiaX.UI.Util {
    public class BaseControl {

        public System.Windows.Forms.Control Control { get; set; }

        public bool Enabled {
            get { return Control != null ? Control.Enabled : false; }
            set { if (Control != null) { Control.Enabled = value; } }
        }

        public string Name {
            get { return Control != null ? Control.Name : string.Empty; }
            set { if (Control != null) { Control.Name = value; } }
        }

        public string Text {
            get { return Control != null ? Control.Text : null; }
            set { if (Control != null) { Control.Text = value; } }
        }

        public override string ToString() {
            return Control != null ? Control.Name : base.ToString();
        }
    }
}
