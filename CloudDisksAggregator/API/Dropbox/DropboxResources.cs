using System.Drawing;
using CloudDisksAggregator.API.Dropbox.Resources;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.Dropbox
{
    internal class DropboxResources : IResourceObject
    {
        public Bitmap MainLogo => DropboxRes.dropboxMainLogo;
        public string DriveTypeName => "Dropbox";
    }
}