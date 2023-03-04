using Autofac;
using Forum.Core;
using Forum.Infrastructure.Data;
using Forum.Shared.Interfaces;
using System.Reflection;
using Module = Autofac.Module;


namespace Forum.Infrastructure;


public class InfrastructureDIModule : Module
{
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public InfrastructureDIModule(Assembly? callingAssembly = null)
    {
        var coreAssembly = Assembly.GetAssembly(typeof(CoreDIModule));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureSetup));
        if (coreAssembly != null)
        {
            _assemblies.Add(coreAssembly);
        }
        if (infrastructureAssembly != null)
        {
            _assemblies.Add(infrastructureAssembly);
        }
        if (callingAssembly != null)
        {
            _assemblies.Add(callingAssembly);
        }
    }

    protected override void Load(ContainerBuilder builder)
    {
        RegisterDependencies(builder);
    }

    private void RegisterDependencies(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .InstancePerLifetimeScope();
    }
}
