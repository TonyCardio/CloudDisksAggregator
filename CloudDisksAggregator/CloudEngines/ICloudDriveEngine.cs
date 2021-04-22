using System.Collections.Generic;
using System.Threading.Tasks;
using CloudDisksAggregator.CloudDrives;

namespace CloudDisksAggregator.CloudEngines
{
    public interface ICloudDriveEngine
    {
        public string UserAccessToken { get; }
        public Task Upload(string pathToEntity, string pathToCatalogForSave = "/");
        public Task Download(string pathToEntity, string pathToCatalogForSave);
        public Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog);
        public Task<List<DriveEntityInfo>> GetCatalogContent();
    }
}