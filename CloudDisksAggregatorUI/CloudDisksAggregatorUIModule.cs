using Autofac;
using CloudDisksAggregator.CloudEngines;
using CloudDisksAggregatorUI.Infrastructure;
using CloudDisksAggregatorUI.UI;
using CloudDisksAggregatorUI.UI.ViewEntity;

namespace CloudDisksAggregatorUI
{
    public class CloudDisksAggregatorUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();

            builder.Register(context => context.Resolve<IInMemoryStorageFactory<DriveViewInfo, ICloudDriveEngine>>().Create())
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}