﻿using System;
using System.Linq;

namespace CloudDisksAggregator.Data
{
    public class DiskEntityInfo
    {
        public readonly string Name;
        public readonly string Expansion;
        public readonly string FullPath;
        
        public DiskEntityInfo(string pathToEntity)
        {
            var (entityName, entityExpansion) = ParsePathToEntity(pathToEntity);
            Name = entityName;
            Expansion = entityExpansion;
            FullPath = pathToEntity;
        }

        private static ValueTuple<string, string> ParsePathToEntity(string pathToEntity)
        {
            var catalogsAndEntity = pathToEntity.Split('/');
            var entityName = catalogsAndEntity.Last();
            var nameAndExpansion = entityName.Split(".");
            return nameAndExpansion.Length > 1
                ? (entityName, nameAndExpansion.Last())
                : (entityName, "Dir");
        }
    }
}