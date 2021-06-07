using System.Collections.Generic;
using CloudDisksAggregator.Core;
using CloudDisksAggregator.UI;
using CloudDisksAggregatorInfrastructure.InMemoryJsonStorage;

namespace CloudDisksAggregator.API.YandexDisk
{
    public class YaDiskCloudDrive : ICloudDriveObject
    {
        private readonly IInMemoryJsonStorage<UserAccount> storage;

        public YaDiskCloudDrive(IInMemoryJsonStorage<UserAccount> storage)
        {
            this.storage = storage;
        }

        public IEnumerable<UserAccount> LoadAccounts()
        {
            return storage.GelAllFromDirectory(YaDiskSettings.AccountsDirectoryName);
        }

        public void AddNewAccount(AddNewCloudControl control)
        {
            control.AddingSucceeded += OnSaveNewAccount;
            control.AddChildAddingControl(new BrowserAuthControl(
                YaDiskSettings.AuthUrl,
                token => new YandexDiskEngine(token)
            ));
        }

        private void OnSaveNewAccount(UserAccount account)
        {
            storage.Add(account, YaDiskSettings.AccountsDirectoryName);
        }
    }
}