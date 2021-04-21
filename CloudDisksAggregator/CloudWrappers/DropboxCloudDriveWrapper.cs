using CloudDisksAggregator.Clouds;
using Dropbox.Api;

namespace CloudDisksAggregator.CloudWrappers
{
    public class DropboxCloudDriveWrapper : ICloudDriveWrapper
    {
        public CloudDriveType DriveType => CloudDriveType.Dropbox;
        public string AppId => "6dmbqyqong7h511";
        public string RedirectUrl => "https://oauth.yandex.ru/verification_code";

        public string AuthUrl =>
            DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, AppId, RedirectUrl).AbsoluteUri;

        public ICloudDrive CreateCloudDrive(string userAccessToken) => new DropboxApiHandler(userAccessToken);
    }
}