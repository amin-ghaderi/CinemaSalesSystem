using System.Globalization;

using CinemaSales.Application;
using CinemaSales.ConsoleUI.Services;
using CinemaSales.Infrastructure;
using CinemaSales.Infrastructure.Persistence.Context;
using CinemaSales.Infrastructure.Persistence.Seed;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace CinemaSales.ConsoleUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting CinemaSalesSystem...");

            var builder = Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext())
                .ConfigureServices((context, services) =>
                {
                    services.AddApplication();
                    services.AddInfrastructure(context.Configuration);

                    services.AddScoped<MovieService>();
                    services.AddScoped<ShowTimeService>();
                    services.AddScoped<TicketService>();
                    services.AddScoped<SnackService>();
                    services.AddScoped<CampaignService>();
                    services.AddScoped<SalesReportService>();

                    services.AddHostedService<ConsoleApp>();
                });

            var host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Database.IsRelational())
                {
                    await dbContext.Database.MigrateAsync(CancellationToken.None);
                }

                await CinemaSalesDbContextSeed.SeedAsync(dbContext, CancellationToken.None);
            }

            Log.Information("CinemaSalesSystem started successfully.");

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The application terminated unexpectedly.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
