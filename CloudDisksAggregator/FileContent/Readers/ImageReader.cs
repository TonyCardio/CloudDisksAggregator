using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisksAggregator.FileContent.Readers
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
