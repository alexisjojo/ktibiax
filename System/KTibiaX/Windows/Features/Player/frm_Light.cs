using System;
using KTibiaX.UI.Controls;
using KTibiaX.UI.Extensions;
using Keyrox.Shared.Objects;

namespace KTibiaX.Windows.Features.Player {
    public partial class frm_Light : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Light"/> class.
        /// </summary>
        public frm_Light() { 
            InitializeComponent();
        }

        #region "[rgn] Form Properties "
        public uint Intensity {
            get { return ddlItensity.GetSelectedValue().ToUInt32(); }
            set { ddlItensity.SelectItem(value); }
        }
        public uint Color {
            get { return ddlColor.GetSelectedValue().ToUInt32(); }
            set { ddlColor.SelectItem(value); }
        }
        #endregion

        /// <summary>
        /// Called when [action execute].
        /// </summary>
        public override void OnActionExecute() {
            if (Player.Light.Color != Color || Player.Light.Intensity != Intensity) {
                Player.Light = new Tibia.Features.Structures.Light(Intensity, Color);
            }
        }
     }
}