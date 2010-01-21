using DevExpress.Xpo;
using Keyrox.Infra.Transaction;
using System;

namespace KTibiaX.Core.Model {
    [Persistent("tbl_item_data"), OptimisticLocking(false)]
    public class ItemData : XPBaseObject {
              
        #region "[rgn] Object Constructors "
        public ItemData(Session session) : base(session) { }
        public ItemData() : base(TransactionScope.DefaultUnitOfWork) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #endregion

        private int id;
        [Persistent("id"), Key(true)]
        public int Id {
            get { return id; }
            set { SetPropertyValue<int>("Id", ref id, value); }
        }

        private string nome;
        [Persistent("nome"), Key(true)]
        public string Nome {
            get { return nome; }
            set { SetPropertyValue<string>("Nome", ref nome, value); }
        }
    }
}
