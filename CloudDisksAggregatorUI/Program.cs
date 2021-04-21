using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using CloudDisksAggregator;
using CloudDisksAggregatorInfrastructure;
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
            Application.Run(new MainForm());
            var container = BuildContainer();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var assembly in Directory.GetFiles(
                path, "CloudDisksAggregator*Module.dll").Select(Assembly.LoadFrom))
            {
                builder.RegisterAssemblyModules(assembly);
            }

            return builder.Build();
        }
    }
}