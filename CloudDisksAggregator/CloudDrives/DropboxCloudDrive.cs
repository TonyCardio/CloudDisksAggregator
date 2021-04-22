using CloudDisksAggregator.CloudEngines;
using Dropbox.Api;

namespace CloudDisksAggregator.CloudDrives
{
    public class DropboxCloudDrive : ICloudDrive
    {
        public CloudDriveType DriveType => CloudDriveType.Dropbox;
        public string AppId => "6dmbqyqong7h511";
        public string RedirectUrl => "https://oauth.yandex.ru/verification_code";

        public string AuthUrl =>
            DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, AppId, RedirectUrl).AbsoluteUri;

        public ICloudDriveEngine CreateDriveEngine(string userAccessToken) => new DropboxEngine(userAccessToken);
    }
}