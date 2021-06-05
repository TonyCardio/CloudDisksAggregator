using System.Drawing;
using System.IO;

namespace CloudDisksAggregatorUI.FileContent.Readers
{
    public class ImageReader : IContentReader<Image>
    {
        public Image FromBytes(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            return Image.FromStream(stream);
        }
    }
}
