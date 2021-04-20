using System.Collections.Generic;
using System.Linq;
using Dropbox.Api.Files;
using YandexDisk.Client.Protocol;

namespace CloudDisksAggregator.Data
{
    public static class CatalogContentsMapper
    {
        public static List<DiskEntityInfo> YandexCatalogContentsMapper(IEnumerable<Resource> catalogContent)
            => catalogContent
                .Select(entity => new DiskEntityInfo(
                    entity.Path
                        .Replace("disk:", "")))
                .ToList();

        public static List<DiskEntityInfo> DropboxCatalogContentsMapper(IEnumerable<Metadata> catalogContent) 
            => catalogContent
                .Select(entity => new DiskEntityInfo(
                    entity.PathDisplay))
                .ToList();
    }
}