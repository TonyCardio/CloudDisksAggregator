using Autofac;
using CloudDisksAggregatorUI.FileContent.FileViewers;
using CloudDisksAggregatorUI.FileContent.Readers;

namespace CloudDisksAggregatorUI
{
    public class CloudDisksAggregatorUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IContentReader<>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(FileViewer).Assembly)
                .Where(t => t.IsSubclassOf(typeof(FileViewer)))
                .As<FileViewer>();
        }
    }
}