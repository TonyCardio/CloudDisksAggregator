using System.Drawing;

namespace CloudDisksAggregator.Core
{
    public interface IResourceObject
    {
        Bitmap MainLogo { get; }
        public string DriveTypeName { get; }
    }
}