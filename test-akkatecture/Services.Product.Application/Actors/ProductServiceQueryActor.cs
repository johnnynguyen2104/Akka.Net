using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;
using Services.Product.Application.Queries;
using Services.Product.Shared.Queries;
using System;

namespace Services.Product.Application.Actors
{
    public class ProductServiceQueryActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private IPropertyQueries _propertyQueries;

        public ProductServiceQueryActor(IServiceProvider services)
        {
            _serviceProvider = services;

            Receive<PropertyQuery>(query =>
            {
                //Using PipeTo instead of await inside the receiver to avoid locking the Mailbox.
                _propertyQueries.GetPropertyAsync(query.Symbol).PipeTo(this.Sender);
            });

            Receive<SearchPropertyQuery>(query =>
            {
                _propertyQueries.SearchPropertiesAsync(query.SearchText).PipeTo(this.Sender);
            });

            Receive<TrendingPropertiesQuery>(query =>
            {
                _propertyQueries.GetTrendingProperties(query.Count).PipeTo(Sender);
            });

            Receive<PropertyCollectionsQuery>(query =>
            {
                 _propertyQueries.GetPropertyCollections(query.Count).PipeTo(this.Sender);
            });

            Receive<PropertyCollectionQuery>(query =>
            {
                _propertyQueries.GetPropertyCollection(query.Id).PipeTo(this.Sender);
            });
        }

        protected override bool AroundReceive(Receive receive, object message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                _propertyQueries = scope.ServiceProvider.GetService<IPropertyQueries>();
                return base.AroundReceive(receive, message);
            }
        }
    }
}
