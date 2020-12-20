using curso.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace CensoRegional.Ioc
{
    public static class DependencyResolver
    {
        public static void AddResolverDependencies(this IServiceCollection services)
        {

            var domainInterfaces = AssemblyReflection.GetInterfaces();
            var repositories = AssemblyReflection.GetInfra();

            foreach (var repo in repositories)
            {
                var @interface = AssemblyReflection.FindInterface(repo, domainInterfaces);

                if (@interface != null)
                    services.AddScoped(@interface, repo);
            }

        }
    }
}
