using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersistentUnreal.Data;
using PersistentUnreal.Mediators;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace PersistentUnreal
{
    public static class IocStartup
    {
        public static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            SetupServices(services);
            SetupMediators(services);

            // For WebApi controllers, you may want to have a bit of reflection
            var controllerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
              .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
              .Where(t => typeof(ApiController).IsAssignableFrom(t)
                || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            // It is only that you need to get service provider in the end
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void SetupServices(IServiceCollection services)
        {
            services.AddDbContext<PUAccountDbContext>(opt => opt.UseInMemoryDatabase("Account"));
            services.AddDbContext<ItemContext>(opt => opt.UseInMemoryDatabase("Item"));
        }

        private static void SetupMediators(IServiceCollection services)
        {
            services.AddScoped<IPUAccountMediator, PUAccountMediator>();
        }
    }
}
