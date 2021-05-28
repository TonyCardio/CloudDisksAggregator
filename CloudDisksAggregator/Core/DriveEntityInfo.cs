using System;
using System.Linq;

namespace CloudDisksAggregator.Core
{
    public class DriveEntityInfo
    {
        public readonly string Name;
        public readonly string Expansion;
        public readonly string FullPath;
        public readonly ICloudDriveEngine DriveEngine;

        public DriveEntityInfo(string pathToEntity, ICloudDriveEngine driveEngine)
        {
            var (entityName, entityExpansion) = ParsePathToEntity(pathToEntity);
            Name = entityName;
            Expansion = entityExpansion;
            FullPath = pathToEntity;
            DriveEngine = driveEngine;
        }

        private static ValueTuple<string, string> ParsePathToEntity(string pathToEntity)
        {
            pathToEntity = pathToEntity.Replace(@"\", "/");
            var catalogsAndEntity = pathToEntity.Split('/');
            var entityName = catalogsAndEntity.Last();
            var nameAndExpansion = entityName.Split(".");
            return nameAndExpansion.Length > 1
                ? (entityName, nameAndExpansion.Last())
                : (entityName, "Dir");
        }
    }
}