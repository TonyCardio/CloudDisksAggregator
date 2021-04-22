using Autofac;

namespace CloudDisksAggregator
{
    public class CloudDisksAggregatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces();
        }
    }
}