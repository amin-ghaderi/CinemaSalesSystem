using CinemaSales.ConsoleUI.Constants;
using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Menus;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CinemaSales.ConsoleUI.Services;

public class ConsoleApp : IHostedService
{
    private readonly ILogger<ConsoleApp> _logger;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IServiceScopeFactory _scopeFactory;

    public ConsoleApp(
        ILogger<ConsoleApp> logger,
        IHostApplicationLifetime lifetime,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _lifetime = lifetime;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("CinemaSales Console UI started.");
        RunMenu();
        return Task.CompletedTask;
    }

    private void RunMenu()
    {
        bool exit = false;

        while (!exit)
        {
            MainMenu.Display();
            var choice = MainMenu.GetUserChoice();

            switch (choice)
            {
                case MenuOptions.Movies:
                    ShowMoviesAsync().GetAwaiter().GetResult();
                    break;

                case MenuOptions.ShowTimes:
                    ShowShowTimesAsync().GetAwaiter().GetResult();
                    break;

                case MenuOptions.PurchaseTicket:
                    PurchaseTicketAsync().GetAwaiter().GetResult();
                    break;

                case MenuOptions.PurchaseSnack:
                    PurchaseSnackAsync().GetAwaiter().GetResult();
                    break;

                case MenuOptions.Campaigns:
                    ShowCampaignsAsync().GetAwaiter().GetResult();
                    break;

                case MenuOptions.Reports:
                    ShowSalesReportAsync().GetAwaiter().GetResult();
                    break;

                case MenuOptions.Exit:
                    exit = true;
                    _lifetime.StopApplication();
                    break;

                default:
                    ConsoleHelper.PrintError("Invalid selection. Please try again.");
                    InputHelper.Pause();
                    break;
            }
        }
    }

    private async Task ShowMoviesAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var movieService = scope.ServiceProvider.GetRequiredService<MovieService>();
        var movies = await movieService.GetMoviesAsync();
        MovieMenu.DisplayMovies(movies);
    }

    private async Task ShowShowTimesAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var showTimeService = scope.ServiceProvider.GetRequiredService<ShowTimeService>();
        var showTimes = await showTimeService.GetShowTimesAsync();
        ShowTimeMenu.DisplayShowTimes(showTimes);
    }

    private async Task PurchaseTicketAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var sp = scope.ServiceProvider;
        var showTimeService = sp.GetRequiredService<ShowTimeService>();
        var ticketService = sp.GetRequiredService<TicketService>();

        var showTimes = await showTimeService.GetShowTimesAsync();
        var showTimeId = TicketMenu.PromptShowTimeSelection(showTimes);

        if (showTimeId == Guid.Empty)
        {
            return;
        }

        var quantity = TicketMenu.PromptTicketQuantity();

        try
        {
            var result = await ticketService.PurchaseTicketAsync(showTimeId, quantity);
            TicketMenu.DisplayPurchaseResult(result);
        }
        catch (Exception ex)
        {
            TicketMenu.DisplayError(ex.Message);
        }
    }

    private async Task PurchaseSnackAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var sp = scope.ServiceProvider;
        var snackService = sp.GetRequiredService<SnackService>();

        var snacks = await snackService.GetSnacksAsync();
        var snackId = SnackMenu.PromptSnackSelection(snacks);

        if (snackId == Guid.Empty)
        {
            return;
        }

        var quantity = SnackMenu.PromptQuantity();

        try
        {
            var (name, purchasedQuantity, unitPrice, totalPrice) = await snackService.PurchaseSnackAsync(snackId, quantity);
            SnackMenu.DisplayPurchaseResult(
                name,
                purchasedQuantity,
                unitPrice,
                totalPrice);
        }
        catch (Exception ex)
        {
            SnackMenu.DisplayError(ex.Message);
        }
    }

    private async Task ShowCampaignsAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var campaignService = scope.ServiceProvider.GetRequiredService<CampaignService>();
        var campaigns = await campaignService.GetCampaignsAsync();
        CampaignMenu.DisplayCampaigns(campaigns);
    }

    private async Task ShowSalesReportAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
        var report = await salesReportService.GetSalesReportAsync();
        SalesReportMenu.DisplayReport(report);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("CinemaSales Console UI is shutting down.");
        return Task.CompletedTask;
    }
}
