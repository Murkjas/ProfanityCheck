using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        // Change the Main method to return a task asynchronously, so that we can run our data seed with await
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // use 'using' so scope doesn't get stored
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try {
                // Get this as a service, because we've added this as a service in Startup.cs
                var context = services.GetRequiredService<DataContext>();
                // Run our migration and create a database if we don't already have it
                await context.Database.MigrateAsync();
                await SeedWords.SeedData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration.");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
