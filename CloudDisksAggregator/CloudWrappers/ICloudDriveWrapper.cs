using CloudDisksAggregator.Clouds;

namespace CloudDisksAggregator.CloudWrappers
{
    public interface ICloudDriveWrapper
    {
        public CloudDriveType DriveType { get; }
        public string AppId { get; }
        public string RedirectUrl { get; }
        public string AuthUrl { get; }

        public ICloudDrive CreateCloudDrive(string userAccessToken);
    }
}