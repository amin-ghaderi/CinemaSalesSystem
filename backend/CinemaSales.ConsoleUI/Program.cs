using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CinemaSales.Application;
using CinemaSales.ConsoleUI.Menus;
using CinemaSales.ConsoleUI.Services;
using CinemaSales.Infrastructure;
using CinemaSales.Infrastructure.Persistence.Context;
using CinemaSales.Infrastructure.Persistence.Seed;

namespace CinemaSales.ConsoleUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddApplication();
                services.AddInfrastructure();

                services.AddSingleton<MainMenu>();
                services.AddSingleton<MovieMenu>();
                services.AddSingleton<ShowTimeMenu>();
                services.AddSingleton<TicketMenu>();
                services.AddSingleton<SnackMenu>();
                services.AddSingleton<CampaignMenu>();
                services.AddSingleton<SalesReportMenu>();

                services.AddScoped<MovieService>();
                services.AddScoped<ShowTimeService>();
                services.AddScoped<TicketService>();
                services.AddScoped<SnackService>();
                services.AddScoped<CampaignService>();
                services.AddScoped<SalesReportService>();

                services.AddHostedService<ConsoleApp>();
            })
            .Build();

        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await CinemaSalesDbContextSeed.SeedAsync(dbContext, CancellationToken.None);
        }

        await host.RunAsync();
    }
}
