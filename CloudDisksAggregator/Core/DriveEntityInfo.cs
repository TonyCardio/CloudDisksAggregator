using System;
using System.Linq;

namespace CloudDisksAggregator.Core
{
    public class DriveEntityInfo
    {
        public readonly string Name;
        public readonly string Expansion;
        public readonly string FullPath;
        public readonly string Parent;
        public readonly ICloudDriveEngine DriveEngine;

        public DriveEntityInfo(string pathToEntity, ICloudDriveEngine driveEngine)
        {
            var (entityName, entityExpansion, parent) = ParsePathToEntity(pathToEntity);
            Name = entityName;
            Expansion = entityExpansion;
            FullPath = ParsePathToFullPath(pathToEntity);
            Parent = parent;
            DriveEngine = driveEngine;
        }

        private static ValueTuple<string, string, string> ParsePathToEntity(string pathToEntity)
        {
            pathToEntity = pathToEntity.Replace(@"\", "/");
            var catalogsAndEntity = pathToEntity.Split('/');
            var parent = string
                .Join("/",catalogsAndEntity
                    .Take(catalogsAndEntity.Length - 1)
                    .ToArray());
            var entityName = catalogsAndEntity.Last();
            var nameAndExpansion = entityName.Split(".");
            return nameAndExpansion.Length > 1
                ? (entityName, nameAndExpansion.Last(), parent)
                : (entityName, "Dir", parent);
        }

        private static string ParsePathToFullPath(string pathToEntity) 
            => pathToEntity[0] == '/' ? pathToEntity : '/' + pathToEntity;
        
        public bool IsDirectory => Expansion == "Dir";
    }
}