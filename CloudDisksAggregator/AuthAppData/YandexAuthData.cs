namespace CloudDisksAggregator.AuthAppData
{
    public class YandexAuthData : IAuthData
    {
        public string AuthUrl { get; }

        public string RedirectUrl { get; }

        public string AppId { get; } = "2a583588aa9244eb975f5fdbf2907089";

        public YandexAuthData()
        {
            AuthUrl = $"https://oauth.yandex.ru/authorize?response_type=token&client_id={AppId}";
            RedirectUrl = "https://oauth.yandex.ru/verification_code";
        }
    }
}
