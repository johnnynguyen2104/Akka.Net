using System;

namespace Pricing.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var pricingService = new PricingSubcriberService();
            pricingService.Start();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                pricingService.Stop();
                eventArgs.Cancel = true;
            };

            pricingService.WhenTerminated.Wait();
        }
    }
}
