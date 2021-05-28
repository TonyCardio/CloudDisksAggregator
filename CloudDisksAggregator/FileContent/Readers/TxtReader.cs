using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisksAggregator.FileContent.Readers
{
    public class TxtReader : IContentReader<string>
    {
        public string FromBytes(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
    }
}
