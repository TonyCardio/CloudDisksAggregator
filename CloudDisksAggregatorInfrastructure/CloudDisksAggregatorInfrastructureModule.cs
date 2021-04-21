using Autofac;

namespace CloudDisksAggregatorInfrastructure
{
    public class CloudDisksAggregatorInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
        }
    }
}