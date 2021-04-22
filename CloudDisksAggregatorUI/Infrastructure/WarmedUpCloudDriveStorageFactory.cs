using System.Threading.Tasks;
using CloudDisksAggregator.CloudEngines;
using CloudDisksAggregatorInfrastructure.InMemoryStorage;
using CloudDisksAggregatorUI.UI.ViewEntity;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public class WarmedUpCloudDriveStorageFactory : IInMemoryStorageFactory<DriveViewInfo, ICloudDriveEngine>
    {
        private readonly IAppSettings settings;

        public WarmedUpCloudDriveStorageFactory(IAppSettings settings)
        {
            this.settings = settings;
        }

        public IInMemoryStorage<DriveViewInfo, ICloudDriveEngine> Create()
        {
            var storage = new SimpleInMemoryStorage<DriveViewInfo, ICloudDriveEngine>(settings.PathToObjectsStorage);
            storage.WarmUpCache();
            return storage;
        }
    }
}