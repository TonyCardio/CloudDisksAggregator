using System.Collections.Generic;
using CloudDisksAggregator.UI;

namespace CloudDisksAggregator.Core
{
    public interface ICloudDriveObject
    {
        IEnumerable<UserAccount> LoadAccounts();
        void AddNewAccount(AddNewCloudControl control);
        
    }
}