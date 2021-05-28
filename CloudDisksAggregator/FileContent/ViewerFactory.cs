using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDisksAggregator.FileContent.FileViewers;

namespace CloudDisksAggregator.FileContent
{
    public class ViewerFactory : IViewerFactory
    {
        private readonly FileViewer[] viewers;

        public ViewerFactory(FileViewer[] viewers)
        {
            this.viewers = viewers;
        }

        public FileViewer Create(string fileName, byte[] bytes)
        {
            var viewer = viewers.FirstOrDefault(v => v.CanView(fileName));
            viewer?.InitializeContent(fileName, bytes);
            return viewer;
        }
    }
}
