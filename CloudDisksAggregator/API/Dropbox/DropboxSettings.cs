using Dropbox.Api;

namespace CloudDisksAggregator.API.Dropbox
{
    internal static class DropboxSettings
    {
        private const string AppId = "6dmbqyqong7h511";
        private const string RedirectUrl = "https://oauth.yandex.ru/verification_code";

        public static string AuthUrl =>
            DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, AppId, RedirectUrl).AbsoluteUri;
    }
}