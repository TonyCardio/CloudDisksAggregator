namespace CloudDisksAggregatorUI.FileContent.Readers
{
    public interface IContentReader<TContent>
    {
        TContent FromBytes(byte[] bytes);
    }
}
