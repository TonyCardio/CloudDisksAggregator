using System.Threading.Tasks;

namespace CloudDisksAggregatorInfrastructure.InMemoryStorage
{
    public interface IInMemoryStorage<TKey, TValue>
    {
        TValue Get(TKey key);
        void Add(TKey key, TValue value);
    }
}