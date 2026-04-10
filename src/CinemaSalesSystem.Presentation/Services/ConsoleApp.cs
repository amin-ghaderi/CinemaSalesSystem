using CinemaSales.ConsoleUI.Constants;
using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Menus;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CinemaSales.ConsoleUI.Services;

public class ConsoleApp : IHostedService
{
    private readonly ILogger<ConsoleApp> _logger;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly MovieService _movieService;
    private readonly ShowTimeService _showTimeService;
    private readonly TicketService _ticketService;
    private readonly SnackService _snackService;
    private readonly CampaignService _campaignService;
    private readonly SalesReportService _salesReportService;

    public ConsoleApp(
        ILogger<ConsoleApp> logger,
        IHostApplicationLifetime lifetime,
        MovieService movieService,
        ShowTimeService showTimeService,
        TicketService ticketService,
        SnackService snackService,
        CampaignService campaignService,
        SalesReportService salesReportService)
    {
        _logger = logger;
        _lifetime = lifetime;
        _movieService = movieService;
        _showTimeService = showTimeService;
        _ticketService = ticketService;
        _snackService = snackService;
        _campaignService = campaignService;
        _salesReportService = salesReportService;
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
        var movies = await _movieService.GetMoviesAsync();
        MovieMenu.DisplayMovies(movies);
    }

    private async Task ShowShowTimesAsync()
    {
        var showTimes = await _showTimeService.GetShowTimesAsync();
        ShowTimeMenu.DisplayShowTimes(showTimes);
    }

    private async Task PurchaseTicketAsync()
    {
        var showTimes = await _showTimeService.GetShowTimesAsync();
        var showTimeId = TicketMenu.PromptShowTimeSelection(showTimes);

        if (showTimeId == Guid.Empty)
        {
            return;
        }

        var quantity = TicketMenu.PromptTicketQuantity();

        try
        {
            var result = await _ticketService.PurchaseTicketAsync(showTimeId, quantity);
            TicketMenu.DisplayPurchaseResult(result);
        }
        catch (Exception ex)
        {
            TicketMenu.DisplayError(ex.Message);
        }
    }

    private async Task PurchaseSnackAsync()
    {
        var snacks = await _snackService.GetSnacksAsync();
        var snackId = SnackMenu.PromptSnackSelection(snacks);

        if (snackId == Guid.Empty)
        {
            return;
        }

        var quantity = SnackMenu.PromptQuantity();

        try
        {
            var (name, purchasedQuantity, unitPrice, totalPrice) = await _snackService.PurchaseSnackAsync(snackId, quantity);
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
        var campaigns = await _campaignService.GetCampaignsAsync();
        CampaignMenu.DisplayCampaigns(campaigns);
    }

    private async Task ShowSalesReportAsync()
    {
        var report = await _salesReportService.GetSalesReportAsync();
        SalesReportMenu.DisplayReport(report);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("CinemaSales Console UI is shutting down.");
        return Task.CompletedTask;
    }
}
