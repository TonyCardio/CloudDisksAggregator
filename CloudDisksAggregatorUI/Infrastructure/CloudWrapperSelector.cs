using System.Collections.Generic;
using System.Linq;
using CloudDisksAggregator.CloudWrappers;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public class CloudWrapperSelector : ICloudWrapperSelector
    {
        private readonly Dictionary<CloudDriveType, ICloudDriveWrapper> driveTypeToWrappers;

        public CloudWrapperSelector(IEnumerable<ICloudDriveWrapper> wrappers)
        {
            driveTypeToWrappers = wrappers.ToDictionary(x => x.DriveType);
        }

        public bool TryGetCloudWrapper(CloudDriveType type, out ICloudDriveWrapper cloudDriveWrapper)
        {
            return driveTypeToWrappers.TryGetValue(type, out cloudDriveWrapper);
        }
    }
}