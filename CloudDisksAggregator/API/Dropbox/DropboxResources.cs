using System.Drawing;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.Dropbox
{
    public class DropboxResources : IResourceObject
    {
        public Bitmap MainLogo => Res.dropboxMainLogo;
        public string DriveTypeName => "Dropbox";
    }
}