using Autofac;
using CloudDisksAggregator.Core;
using CloudDisksAggregatorUI.FileContent;
using CloudDisksAggregatorUI.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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
            var apis = container.Resolve<ICloudApi[]>();
            var viewerFactory = container.Resolve<IViewerFactory>();
            Application.Run(new MainForm(apis, viewerFactory));
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(path, "CloudDisksAggregator*.dll")
                .Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyModules(assemblies);

            return builder.Build();
        }
    }
}