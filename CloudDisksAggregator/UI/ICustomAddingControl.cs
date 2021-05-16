using System;
using CloudDisksAggregator.API;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.UI
{
    public interface ICustomAddingControl
    {
        event Action<UserAccount> AddingSucceeded;
    }
}