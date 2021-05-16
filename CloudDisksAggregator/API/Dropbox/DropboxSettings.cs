using Dropbox.Api;

namespace CloudDisksAggregator.API.Dropbox
{
    public class DropboxSettings
    {
        public static string appId = "6dmbqyqong7h511";
        public static string redirectUrl = "https://oauth.yandex.ru/verification_code";

        public static string authUrl =
            DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, appId, redirectUrl).AbsoluteUri;
    }
}