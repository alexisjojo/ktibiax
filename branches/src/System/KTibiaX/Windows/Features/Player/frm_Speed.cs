using System;
using KTibiaX.UI.Controls;

namespace KTibiaX.Windows.Features.Player {
    public partial class frm_Speed : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Speed"/> class.
        /// </summary>
        public frm_Speed() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Speed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Speed_Load(object sender, EventArgs e) {
            txtOriginal.Text = Player.WalkSpeed.ToString();
            txtActual.Text = Player.WalkSpeed.ToString();
            OriginalSpeed = Player.WalkSpeed;
        }
                
        #region "[rgn] Form Properties "
        public uint OriginalSpeed { get; set; }
        public uint UPTO {
            get {
                if (ck10.Checked) { return 10; }
                if (ck20.Checked) { return 40; }
                if (ck30.Checked) { return 70; }
                return 0;
            }
        }
        #endregion
        
        /// <summary>
        /// Called when [action execute].
        /// </summary>
        public override void OnActionExecute() {
            uint newSpeed = ((OriginalSpeed * UPTO) / 100) + OriginalSpeed;
            if (Player.WalkSpeed != newSpeed) {
                Player.WalkSpeed = newSpeed;
            }
        }


    }
}