using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutOrderApi.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection)
        {

            Application.Services.Configuration.DependenciesConfiguration.ConfigureDependencies(serviceCollection);

            return serviceCollection;
        }
    }
}
