using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.YandexDisk
{
    public class YaDiskApi : ICloudApi
    {
        public ICloudDriveObject Drive { get; }
        public IResourceObject Resources { get; }

        public YaDiskApi()
        {
            Drive = new YaDiskCloudDrive();
            Resources = new YaDiskResources();
        }
    }
}