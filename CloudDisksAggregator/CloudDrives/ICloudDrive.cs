using CloudDisksAggregator.CloudEngines;

namespace CloudDisksAggregator.CloudDrives
{
    public interface ICloudDrive
    {
        public CloudDriveType DriveType { get; }
        public string AppId { get; }
        public string RedirectUrl { get; }
        public string AuthUrl { get; }

        public ICloudDriveEngine CreateDriveEngine(string userAccessToken);
    }
}