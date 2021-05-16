namespace CloudDisksAggregator.API.YandexDisk
{
    internal static class YaDiskSettings
    {
        public static string appId = "2a583588aa9244eb975f5fdbf2907089";
        public static string redirectUrl = "https://oauth.yandex.ru/verification_code";
        public static string authUrl => $"https://oauth.yandex.ru/authorize?response_type=token&client_id={appId}";
    }
}