using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudDisksAggregator.Core;
using Dropbox.Api;
using Dropbox.Api.Files;
using Newtonsoft.Json;

namespace CloudDisksAggregator.API.Dropbox
{
    internal class DropboxEngine : ICloudDriveEngine
    {
        private readonly DropboxClient diskApi;
        [JsonProperty] private readonly string userAccessToken;

        public DropboxEngine(string userAccessToken)
        {
            this.userAccessToken = userAccessToken;
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
            if (entity.IsDirectory)
                return await (await diskApi.Files.DownloadZipAsync(entity.FullPath)).GetContentAsByteArrayAsync();
            return await (await diskApi.Files.DownloadAsync(entity.FullPath)).GetContentAsByteArrayAsync();
        }
        
        public async Task Save(string pathToEntity, string pathToCatalogForSave)
        {
            var data = await Download(pathToEntity);
            var entity = new DriveEntityInfo(pathToEntity, this);
            var path = entity.IsDirectory 
                ? $"{pathToCatalogForSave}/{entity.Name}.zip" 
                : $"{pathToCatalogForSave}/{entity.Name}";
            await File.WriteAllBytesAsync(path, data);
        }

        public async Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog)
        {
            if (pathToCatalog == "/") pathToCatalog = "";
            return CatalogContentsMapper.MapDropboxCatalogContent(
                (await diskApi.Files.ListFolderAsync(pathToCatalog)).Entries, this);
        }
        
        public async Task Delete(string pathToEntity) => await diskApi.Files.DeleteV2Async(new DeleteArg(pathToEntity));
        
        private async Task MoveEntity(string pathToEntity, string whereToMove, string newName = "")
        {
            var entity = new DriveEntityInfo(pathToEntity, this);
            var toPath = newName.Equals("") ? $"{whereToMove}/{entity.Name}" : $"{whereToMove}/{newName}";
            try
            {
                await diskApi.Files.MoveV2Async(pathToEntity, toPath);
            }
            catch (ApiException<RelocationError> e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task RenameEntity(string path, string newName)
        {
            path = Equals(path[0], '/') ? path : "/" + path;
            var entity = new DriveEntityInfo(path, this);
            await MoveEntity(path, entity.Parent, newName);
        }
    }
}