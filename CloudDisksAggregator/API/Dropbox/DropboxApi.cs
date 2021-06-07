using CloudDisksAggregator.Core;
using CloudDisksAggregatorInfrastructure.InMemoryJsonStorage;

namespace CloudDisksAggregator.API.Dropbox
{
    internal class DropboxApi : ICloudApi
    {
        public ICloudDriveObject Drive { get; }
        public IResourceObject Resources { get; }

        public DropboxApi(IInMemoryJsonStorage<UserAccount> storage)
        {
            Drive = new DropboxCloudDrive(storage);
            Resources = new DropboxResources();
        }
    }
}