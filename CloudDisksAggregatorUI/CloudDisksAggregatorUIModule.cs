using Autofac;

namespace CloudDisksAggregatorUI
{
    public class CloudDisksAggregatorUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
        }
    }
}