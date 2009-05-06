using System;
using KTibiaX.UI.Controls;
using Tibia.Features.Model;
using Tibia.Features.Model.List;

namespace KTibiaX.Windows.Features.Player {
    public partial class frm_SpyBattleList : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_SpyBattleList"/> class.
        /// </summary>
        public frm_SpyBattleList() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_SpyBattleList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_SpyBattleList_Load(object sender, EventArgs e) {
            LoadPlayers();
        }

        #region "[rgn] Form Events "
        private void btnRefresh_Click(object sender, EventArgs e) {
            LoadPlayers();
        }
        private void lstPlayers_SelectedIndexChanged(object sender, EventArgs e) {
            gdInfo.SelectedObject = lstPlayers.SelectedValue;
        }
        #endregion

        /// <summary>
        /// Loads the players.
        /// </summary>
        private void LoadPlayers() {
            var blist = new BattleList(TibiaClient.Connection, new Tibia.Features.Structures.Range(Tibia.Features.RangeType.Screen, 3));
            var creatures = blist.GetAll();
            lstPlayers.DataSource = creatures;
            lstPlayers.DisplayMember = "Name";
        }

    }
}