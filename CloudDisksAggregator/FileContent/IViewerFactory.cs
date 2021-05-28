using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDisksAggregator.FileContent.FileViewers;

namespace CloudDisksAggregator.FileContent
{
    public interface IViewerFactory
    {
        FileViewer Create(string fileName, byte[] bytes);
    }
}
