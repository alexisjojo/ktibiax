using System;
using Keyrox.Infra.Repository;
using Keyrox.Infra.Service;

namespace Keyrox.Infra.Manager {
    public class ManagerBase<R, S> : ManagerBase<R>, IManagerBase<R, S>
        where R : IRepositoryBase
        where S : IServiceBase {

        public S Service {
            get {
                Domain.ThrowNotInitializedContext();
                if (!Domain.ComponentExists<S>()) throw new Exception(string.Format(Properties.Resources.ServiceNotFoundMessage, typeof(S).FullName));
                return Domain.GetComponent<S>();

            }
        }
    }

    public class ManagerBase<R> : IManagerBase<R>
    where R : IRepositoryBase {
        public R Repository {
            get {
                Domain.ThrowNotInitializedContext();
                if (!Domain.ComponentExists<R>()) throw new Exception(string.Format(Properties.Resources.RepositoryNotFoundMessage, typeof(R).FullName));
                return Domain.GetComponent<R>();

            }
        }
    }
}
