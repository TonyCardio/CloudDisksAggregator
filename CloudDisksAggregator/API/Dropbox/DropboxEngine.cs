using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudDisksAggregator.Core;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;

namespace CloudDisksAggregator.API.Dropbox
{
    public class DropboxEngine : ICloudDriveEngine
    {
        private readonly DropboxClient diskApi;
        public string UserAccessToken { get; }

        public DropboxEngine(string userAccessToken)
        {
            UserAccessToken = userAccessToken;
            diskApi = new DropboxClient(userAccessToken);
        }

        public async Task Upload(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DriveEntityInfo(pathToEntity);
            var data = ReadEntity(entity.FullPath);
            await diskApi.Files.UploadAsync(
                $"{pathToCatalogForSave}/{entity.Name}",
                WriteMode.Overwrite.Instance,
                body: data);
        }

        private static MemoryStream ReadEntity(string pathToEntity)
            => new MemoryStream(File.ReadAllBytes(pathToEntity));

        public async Task Download(string pathToEntity, string pathToCatalogForSave)
        {
            var entity = new DriveEntityInfo(pathToEntity);
            var response = await diskApi.Files.DownloadAsync(entity.FullPath);
            await Save(response, $@"{pathToCatalogForSave}/{entity.Name}");
        }

        private static async Task Save(IDownloadResponse<FileMetadata> response, string pathForSave)
        {
            var data = await response.GetContentAsByteArrayAsync();
            await File.WriteAllBytesAsync(pathForSave, data);
        }

        public async Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog)
        {
            return CatalogContentsMapper.MapDropboxCatalogContent(
                (await diskApi.Files.ListFolderAsync(pathToCatalog)).Entries);
        }

        public Task<List<DriveEntityInfo>> GetCatalogContent() => GetCatalogContent("");
    }
}