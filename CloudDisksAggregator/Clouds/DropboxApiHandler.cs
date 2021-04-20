using System.IO;
using System.Threading.Tasks;
using CloudDisksAggregator.Data;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;

namespace CloudDisksAggregator.Clouds
{
    public class DropboxApiHandler : CloudDriveHelper, ICloudDrive
    {
        private DropboxClient DiskApi { get; set; }

        public void SetToken(string token) => DiskApi = new DropboxClient(token);

        public async Task Upload(string pathToEntity, string pathToCatalogForSave = "")
        {
            ThrowIfTokenNotSet(DiskApi.Equals(null));
            var entity = GetEntityInfo(pathToEntity);
            var data = ReadEntity(entity.FullPath);
            await DiskApi.Files.UploadAsync(
                $"{pathToCatalogForSave}/{entity.Name}",
                WriteMode.Overwrite.Instance, 
                body: data);
        }

        private static MemoryStream ReadEntity(string pathToEntity)
            => new(File.ReadAllBytes(pathToEntity));

        public async Task Download(string pathToEntity, string pathToCatalogForSave)
        {
            ThrowIfTokenNotSet(DiskApi.Equals(null));
            var entity = GetEntityInfo(pathToEntity);
            var response = await DiskApi.Files.DownloadAsync(entity.FullPath);
            await Save(response, $@"{pathToCatalogForSave}/{entity.Name}");
        }

        private static async Task Save(IDownloadResponse<FileMetadata> response, string pathForSave)
        {
            var data = await response.GetContentAsByteArrayAsync();
            await File.WriteAllBytesAsync(pathForSave, data);
        }

        public async Task<object> GetCatalogContents(string pathToCatalog = "")
        {
            ThrowIfTokenNotSet(DiskApi.Equals(null));
            return CatalogContentsMapper.DropboxCatalogContentsMapper(
                (await DiskApi.Files.ListFolderAsync(pathToCatalog)).Entries);
        }
    }
}