using System.Drawing;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.YandexDisk
{
    public class YaDiskResources : IResourceObject
    {
        public Bitmap MainLogo => Res.mainLogoStub;
        public string DriveTypeName => "Yandex Disk";
    }
}