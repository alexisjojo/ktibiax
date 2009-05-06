using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using Tibia.Client;
using Tibia.Features.Model;

namespace KTibiaX.UI.Controls {
    public class BaseFeatureControl : XtraUserControl {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeatureControl"/> class.
        /// </summary>
        public BaseFeatureControl() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeatureControl"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public BaseFeatureControl(TibiaClient client) {
            TibiClient = client;
        }

        #region "[rgn] Properties "
        public TibiaClient TibiClient { get; set; }
        public Player Player { get; set; }
        #endregion
    }
}
