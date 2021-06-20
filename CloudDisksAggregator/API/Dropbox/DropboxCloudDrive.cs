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
            return storage.GetAllFromDirectory(DropboxSettings.AccountsDirectoryName);
        }

        public IAddingCloudEventHandler AddNewAccount(AddNewCloudControl control)
        {
            var browser = new BrowserAuthControl(
                DropboxSettings.AuthUrl,
                token => new DropboxEngine(token)
            );
            browser.AddingSucceeded += OnSaveNewAccount;
            control.AddChildControl(browser);
            return browser;
        }

        private void OnSaveNewAccount(UserAccount account)
        {
            storage.Add(account, DropboxSettings.AccountsDirectoryName);
        }
    }
}