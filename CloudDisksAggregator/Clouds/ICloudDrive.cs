﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CloudDisksAggregator.Data;

namespace CloudDisksAggregator.Clouds
{
    public interface ICloudDrive
    {
        public void SetToken(string token);
        public Task Upload(string pathToEntity, string pathToCatalogForSave = "/");
        public Task Download(string pathToEntity, string pathToCatalogForSave);
        public Task<List<DiskEntityInfo>> GetCatalogContents(string pathToCatalog);
    }
}