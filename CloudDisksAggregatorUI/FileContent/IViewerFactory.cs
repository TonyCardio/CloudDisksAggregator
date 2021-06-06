using CloudDisksAggregatorUI.FileContent.FileViewers;

namespace CloudDisksAggregatorUI.FileContent
{
    public interface IViewerFactory
    {
        FileViewer Create(string fileName, byte[] bytes);
    }
}
