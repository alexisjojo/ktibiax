using Keyrox.Infra.Repository;
using KTibiaX.Core.Model;
using KTibiaX.Core.Repositories.Contracts;
using System.Collections.Generic;
using DevExpress.Data.Filtering;

namespace KTibiaX.Core.Repositories {
    public class FeatureDataRepository : RepositoryBase<FeatureData>, IFeatureDataRepository {

        public IList<FeatureData> LoadAll(long playerId, string featureName, string name) {
            return LoadAll(
                new GroupOperator(
                    new BinaryOperator("Feature.PlayerId", playerId),
                    new BinaryOperator("Feature.FeatureName", featureName),
                    new BinaryOperator("Name", name)
                )
            );
        }

        public IList<FeatureData> LoadAll(string playerName, string serverName, string featureName, string name) {
            return LoadAll(
                new GroupOperator(
                    new BinaryOperator("Feature.PlayerName", playerName),
                    new BinaryOperator("Feature.ServerName", serverName),
                    new BinaryOperator("Feature.FeatureName", featureName),
                    new BinaryOperator("Name", name)
                )
            );
        }

    }
}