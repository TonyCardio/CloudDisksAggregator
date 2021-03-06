using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDisksAggregator.Core
{
    public interface ICloudDriveEngine
    {
        public Task Upload(string pathToEntity, string pathToCatalogForSave = "/");
        public Task<byte[]> Download(string pathToEntity);
        public Task Save(string pathToEntity, string pathToCatalogForSave);
        public Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog);
        public Task Delete(string pathToEntity);
        public Task RenameEntity(string path, string newName);
        public Task<List<DriveEntityInfo>> Search(string searchLine, string directory);
    }
}