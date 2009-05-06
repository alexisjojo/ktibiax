using Keyrox.Infra.Repository;
using KTibiaX.Core.Model;
using System.Collections.Generic;

namespace KTibiaX.Core.Repositories.Contracts {
    public interface IFeatureDataRepository : IRepositoryBase<FeatureData> {
        IList<FeatureData> LoadAll(long playerId, string featureName, string name);
        IList<FeatureData> LoadAll(string playerName, string serverName, string featureName, string name);
    }
}