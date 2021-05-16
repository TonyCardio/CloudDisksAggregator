using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.Dropbox
{
    public class DropboxApi : ICloudApi
    {
        public ICloudDriveObject Drive { get; }
        public IResourceObject Resources { get; }

        public DropboxApi()
        {
            Drive = new DropboxCloudDrive();
            Resources = new DropboxResources();
        }
    }
}