using System;
using CloudDisksAggregator.API;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.UI
{
    public interface IAddingCloudEventHandler
    {
        event Action<UserAccount> AddingSucceeded;
    }
}