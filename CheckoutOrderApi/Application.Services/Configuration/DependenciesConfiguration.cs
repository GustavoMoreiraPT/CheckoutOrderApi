using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data.Repository;
using Domain.Core;
using Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IItemsService, ItemsService>();
            serviceCollection.AddSingleton<IOrdersService, OrdersService>();

            serviceCollection.AddSingleton<IRepository<Customer>, GenericInMemoryRepository<Customer>>();
            serviceCollection.AddSingleton<IRepository<Order>, GenericInMemoryRepository<Order>>();
            serviceCollection.AddSingleton<IRepository<OrderItem>, GenericInMemoryRepository<OrderItem>>();
            serviceCollection.AddSingleton<IRepository<Item>, GenericInMemoryRepository<Item>>();

            serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
            

            Data.Repository.Configuration.DependenciesConfiguration.ConfigureDependencies(serviceCollection);

            return serviceCollection;
        }
    }
}
