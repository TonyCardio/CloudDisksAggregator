namespace CloudDisksAggregatorUI.AuthAppData
{
    public interface IAuthData
    {
        string AuthUrl { get; }
        string RedirectUrl { get; }
        string AppId { get; }
    }
}
