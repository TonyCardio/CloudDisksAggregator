using CloudDisksAggregator;
using CloudDisksAggregator.CloudDrives;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public interface ICloudDriveSelector
    {
        bool TryGetCloudDrive(CloudDriveType type, out ICloudDrive cloudDrive);
    }
}