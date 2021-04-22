using CloudDisksAggregator.CloudEngines;

namespace CloudDisksAggregator.CloudDrives
{
    public class YandexCloudDrive : ICloudDrive
    {
        public CloudDriveType DriveType => CloudDriveType.YandexDisk;
        public string AppId => "2a583588aa9244eb975f5fdbf2907089";
        public string RedirectUrl => "https://oauth.yandex.ru/verification_code";
        public string AuthUrl => $"https://oauth.yandex.ru/authorize?response_type=token&client_id={AppId}";

        public ICloudDriveEngine CreateDriveEngine(string userAccessToken) => new YandexDiskEngine(userAccessToken);
    }
}