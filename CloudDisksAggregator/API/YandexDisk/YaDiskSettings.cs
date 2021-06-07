namespace CloudDisksAggregator.API.YandexDisk
{
    internal static class YaDiskSettings
    {
        private const string AppId = "2a583588aa9244eb975f5fdbf2907089";
        public static string AuthUrl => $"https://oauth.yandex.ru/authorize?response_type=token&client_id={AppId}";
        public static string AccountsDirectoryName => "yandex_data";
    }
}