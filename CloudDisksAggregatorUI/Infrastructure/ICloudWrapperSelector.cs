using CloudDisksAggregator.Clouds;
using CloudDisksAggregator.CloudWrappers;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public interface ICloudWrapperSelector
    {
        bool TryGetCloudWrapper(CloudDriveType type, out ICloudDriveWrapper cloudDriveWrapper);
    }
}