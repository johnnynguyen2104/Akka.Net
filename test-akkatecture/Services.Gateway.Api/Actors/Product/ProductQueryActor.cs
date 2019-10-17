using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Routing;
using Services.Gateway.Api.Actors.Product.Messages;
using Services.Product.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Services.Gateway.Api.Actors.Product
{
    public class ProductQueryActor : ReceiveActor
    {
        private IActorRef ProductServiceClient = ActorRefs.Nobody;

        //This route is matched with the router that's defined in the serivces.product.hocon.
        private readonly string ProductServiceRoute = "/user/product";

        public ProductQueryActor(string productServiceAddress, IServiceProvider services, ActorSystem actorSystem)
        {
            // Create the ClusterClient actor and use it as a gateway for sending messages to the actors identified by their path
            var initialContacts = ImmutableHashSet.Create<ActorPath>(new ActorPath[] {
                ActorPath.Parse(productServiceAddress)
            });

            ProductServiceClient = actorSystem.ActorOf(ClusterClient.Props(ClusterClientSettings.Create(actorSystem).WithInitialContacts(initialContacts)), "productclient");

            Receive<GetPropertyMessage>(msg =>
            {
                ProductServiceClient.Forward(new ClusterClient.Send(ProductServiceRoute, new PropertyQuery(msg.Symbol)));
            });

            Receive<SearchPropertiesMessage>(msg =>
            {
                ProductServiceClient.Forward(new ClusterClient.Send(ProductServiceRoute, new SearchPropertyQuery(msg.SearchText)));
            });

            Receive<GetTrendingPropertiesMessage>(msg =>
            {
                ProductServiceClient.Forward(new ClusterClient.Send(ProductServiceRoute, new TrendingPropertiesQuery(msg.Count)));
            });

            Receive<GetPropertyCollectionsMessage>(msg =>
            {
                ProductServiceClient.Forward(new ClusterClient.Send(ProductServiceRoute, new PropertyCollectionsQuery(msg.Count)));
            });

            Receive<GetPropertyCollectionMessage>(msg =>
            {
                ProductServiceClient.Forward(new ClusterClient.Send(ProductServiceRoute, new PropertyCollectionQuery(msg.Id)));
            });
        }
    }
}
