using System.Threading.Tasks;
using CloudDisksAggregator.Clouds;
using CloudDisksAggregatorInfrastructure.InMemoryStorage;
using CloudDisksAggregatorUI.UI.ViewEntity;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public class WarmedUpCloudDriveStorageFactory : IInMemoryStorageFactory<DriveViewInfo, ICloudDrive>
    {
        private readonly IAppSettings settings;

        public WarmedUpCloudDriveStorageFactory(IAppSettings settings)
        {
            this.settings = settings;
        }

        public IInMemoryStorage<DriveViewInfo, ICloudDrive> Create()
        {
            var storage = new SimpleInMemoryStorage<DriveViewInfo, ICloudDrive>(settings.PathToObjectsStorage);
            storage.WarmUpCache();
            return storage;
        }
    }
}