namespace CloudDisksAggregator.Core
{
    public interface ICloudApi
    {
        ICloudDriveObject Drive { get; }
        IResourceObject Resources { get; }
    }
}