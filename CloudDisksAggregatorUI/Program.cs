using System;
using System.Windows.Forms;
using CloudDisksAggregator.Clouds;
using CloudDisksAggregator.CloudWrappers;
using CloudDisksAggregatorInfrastructure.InMemoryStorage;
using CloudDisksAggregatorUI.Infrastructure;
using CloudDisksAggregatorUI.UI;
using CloudDisksAggregatorUI.UI.ViewEntity;

namespace CloudDisksAggregatorUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var wrappers = new ICloudDriveWrapper[] { new YandexCloudDriveWrapper(), new DropboxCloudDriveWrapper() };
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(
                new CloudWrapperSelector(wrappers),
                new WarmedUpCloudDriveStorageFactory(new AppSettings())));
        }
    }
}