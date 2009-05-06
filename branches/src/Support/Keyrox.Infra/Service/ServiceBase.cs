using System;
using Keyrox.Infra.Manager;
using Keyrox.Infra.Repository;
using DevExpress.Xpo;

namespace Keyrox.Infra.Service {
    public class ServiceBase : IServiceBase {

    }
    public class ServiceBase<R> : IServiceBase<R> where R : IRepositoryBase {

        #region IServiceBase<R> Members
        public R Repository { get { return Domain.GetComponent<R>(); } }
        #endregion
    }
}
