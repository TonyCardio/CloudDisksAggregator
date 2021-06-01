﻿using System.Drawing;
using CloudDisksAggregator.API.YandexDisk.Resources;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.API.YandexDisk
{
    public class YaDiskResources : IResourceObject
    {
        public Bitmap MainLogo => YandexRes.yandexDiskMainLogo;
        public string DriveTypeName => "Yandex Disk";
    }
}