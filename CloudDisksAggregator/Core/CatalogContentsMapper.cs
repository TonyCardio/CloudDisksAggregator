using System.Collections.Generic;
using System.Linq;
using Dropbox.Api.Files;
using YandexDisk.Client.Protocol;

namespace CloudDisksAggregator.Core
{
    public static class CatalogContentsMapper
    {
        public static List<DriveEntityInfo> MapYandexCatalogContent(IEnumerable<Resource> catalogContent)
            => catalogContent
                .Select(entity => new DriveEntityInfo(
                    entity.Path
                        .Replace("disk:", "")))
                .ToList();

        public static List<DriveEntityInfo> MapDropboxCatalogContent(IEnumerable<Metadata> catalogContent)
            => catalogContent
                .Select(entity => new DriveEntityInfo(
                    entity.PathDisplay))
                .ToList();
    }
}