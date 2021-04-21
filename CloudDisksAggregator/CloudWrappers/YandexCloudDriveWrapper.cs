using CloudDisksAggregator.Clouds;

namespace CloudDisksAggregator.CloudWrappers
{
    public class YandexCloudDriveWrapper : ICloudDriveWrapper
    {
        public CloudDriveType DriveType => CloudDriveType.YandexDisk;
        public string AppId => "2a583588aa9244eb975f5fdbf2907089";
        public string RedirectUrl => "https://oauth.yandex.ru/verification_code";
        public string AuthUrl => $"https://oauth.yandex.ru/authorize?response_type=token&client_id={AppId}";

        public ICloudDrive CreateCloudDrive(string userAccessToken) => new YandexDiskApiHandler(userAccessToken);
    }
}