using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CloudDisksAggregator.CloudDrives;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace CloudDisksAggregator.CloudEngines
{
    public class YandexDiskEngine : ICloudDriveEngine
    {
        private readonly IDiskApi diskApi;
        public string UserAccessToken { get; }

        public YandexDiskEngine(string userAccessToken)
        {
            UserAccessToken = userAccessToken;
            diskApi = new DiskHttpApi(userAccessToken);
        }

        public async Task Upload(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DriveEntityInfo(pathToEntity);
            var pathForSave = $"{pathToCatalogForSave}/{entity.Name}";
            await diskApi.Files.UploadFileAsync(
                pathForSave,
                false,
                pathToEntity,
                CancellationToken.None);
        }

        public async Task Download(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DriveEntityInfo(pathToEntity);
            if (pathToCatalogForSave.Equals(""))
                pathToCatalogForSave = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            await diskApi.Files.DownloadFileAsync(path: entity.FullPath,
                Path.Combine(pathToCatalogForSave, entity.Name));
        }

        public async Task<List<DriveEntityInfo>> GetCatalogContent(string pathToCatalog)
        {
            return CatalogContentsMapper.MapYandexCatalogContent(
                (await diskApi.MetaInfo.GetInfoAsync(
                    new ResourceRequest {Path = pathToCatalog},
                    CancellationToken.None)).Embedded.Items);
        }

        public Task<List<DriveEntityInfo>> GetCatalogContent() => GetCatalogContent("/");
    }
}