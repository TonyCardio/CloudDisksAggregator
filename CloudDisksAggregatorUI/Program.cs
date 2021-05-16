using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using CloudDisksAggregator.API;
using CloudDisksAggregator.Core;
using CloudDisksAggregatorUI.UI;

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
            Application.Run(new MainForm(apis));
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