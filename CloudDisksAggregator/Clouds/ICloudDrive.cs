using System.Threading.Tasks;

namespace CloudDisksAggregator.Clouds
{
    public interface ICloudDrive
    {
        public void SetToken(string token);
        public Task Upload(string pathToEntity, string pathToCatalogForSave = "/");
        public Task Download(string pathToEntity, string pathToCatalogForSave);
        public Task<object> GetCatalogContents(string pathToCatalog);
    }
}