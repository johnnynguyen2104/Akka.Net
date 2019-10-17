using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Configuration;
using Pricing.Service.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pricing.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shouldBreakTheLoop = false;
            var pricingService = new PricingService();
            pricingService.Start();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                pricingService.Stop();
                eventArgs.Cancel = true;
                shouldBreakTheLoop = true;
            };

            SendRandomPrice(pricingService).Wait();

            pricingService.WhenTerminated.Wait();
        }

        static async Task SendRandomPrice(PricingService pricingService)
        {
            var random = new Random();
            int maxLoopTimes = 5, loopCount = 0;

            while (loopCount != maxLoopTimes)
            {
                await Task.Delay(5000);

                decimal newPrice = random.Next(0, 30);
                Console.WriteLine($"New price has been generated at {DateTime.UtcNow} on Pricing Service.");

                pricingService.PriceUpdaterActor.Tell(new UpdateNewPriceCommand(newPrice));

                loopCount++;
            }
        }
    }
}
