using System;
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
    public class YandexDiskApiHandler: CloudDriveHelper, ICloudDrive
    {
        private IDiskApi DiskApi { get; set; }

        public void SetToken(string token) => DiskApi = new DiskHttpApi(token);

        public async Task Upload(string pathToEntity, string pathToCatalogForSave = "")
        {
            ThrowIfTokenNotSet(DiskApi.Equals(null));
            var entity = GetEntityInfo(pathToEntity);
            var pathForSave = $"{pathToCatalogForSave}/{entity.Name}";
            await DiskApi.Files.UploadFileAsync(
                pathForSave, 
                false, 
                pathToEntity, 
                CancellationToken.None);
        }

        public async Task Download(string pathToEntity, string pathToCatalogForSave = "")
        {
            ThrowIfTokenNotSet(DiskApi.Equals(null));
            var entity = GetEntityInfo(pathToEntity);
            if (pathToCatalogForSave.Equals(""))
                pathToCatalogForSave = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            await DiskApi.Files.DownloadFileAsync(path: entity.FullPath,
                Path.Combine(pathToCatalogForSave, entity.Name));
        }

        public async Task<object> GetCatalogContents(string pathToCatalog = "/")
        {
            ThrowIfTokenNotSet(DiskApi.Equals(null));
            return CatalogContentsMapper.YandexCatalogContentsMapper(
                (await DiskApi.MetaInfo.GetInfoAsync(
                    new ResourceRequest {Path = pathToCatalog},
                    CancellationToken.None)).Embedded.Items);
        }
    }
}