using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CloudDisksAggregatorInfrastructure.InMemoryStorage
{
    public class SimpleInMemoryStorage<TKey, TValue> : IInMemoryStorage<TKey, TValue>
    {
        private static readonly JsonSerializerSettings settings =
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

        private readonly Dictionary<TKey, TValue> items;
        private readonly string pathToData;

        public SimpleInMemoryStorage(string pathToData)
        {
            this.pathToData = pathToData;
            if (!Directory.Exists(pathToData))
                Directory.CreateDirectory(pathToData);
            items = new Dictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            return items[key];
        }

        public void Add(TKey key, TValue value)
        {
            items.Add(key, value);
            var kvp = new KeyValuePair<TKey, TValue>(key, value);
            var serialized = JsonConvert.SerializeObject(kvp, Formatting.Indented, settings);
            File.WriteAllText(Path.Join(pathToData, $"{Guid.NewGuid()}.json"), serialized);
        }

        public void WarmUpCache()
        {
            foreach (var filePath in Directory.GetFiles(pathToData))
            {
                var serialized = File.ReadAllText(filePath);
                var (key, value) = JsonConvert.DeserializeObject<KeyValuePair<TKey, TValue>>(serialized, settings);
                items.Add(key, value);
            }
        }

        public List<(TKey, TValue)> GetAllElements()
        {
            var pairs = new List<(TKey, TValue)>();
            foreach (var (key, value) in items)
                 pairs.Add( (key, value));
            return pairs;
        }
    }
}