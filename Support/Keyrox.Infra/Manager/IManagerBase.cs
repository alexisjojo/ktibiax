using Keyrox.Infra.Repository;
using Keyrox.Infra.Service;

namespace Keyrox.Infra.Manager {
    public interface IManagerBase<R, S> : IManagerBase<R>
        where R : IRepositoryBase
        where S : IServiceBase {

        S Service { get; }
    }

    public interface IManagerBase<R>
        where R : IRepositoryBase {

        R Repository { get; }
    }
}
