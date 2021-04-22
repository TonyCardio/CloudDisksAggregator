using System.Collections.Generic;

namespace CloudDisksAggregatorInfrastructure.InMemoryStorage
{
    public interface IInMemoryStorage<TKey, TValue>
    {
        TValue Get(TKey key);
        void Add(TKey key, TValue value);
        List<(TKey, TValue)> GetAllElements();
    }
}