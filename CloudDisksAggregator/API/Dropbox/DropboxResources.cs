using System.Drawing;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.Dropbox
{
    public class DropboxResources : IResourceObject
    {
        public Bitmap MainLogo => Res.mainLogoStub;
        public string DriveTypeName => "Dropbox";
    }
}