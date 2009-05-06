using DevExpress.Data.Filtering;
using Keyrox.Infra.Repository;
using KTibiaX.Core.Model;
using KTibiaX.Core.Repositories.Contracts;
using System.Collections.Generic;

namespace KTibiaX.Core.Repositories {
    public class PlayerFeatureRepository : RepositoryBase<PlayerFeature>, IPlayerFeatureRepository {
        
        /// <summary>
        /// Load all player features.
        /// </summary>
        /// <param name="playerId">The player id.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <returns></returns>
        public IList<PlayerFeature> LoadAll(long playerId, string featureName) {
            return LoadAll(
                new GroupOperator(
                    new BinaryOperator("PlayerId", playerId),
                    new BinaryOperator("FeatureName", featureName)
                )
            );
        }

        /// <summary>
        /// Loads all.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <returns></returns>
        public IList<PlayerFeature> LoadAll(string playerName, string serverName, string featureName) {
            return LoadAll(
                new GroupOperator(
                    new BinaryOperator("PlayerName", playerName),
                    new BinaryOperator("ServerName", serverName),
                    new BinaryOperator("FeatureName", featureName)
                )
            );
        }


        

    }
}