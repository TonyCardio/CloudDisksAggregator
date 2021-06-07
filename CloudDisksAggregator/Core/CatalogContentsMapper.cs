using System.Collections.Generic;
using System.Linq;
using Dropbox.Api.Files;
using YandexDisk.Client.Protocol;

namespace CloudDisksAggregator.Core
{
    internal static class CatalogContentsMapper
    {
        public static List<DriveEntityInfo> MapYandexCatalogContent(IEnumerable<Resource> catalogContent, 
            ICloudDriveEngine driveEngine)
            => catalogContent
                .Select(entity => new DriveEntityInfo(
                    entity.Path
                        .Replace("disk:", ""),
                    driveEngine))
                .ToList();

        public static List<DriveEntityInfo> MapDropboxCatalogContent(IEnumerable<Metadata> catalogContent,
            ICloudDriveEngine driveEngine)
            => catalogContent
                .Select(entity => new DriveEntityInfo(
                    entity.PathDisplay, driveEngine))
                .ToList();
    }
}