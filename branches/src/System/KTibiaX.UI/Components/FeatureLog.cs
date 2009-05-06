using System;
using System.ComponentModel;

namespace KTibiaX.UI.Components {
    public class FeatureLog : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureLog"/> class.
        /// </summary>
        public FeatureLog() {
        }

        #region "[rgn] Singleton "
        private static FeatureLog current;
        public static FeatureLog Current {
            get {
                if (current == null) { current = new FeatureLog(); }
                return current;
            }
        }
        #endregion

        /// <summary>
        /// Writes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public void Write(string name, string description) {
            return;
        }

    }
}
