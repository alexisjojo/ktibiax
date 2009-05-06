using DevExpress.Xpo;
using Keyrox.Infra.Transaction;
using System;

namespace KTibiaX.Core.Model {
    [Persistent("FeatureData"), OptimisticLocking(false)]
    public class FeatureData : XPBaseObject {

        #region "[rgn] Object Constructors "
        public FeatureData(Session session) : base(session) { }
        public FeatureData() : base(TransactionScope.DefaultUnitOfWork) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #endregion

        private int id;
        [Persistent("Code"), Key(true)]
        public int Id {
            get { return id; }
            set { SetPropertyValue<int>("Id", ref id, value); }
        }

        private PlayerFeature feature;
        [Persistent("Feature"), Association("Player-Feature-Data")]
        public PlayerFeature Feature {
            get { return feature; }
            set { SetPropertyValue<PlayerFeature>("Feature", ref feature, value); }
        }

        private string name;
        [Persistent("Name")]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }

        private string fieldValue;
        [Persistent("Value")]
        public string Value {
            get { return fieldValue; }
            set { SetPropertyValue<string>("Value", ref fieldValue, value); }
        }

        private string type;
        [Persistent("Type")]
        public string Type {
            get { return type; }
            set { SetPropertyValue<string>("Type", ref type, value); }
        }

        private bool serialized;
        [Persistent("Serialized")]
        public bool Serialized {
            get { return serialized; }
            set { SetPropertyValue<bool>("Serialized", ref serialized, value); }
        }

        private DateTime lastChange;
        [Persistent("LastChange")]
        public DateTime LastChange {
            get { return lastChange; }
            set { SetPropertyValue<DateTime>("LastChange", ref lastChange, value); }
        }

    }
}
