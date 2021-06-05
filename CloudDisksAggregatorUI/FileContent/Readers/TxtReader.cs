using System.Text;

namespace CloudDisksAggregatorUI.FileContent.Readers
{
    public class TxtReader : IContentReader<string>
    {
        public string FromBytes(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
    }
}
