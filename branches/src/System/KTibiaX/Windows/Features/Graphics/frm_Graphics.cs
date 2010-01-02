using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace KTibiaX.Windows.Features.Graphics {
    public partial class frm_Graphics : DevExpress.XtraEditors.XtraForm {
        public frm_Graphics() {
            InitializeComponent();
        }

        public Tibia.Client.TibiaClient TibiaClient { get; set; }

        public Tibia.Features.Model.Player Player { get { return TibiaClient.Features.Player; } }

        private void frm_Graphics_Load(object sender, EventArgs e) {

        }

        private void btnLoad_Click(object sender, EventArgs e) {

            var sqms = TibiaClient.GraphicProvider.GetSQMs(5);
            lblLocation.Text = TibiaClient.Features.Player.Location.ToString();

            for (int i = 0; i < sqms.Count; i++) {
                var dx = (Convert.ToInt32(Player.Location.X) - Convert.ToInt32(sqms[i].WorldLocation.X));
                var dy = (Convert.ToInt32(Player.Location.Y) - Convert.ToInt32(sqms[i].WorldLocation.Y));

                var sx = dx < 0 ? dx.ToString() : string.Format("+{0}", dx);
                var sy = dy < 0 ? dy.ToString() : string.Format("+{0}", dy);

                var label = GetLabel("labelcontrol" + (i + 1).ToString());
                label.Text = sx + sy;
                FormatLabel(sqms[i], label);                
            }
        }

        private void FormatLabel(Tibia.Features.Model.Graphics.Sqm sqm, LabelControl label) {
            if (!sqm.Walkeable()) {
                label.Appearance.BackColor = Color.Silver;
                label.Appearance.BorderColor = Color.Gray;
                label.Appearance.ForeColor = Color.WhiteSmoke;                
            }
            else if (sqm.GoUP()) {
                label.Appearance.BackColor = Color.LightGreen;
                label.Appearance.BorderColor = Color.LimeGreen;
                label.Appearance.ForeColor = Color.ForestGreen;
            }
            else if (sqm.GoDown()) {
                label.Appearance.BackColor = Color.DarkSeaGreen;
                label.Appearance.BorderColor = Color.DarkOliveGreen;
                label.Appearance.ForeColor = Color.DarkGreen;
            }
            else if (sqm.HasPlayer()) {
                label.Appearance.BackColor = Color.PaleTurquoise;
                label.Appearance.BorderColor = Color.LightSeaGreen;
                label.Appearance.ForeColor = Color.DarkSlateGray;
            }
            else {
                label.Appearance.BackColor = Color.White;
                label.Appearance.BorderColor = Color.Black;
                label.Appearance.ForeColor = Color.LightGray;
            }            
        }

        private LabelControl GetLabel(string name) {
            return groupControl1.Controls.Find(name, true).First() as LabelControl;
        }

    }
}