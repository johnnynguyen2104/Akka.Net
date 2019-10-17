using Microsoft.Extensions.DependencyInjection;
using Services.Product.Application.Actors;
using Services.Product.Application.Queries;
using System;

namespace Services.Product.Application
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();

            var productService = new ProductService(_serviceProvider);

            productService.WhenTerminated.Wait();

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IPropertyQueries, PropertyQueries>();
            // ...
            // Add other services
            // ...
            _serviceProvider = collection.BuildServiceProvider();
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
