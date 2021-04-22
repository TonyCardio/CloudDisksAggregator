using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using CloudDisksAggregator.Clouds;
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
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = BuildContainer();
            var selector = container.Resolve<ICloudWrapperSelector>();
            var storage = container.Resolve<IInMemoryStorage<DriveViewInfo, ICloudDrive>>();
            Application.Run(new MainForm(selector,storage));
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            var path = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var assembly in Directory.GetFiles(path, "CloudDisksAggregator*.dll")
                .Select(Assembly.LoadFrom))
            {
                builder.RegisterAssemblyModules(assembly);
            }

            return builder.Build();
        }
    }
}