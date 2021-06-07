using System.Collections.Generic;

namespace CloudDisksAggregatorInfrastructure.InMemoryJsonStorage
{
    public interface IInMemoryJsonStorage<TValue>
    {
        void Add(TValue value, string directoryName);
        IEnumerable<TValue> GelAllFromDirectory(string directoryName);
    }
}