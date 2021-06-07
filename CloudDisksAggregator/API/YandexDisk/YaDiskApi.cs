using CloudDisksAggregator.Core;
using CloudDisksAggregatorInfrastructure.InMemoryJsonStorage;

namespace CloudDisksAggregator.API.YandexDisk
{
    internal class YaDiskApi : ICloudApi
    {
        public ICloudDriveObject Drive { get; }
        public IResourceObject Resources { get; }

        public YaDiskApi(IInMemoryJsonStorage<UserAccount> storage)
        {
            Drive = new YaDiskCloudDrive(storage);
            Resources = new YaDiskResources();
        }
    }
}