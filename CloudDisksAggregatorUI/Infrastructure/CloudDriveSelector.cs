using System.Collections.Generic;
using System.Linq;
using CloudDisksAggregator;
using CloudDisksAggregator.CloudDrives;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public class CloudDriveSelector : ICloudDriveSelector
    {
        private readonly Dictionary<CloudDriveType, ICloudDrive> driveTypeToDrives;

        public CloudDriveSelector(IEnumerable<ICloudDrive> drives)
        {
            driveTypeToDrives = drives.ToDictionary(x => x.DriveType);
        }

        public bool TryGetCloudDrive(CloudDriveType type, out ICloudDrive cloudDrive)
        {
            return driveTypeToDrives.TryGetValue(type, out cloudDrive);
        }
    }
}