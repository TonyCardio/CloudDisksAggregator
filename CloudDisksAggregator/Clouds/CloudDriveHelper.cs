using System;
using CloudDisksAggregator.Data;

namespace CloudDisksAggregator.Clouds
{
    public class CloudDriveHelper
    {
        protected static DiskEntityInfo GetEntityInfo(string pathToEntity) => new(pathToEntity);

        protected static void ThrowIfTokenNotSet(bool isNotSet)
        {
            if (isNotSet) throw new InvalidOperationException("Token have not set");
        }
    }
}