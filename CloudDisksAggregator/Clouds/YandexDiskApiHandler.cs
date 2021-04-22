using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CloudDisksAggregator.Data;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace CloudDisksAggregator.Clouds
{
    public class YandexDiskApiHandler: ICloudDrive
    {
        private IDiskApi DiskApi { get; }
        public string UserAccessToken { get; }

        public YandexDiskApiHandler(string userAccessToken)
        {
            UserAccessToken = userAccessToken;
            DiskApi = new DiskHttpApi(userAccessToken);
        }

        public async Task Upload(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DiskEntityInfo(pathToEntity);
            var pathForSave = $"{pathToCatalogForSave}/{entity.Name}";
            await DiskApi.Files.UploadFileAsync(
                pathForSave, 
                false, 
                pathToEntity, 
                CancellationToken.None);
        }

        public async Task Download(string pathToEntity, string pathToCatalogForSave = "")
        {
            var entity = new DiskEntityInfo(pathToEntity);
            if (pathToCatalogForSave.Equals(""))
                pathToCatalogForSave = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            await DiskApi.Files.DownloadFileAsync(path: entity.FullPath,
                Path.Combine(pathToCatalogForSave, entity.Name));
        }

        public async Task<List<DiskEntityInfo>> GetCatalogContents(string pathToCatalog)
        {
            return CatalogContentsMapper.YandexCatalogContentsMapper(
                (await DiskApi.MetaInfo.GetInfoAsync(
                    new ResourceRequest { Path = pathToCatalog },
                    CancellationToken.None)).Embedded.Items);
        }
        public Task<List<DiskEntityInfo>> GetCatalogContents() => GetCatalogContents("/");  
    }
}