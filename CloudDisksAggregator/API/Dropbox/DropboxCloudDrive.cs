using System.Collections.Generic;
using CloudDisksAggregator.Core;
using CloudDisksAggregator.UI;
using CloudDisksAggregatorInfrastructure.InMemoryJsonStorage;

namespace CloudDisksAggregator.API.Dropbox
{
    internal class DropboxCloudDrive : ICloudDriveObject
    {
        private readonly IInMemoryJsonStorage<UserAccount> storage;

        public DropboxCloudDrive(IInMemoryJsonStorage<UserAccount> storage)
        {
            this.storage = storage;
        }

        public IEnumerable<UserAccount> LoadAccounts()
        {
            return storage.GelAllFromDirectory(DropboxSettings.AccountsDirectoryName);
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
            storage.Add(account, DropboxSettings.AccountsDirectoryName);
        }
    }
}