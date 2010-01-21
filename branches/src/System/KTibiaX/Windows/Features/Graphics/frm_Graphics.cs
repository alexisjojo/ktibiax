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

        public Dictionary<string, Tibia.Features.Model.Graphics.Sqm> CurrentSQMs { get; set; }

        public Tibia.Features.Structures.Location CurrentLocation { get; set; }

        private void frm_Graphics_Load(object sender, EventArgs e) {
            for (int i = 1; i <= 25; i++) {
                var label = GetLabel("labelcontrol" + i.ToString());
                label.Click += new EventHandler(label_Click);
            }
        }

        private void label_Click(object sender, EventArgs e) {
            //Restore the label formatting.
            for (int i = 1; i <= 25; i++) {
                var label = GetLabel("labelcontrol" + i.ToString());
                FormatLabel((Tibia.Features.Model.Graphics.Sqm)label.Tag, label);
            }
            //Get the selected title information.
            var sqm = CurrentSQMs[((LabelControl)sender).Name.ToLower()];
            ((LabelControl)sender).Appearance.BorderColor = Color.Red;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var title in sqm.Tiles) {
                listBox1.Items.Add(title.Data.ToString());
                listBox2.Items.Add(title.TileId.ToString());
            }
        }

        private void btnLoad_Click(object sender, EventArgs e) {

            CurrentSQMs = new Dictionary<string, Tibia.Features.Model.Graphics.Sqm>();
            TibiaClient.GraphicProvider.GetAllSqms();
            var sqms = TibiaClient.GraphicProvider.GetSQMs(5);
            CurrentLocation = TibiaClient.Features.Player.Location;
            lblLocation.Text = CurrentLocation.ToString();

            for (int i = 0; i < sqms.Count; i++) {
                var dx = (Convert.ToInt32(Player.Location.X) - Convert.ToInt32(sqms[i].WorldLocation.X));
                var dy = (Convert.ToInt32(Player.Location.Y) - Convert.ToInt32(sqms[i].WorldLocation.Y));

                var sx = dx < 0 ? dx.ToString() : string.Format("+{0}", dx);
                var sy = dy < 0 ? dy.ToString() : string.Format("+{0}", dy);

                var label = GetLabel("labelcontrol" + (i + 1).ToString());
                label.Text = sx + sy;
                label.Tag = sqms[i];
                label.ToolTip = sqms[i].WorldLocation.ToString();
                FormatLabel(sqms[i], label);
                CurrentSQMs.Add("labelcontrol" + (i + 1).ToString(), sqms[i]);
            }
        }

        private void FormatLabel(Tibia.Features.Model.Graphics.Sqm sqm, LabelControl label) {
            if (!sqm.Walkeable()) {
                label.Appearance.BackColor = Color.Silver;
                label.Appearance.BorderColor = Color.Gray;
                label.Appearance.ForeColor = Color.WhiteSmoke;
                //label.Cursor = Cursors.Help;
            }
            else if (sqm.GoUP()) {
                label.Appearance.BackColor = Color.LightGreen;
                label.Appearance.BorderColor = Color.LimeGreen;
                label.Appearance.ForeColor = Color.ForestGreen;
                //label.Cursor = Cursors.PanNorth;
            }
            else if (sqm.GoDown()) {
                label.Appearance.BackColor = Color.DarkSeaGreen;
                label.Appearance.BorderColor = Color.DarkOliveGreen;
                label.Appearance.ForeColor = Color.DarkGreen;
                //label.Cursor = Cursors.PanSouth;
            }
            else if (sqm.HasPlayer()) {
                label.Appearance.BackColor = Color.PaleTurquoise;
                label.Appearance.BorderColor = Color.LightSeaGreen;
                label.Appearance.ForeColor = Color.DarkSlateGray;
                //label.Cursor = Cursors.Hand;
            }
            else {
                label.Appearance.BackColor = Color.White;
                label.Appearance.BorderColor = Color.Black;
                label.Appearance.ForeColor = Color.LightGray;
                //label.Cursor = Cursors.No;
            }
        }

        private LabelControl GetLabel(string name) {
            return groupControl1.Controls.Find(name, true).First() as LabelControl;
        }

    }
}