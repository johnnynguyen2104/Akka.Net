using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Product.Application.Actors
{
    public sealed class ProductService
    {
        public ActorSystem ActorSystem { get; }

        public IActorRef ProductQueryActor { get; }

        public Task WhenTerminated => ActorSystem.WhenTerminated;

        public ProductService(IServiceProvider services)
        {

            var config = ConfigurationFactory.ParseString(File.ReadAllText("serivces.product.hocon"));
            ActorSystem = ActorSystem.Create("productservice", config);

            ProductQueryActor = ActorSystem.ActorOf(Props.Create(() => new ProductServiceQueryActor(services)), "product");

            //Register the actors that should be available for client 
            ClusterClientReceptionist.Get(ActorSystem).RegisterService(ProductQueryActor);
        }
    }
}
