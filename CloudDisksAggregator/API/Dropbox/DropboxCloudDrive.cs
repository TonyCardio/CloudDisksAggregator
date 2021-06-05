using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CloudDisksAggregator.Core;
using CloudDisksAggregator.UI;
using Newtonsoft.Json;

namespace CloudDisksAggregator.API.Dropbox
{
    public class DropboxCloudDrive : ICloudDriveObject
    {
        private static readonly JsonSerializerSettings settings =
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

        private readonly string pathToData = Path.Combine(Environment.CurrentDirectory, "dropbox_data");

        public IEnumerable<UserAccount> LoadAccounts()
        {
            if (!Directory.Exists(pathToData))
                Directory.CreateDirectory(pathToData);

            return Directory.GetFiles(pathToData)
                .Select(File.ReadAllText)
                .Select(x => JsonConvert.DeserializeObject<UserAccount>(x, settings));
        }

        public void AddNewAccount(AddNewCloudControl control)
        {
            control.AddingSucceeded += OnSaveNewAccount;
            control.AddChildAddingControl(new BrowserAuthControl(
                DropboxSettings.AuthUrl,
                token => new DropboxEngine(token)
            ));
        }

        private void OnSaveNewAccount(UserAccount account)
        {
            var serialized = JsonConvert.SerializeObject(account, Formatting.Indented, settings);
            File.WriteAllText(Path.Join(pathToData, $"{Guid.NewGuid()}.json"), serialized);
        }
    }
}