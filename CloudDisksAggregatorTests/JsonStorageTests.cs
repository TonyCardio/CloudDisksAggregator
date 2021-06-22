using System;
using System.IO;
using System.Linq;
using CloudDisksAggregatorInfrastructure.InMemoryJsonStorage;
using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;

namespace CloudDisksAggregatorTests
{
    public interface ISomeInterface
    {
    }

    public class SomeImplementation : ISomeInterface
    {
        public string Prop { get; }

        public SomeImplementation(string prop)
        {
            Prop = prop;
        }
    }

    public class SomeClass
    {
        public ISomeInterface InterfaceProperty { get; }
        public readonly string PublicField;
        public readonly string nullField;
        private readonly string privateField;
        [JsonProperty] private string privateFieldWithAttribute;

        public SomeClass(
            ISomeInterface interfaceProperty,
            string privateField,
            string publicField,
            string privateFieldWithAttribute)
        {
            InterfaceProperty = interfaceProperty;
            this.privateField = privateField;
            PublicField = publicField;
            this.privateFieldWithAttribute = privateFieldWithAttribute;
        }

        public string GetPrivateFieldWithAttribute() => privateFieldWithAttribute;
        public string GetPrivateField() => privateField;
    }

    [TestFixture]
    internal class JsonStorageTests
    {
        private SimpleInMemoryJsonStorage<SomeClass> storage;
        private SomeClass obj;
        private SomeImplementation interfaceImplementation;
        private const string DirectoryName = "some_dir";

        [SetUp]
        public void Setup()
        {
            storage = new SimpleInMemoryJsonStorage<SomeClass>();
            interfaceImplementation = new SomeImplementation("prop");
            obj = new SomeClass(
                interfaceImplementation,
                "private-field",
                "public-field",
                "private-with-attribute"
            );
        }

        [Test]
        public void ShouldLoadField_WhenItIsPublic()
        {
            AddObjectToStorage();

            var loadedObject = storage.GetAllFromDirectory(DirectoryName).FirstOrDefault();
            loadedObject?.PublicField.Should().BeEquivalentTo(obj.PublicField);
        }

        [Test]
        public void ShouldLoadProp_WhenItIsInterfaceProp()
        {
            AddObjectToStorage();

            var loadedObject = storage.GetAllFromDirectory(DirectoryName).FirstOrDefault();
            loadedObject?.InterfaceProperty.Should().BeEquivalentTo(interfaceImplementation);
        }

        [Test]
        public void ShouldLoadPrivateField_WhenItMarketByAttribute()
        {
            AddObjectToStorage();

            var loadedObject = storage.GetAllFromDirectory(DirectoryName).FirstOrDefault();
            loadedObject?.GetPrivateFieldWithAttribute()
                .Should().BeEquivalentTo(obj.GetPrivateFieldWithAttribute());
        }

        [Test]
        public void ShouldSaveObjectTwice_WhenItIsTheSameObjects()
        {
            AddObjectToStorage();
            AddObjectToStorage();

            var loadedObjects = storage.GetAllFromDirectory(DirectoryName).ToList();
            loadedObjects.Count.Should().Be(2);
            loadedObjects[0].Should().BeEquivalentTo(loadedObjects[1]);
        }

        [Test]
        public void ShouldSaveField_WhenItIsNull()
        {
            AddObjectToStorage();

            var loadedObject = storage.GetAllFromDirectory(DirectoryName).FirstOrDefault();
            loadedObject?.nullField.Should().Be(null);
        }

        [Test]
        public void ShouldSetDefaultValueToField_WhenItIsPrivate()
        {
            AddObjectToStorage();

            var loadedObject = storage.GetAllFromDirectory(DirectoryName).FirstOrDefault();
            loadedObject?.GetPrivateField().Should().Be(null);
        }

        private void AddObjectToStorage()
        {
            storage.Add(obj, DirectoryName);
        }

        [TearDown]
        public void TearDown()
        {
            var dataPath = Path.Combine(Environment.CurrentDirectory, "data");
            if (Directory.Exists(dataPath))
                Directory.Delete(dataPath, true);
        }
    }
}