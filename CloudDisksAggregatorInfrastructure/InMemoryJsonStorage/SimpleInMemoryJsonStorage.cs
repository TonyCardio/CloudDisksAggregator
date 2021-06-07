using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CloudDisksAggregatorInfrastructure.InMemoryJsonStorage
{
    public class SimpleInMemoryJsonStorage<TValue> : IInMemoryJsonStorage<TValue>
    {
        private readonly JsonSerializerSettings settings =
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

        private readonly string baseDirectory = Path.Combine(Environment.CurrentDirectory, "data");

        public SimpleInMemoryJsonStorage()
        {
            if (!Directory.Exists(baseDirectory))
                Directory.CreateDirectory(baseDirectory);
        }

        public void Add(TValue value, string directoryName)
        {
            var path = Path.Combine(baseDirectory, directoryName);
            var serialized = JsonConvert.SerializeObject(value, Formatting.Indented, settings);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(Path.Join(path, $"{Guid.NewGuid()}.json"), serialized);
        }

        public IEnumerable<TValue> GetAllFromDirectory(string directoryName)
        {
            var path = Path.Combine(baseDirectory, directoryName);
            return Directory.GetFiles(path)
                .Select(File.ReadAllText)
                .Select(x => JsonConvert.DeserializeObject<TValue>(x, settings));
        }
    }
}