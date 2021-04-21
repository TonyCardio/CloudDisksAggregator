using System.Threading.Tasks;
using CloudDisksAggregator.Clouds;
using CloudDisksAggregatorInfrastructure.InMemoryStorage;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public class WarmedUpCloudDriveStorageFactory : IInMemoryStorageFactory<string, ICloudDrive>
    {
        private readonly IAppSettings settings;

        public WarmedUpCloudDriveStorageFactory(IAppSettings settings)
        {
            this.settings = settings;
        }

        public IInMemoryStorage<string, ICloudDrive> Create()
        {
            var storage = new SimpleInMemoryStorage<string, ICloudDrive>(settings.PathToObjectsStorage);
            storage.WarmUpCache();
            return storage;
        }
    }
}