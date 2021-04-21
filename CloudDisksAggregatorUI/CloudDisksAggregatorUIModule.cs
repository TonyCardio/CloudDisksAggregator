using Autofac;
using CloudDisksAggregator.Clouds;
using CloudDisksAggregatorUI.Infrastructure;

namespace CloudDisksAggregatorUI
{
    public class CloudDisksAggregatorUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
            builder.Register(context => context.Resolve<IInMemoryStorageFactory<string, ICloudDrive>>().Create())
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}