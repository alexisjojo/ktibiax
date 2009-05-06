using System.Linq;
using DevExpress.Xpo;
using Keyrox.Infra.Transaction;
using System.Collections.Generic;
using Keyrox.Shared.Objects;

namespace KTibiaX.Core.Model {
    [Persistent("PlayerFeature"), OptimisticLocking(false)]
    public class PlayerFeature : XPBaseObject {

        #region "[rgn] Object Constructors "
        public PlayerFeature(Session session) : base(session) { }
        public PlayerFeature() : base(TransactionScope.DefaultUnitOfWork) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #endregion

        private int id;
        [Persistent("Code"), Key(true)]
        public int Id {
            get { return id; }
            set { SetPropertyValue<int>("Id", ref id, value); }
        }

        private long playerId;
        [Persistent("PlayerId")]
        public long PlayerId {
            get { return playerId; }
            set { SetPropertyValue<long>("PlayerId", ref playerId, value); }
        }

        private string playerName;
        [Persistent("PlayerName")]
        public string PlayerName {
            get { return playerName; }
            set { SetPropertyValue<string>("PlayerName", ref playerName, value); }
        }

        private string serverName;
        [Persistent("ServerName")]
        public string ServerName {
            get { return serverName; }
            set { SetPropertyValue<string>("ServerName", ref serverName, value); }
        }

        private string featureName;
        [Persistent("FeatureName")]
        public string FeatureName {
            get { return featureName; }
            set { SetPropertyValue<string>("FeatureName", ref featureName, value); }
        }

        [Association("Player-Feature-Data", typeof(FeatureData)), Aggregated]
        public XPCollection XpFeatures { get { return GetCollection("XpFeatures"); } }

        [PersistentAlias("XpFeatures")]
        public XPCollection<FeatureData> Features { get { return XpFeatures.To<XPCollection<FeatureData>>(); } }

    }
}
