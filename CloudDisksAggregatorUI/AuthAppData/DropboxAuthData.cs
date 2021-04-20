using Dropbox.Api;

namespace CloudDisksAggregatorUI.AuthAppData
{
    public class DropboxAuthData : IAuthData
    {
        public string AuthUrl { get; }

        public string RedirectUrl { get; }

        public string AppId { get; } = "6dmbqyqong7h511";

        public DropboxAuthData()
        {
            RedirectUrl = "https://oauth.yandex.ru/verification_code";
            AuthUrl = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, AppId, RedirectUrl).AbsoluteUri;
        }
    }
}
