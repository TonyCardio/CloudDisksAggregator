using Autofac;
using CloudDisksAggregator.FileContent.FileViewers;
using CloudDisksAggregator.FileContent.Readers;

namespace CloudDisksAggregator
{
    public class CloudDisksAggregatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IContentReader<>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(FileViewer).Assembly)
                .Where(t => t.IsSubclassOf(typeof(FileViewer)))
                .As<FileViewer>();
        }
    }
}