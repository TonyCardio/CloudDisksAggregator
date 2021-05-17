using System.Drawing;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.YandexDisk
{
    public class YaDiskResources : IResourceObject
    {
        public Bitmap MainLogo => Res.yandexDiskMainLogo;
        public string DriveTypeName => "Yandex Disk";
    }
}