using Apitron.PDF.Rasterizer;
using System.IO;

namespace CloudDisksAggregatorUI.FileContent.Readers
{
    public class PdfReader : IContentReader<Document>
    {
        public Document FromBytes(byte[] bytes)
        {
            return new(new MemoryStream(bytes));
        }
    }
}
