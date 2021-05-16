using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.YandexDisk
{
    public class YaDiskApi : ICloudApi
    {
        public ICloudDriveObject Drive => new YaDiskCloudDrive();
        public IResourceObject Resources => new YaDiskResources();
    }
}