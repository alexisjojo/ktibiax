using System;
using Keyrox.Infra.Manager;
using KTibiaX.Core.Repositories.Contracts;

namespace KTibiaX.Core {
    public static class Data {

        public static ManagerBase<IFeatureDataRepository> FeatureData {
            get { return new ManagerBase<IFeatureDataRepository>(); }
        }

        public static ManagerBase<IPlayerFeatureRepository> PlayerFeature {
            get { return new ManagerBase<IPlayerFeatureRepository>(); }
        }

        public static ManagerBase<IItemDataRepository> ItemData {
            get { return new ManagerBase<IItemDataRepository>(); }
        }

    }
}
