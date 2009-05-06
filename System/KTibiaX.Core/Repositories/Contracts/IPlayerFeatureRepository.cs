using Keyrox.Infra.Repository;
using KTibiaX.Core.Model;
using System.Collections.Generic;

namespace KTibiaX.Core.Repositories.Contracts {
    public interface IPlayerFeatureRepository : IRepositoryBase<PlayerFeature> {
        IList<PlayerFeature> LoadAll(long playerId, string featureName);
        IList<PlayerFeature> LoadAll(string playerName, string serverName, string featureName);
    }
}