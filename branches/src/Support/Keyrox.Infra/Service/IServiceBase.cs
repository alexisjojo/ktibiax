using System;
using Keyrox.Infra.Repository;
using DevExpress.Xpo;

namespace Keyrox.Infra.Service {
    public interface IServiceBase {
    }
    public interface IServiceBase<R> : IServiceBase where R : IRepositoryBase {
        R Repository { get; }
    }
}
