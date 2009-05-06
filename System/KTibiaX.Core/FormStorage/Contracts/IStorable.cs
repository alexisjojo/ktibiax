using System;

namespace KTibiaX.Core.FormStorage.Contracts {
    public interface IStorable {

        /// <summary>
        /// Gets the name of the feature.
        /// </summary>
        /// <value>The name of the feature.</value>
        string FeatureName { get; }

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        /// <value>The player ID.</value>
        long PlayerID { get; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        /// <value>The name of the player.</value>
        string PlayerName { get; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        string ServerName { get; }

        /// <summary>
        /// Gets or sets the storage manager.
        /// </summary>
        /// <value>The storage manager.</value>
        StorageManager StorageManager { get; }
    }
}
