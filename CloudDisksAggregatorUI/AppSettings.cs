using System;
using System.IO;
using CloudDisksAggregatorUI.Infrastructure;

namespace CloudDisksAggregatorUI
{
    public class AppSettings : IAppSettings
    {
        public string PathToObjectsStorage => Path.Combine(Environment.CurrentDirectory, "data");
    }
}