using System;
using KTibiaX.UI.Controls;
using KTibiaX.UI.Extensions;
using Keyrox.Shared.Objects;
using Tibia.Features.Common;

namespace KTibiaX.Windows.Features.Player {
    public partial class frm_Outfit : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Outfit"/> class.
        /// </summary>
        public frm_Outfit() {
            InitializeComponent();
        }

        #region "[rgn] Form Properties "
        public bool UseCreature {
            get { return ckCreatures.Checked; }
            set { ckCreatures.Checked = value; }
        }
        public bool UsePlayer {
            get { return ckPlayer.Checked; }
            set { ckPlayer.Checked = value; }
        }
        public uint CreatureOutFit {
            get { return ddlCreatures.EditValue.ToUInt32(); }
            set { ddlCreatures.EditValue = value.ToInt32(); }
        }
        public uint PlayerOutFit {
            get { return ddlPlayer.EditValue.ToUInt32(); }
            set { ddlPlayer.EditValue = value.ToInt32(); }
        }
        public bool NoAddon {
            get { return ckNoAddon.Checked; }
            set { ckNoAddon.Checked = value; }
        }
        public bool FirstAddon {
            get { return ckFirstAddon.Checked; }
            set { ckFirstAddon.Checked = value; }
        }
        public bool BothAddon {
            get { return ckBothAddon.Checked; }
            set { ckBothAddon.Checked = value; }
        }
        #endregion

        #region "[rgn] Form Events     "
        private void ckCreatures_CheckedChanged(object sender, EventArgs e) {
            ddlCreatures.Enabled = ckCreatures.Checked;
            ddlPlayer.Enabled = !ckCreatures.Checked;
        }
        private void ckPlayer_CheckedChanged(object sender, EventArgs e) {
            ddlCreatures.Enabled = !ckPlayer.Checked;
            ddlPlayer.Enabled = ckPlayer.Checked;
        }
        #endregion

        /// <summary>
        /// Called when [action execute].
        /// </summary>
        public override void OnActionExecute() {
            if (UsePlayer && Player.Outfit != (OutfitKind)PlayerOutFit) {
                Player.Outfit = (OutfitKind)PlayerOutFit;
            }
            else if (UseCreature && Player.Outfit != (OutfitKind)CreatureOutFit) {
                Player.Outfit = (OutfitKind)CreatureOutFit;
            }
            uint addon = 0; if (FirstAddon) { addon = 1; } else if (BothAddon) { addon = 3; }
            if (Player.Addon != addon) { Player.Addon = addon; }
        }

    }
}