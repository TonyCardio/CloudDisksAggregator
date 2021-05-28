using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDisksAggregator.FileContent.Readers
{
    public interface IContentReader<TContent>
    {
        TContent FromBytes(byte[] bytes);
    }
}
