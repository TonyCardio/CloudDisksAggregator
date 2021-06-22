using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudDisksAggregator.Core;
using Newtonsoft.Json;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace CloudDisksAggregator.API.YandexDisk
{
    internal class YandexDiskEngine : ICloudDriveEngine
    {
        private readonly IDiskApi diskApi;
        [JsonProperty] private readonly string userAccessToken;

        public YandexDiskEngine(string userAccessToken)
        {
            this.userAccessToken = userAccessToken;
            diskApi = new DiskHttpApi(userAccessToken);
        }

        public async Task Upload(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DriveEntityInfo(pathToEntity, this);
            var pathForSave = $"{pathToCatalogForSave}/{entity.Name}";
            await diskApi.Files.UploadFileAsync(
                pathForSave,
                false,
                pathToEntity,
                CancellationToken.None);
        }

        public async Task<byte[]> Download(string pathToEntity)
        {
            var data = await diskApi.Files.DownloadAsync(await diskApi.Files.GetDownloadLinkAsync(pathToEntity));
            await using var ms = new MemoryStream();
            await data.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task Save(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DriveEntityInfo(pathToEntity, this);
            if (pathToCatalogForSave.Equals(""))
                pathToCatalogForSave = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            await diskApi.Files.DownloadFileAsync(path: entity.FullPath,
                Path.Combine(pathToCatalogForSave, entity.Name));
        }

        public async Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog)
        {
            return CatalogContentsMapper.MapYandexCatalogContent(
                (await diskApi.MetaInfo.GetInfoAsync(
                    new ResourceRequest {Path = pathToCatalog, Limit = 2147483647},
                    CancellationToken.None)).Embedded.Items, this);
        }
        
        private async Task<List<DriveEntityInfo>> GetFlatList(string pathToCatalog="/")
        {
            var entities = new List<DriveEntityInfo>();
            foreach (var entity in await GetCatalogContent(pathToCatalog))
            {
                entities.Add(entity);
                if (entity.Expansion.Equals("Dir"))
                    entities = entities.Concat(await GetFlatList(entity.FullPath)).ToList();
            }
            return entities;
        }
        
        public async Task<List<DriveEntityInfo>> Search(string searchLine, string directory) 
            => EntityFinder.Search(searchLine, await GetFlatList(directory));
    }
}