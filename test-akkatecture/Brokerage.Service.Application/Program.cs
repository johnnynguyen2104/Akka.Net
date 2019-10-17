using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Brokerage.Service.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder<Startup>(args)
                .Build();

            webHost.Run();
        }
    }
}
