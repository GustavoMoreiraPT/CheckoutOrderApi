using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection)
        {

            return serviceCollection;
        }
    }
}
