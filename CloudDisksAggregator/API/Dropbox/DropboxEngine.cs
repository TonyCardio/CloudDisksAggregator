using System;
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
            var entity = new DriveEntityInfo(pathToEntity, this);
            var data = ReadEntity(entity.FullPath);
            await diskApi.Files.UploadAsync(
                $"{pathToCatalogForSave}/{entity.Name}",
                WriteMode.Overwrite.Instance,
                body: data);
        }

        private static MemoryStream ReadEntity(string pathToEntity)
            => new MemoryStream(File.ReadAllBytes(pathToEntity));

        public async Task<byte[]> Download(string pathToEntity)
        {
            var entity = new DriveEntityInfo(pathToEntity, this);
            return await (await diskApi.Files.DownloadAsync(entity.FullPath)).GetContentAsByteArrayAsync();
        }

        public async Task Save(string pathToEntity, string pathToCatalogForSave = "")
        {
            var data = await Download(pathToEntity);
            if (pathToCatalogForSave.Equals(""))
                pathToCatalogForSave = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var entity = new DriveEntityInfo(pathToEntity, this);
            var path = $"{pathToCatalogForSave}/{entity.Name}";
            await File.WriteAllBytesAsync(path, data);
        }

        public async Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog)
        {
            if (pathToCatalog == "/") pathToCatalog = "";
            return CatalogContentsMapper.MapDropboxCatalogContent(
                (await diskApi.Files.ListFolderAsync(pathToCatalog)).Entries, this);
        }
    }
}