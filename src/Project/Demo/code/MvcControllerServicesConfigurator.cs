using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Semantic.Project.Demo
{
    public class MvcControllerServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}