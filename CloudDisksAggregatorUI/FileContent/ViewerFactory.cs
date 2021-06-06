using CloudDisksAggregatorUI.FileContent.FileViewers;
using System.Linq;

namespace CloudDisksAggregatorUI.FileContent
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
