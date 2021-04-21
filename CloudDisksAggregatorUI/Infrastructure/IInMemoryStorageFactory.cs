using CloudDisksAggregatorInfrastructure.InMemoryStorage;

namespace CloudDisksAggregatorUI.Infrastructure
{
    public interface IInMemoryStorageFactory<TKey, TValue>
    {
        IInMemoryStorage<TKey, TValue> Create();
    }
}