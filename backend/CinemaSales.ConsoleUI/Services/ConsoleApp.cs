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
    private readonly MainMenu _mainMenu;
    private readonly MovieService _movieService;
    private readonly MovieMenu _movieMenu;
    private readonly ShowTimeService _showTimeService;
    private readonly ShowTimeMenu _showTimeMenu;
    private readonly TicketService _ticketService;
    private readonly TicketMenu _ticketMenu;
    private readonly SnackService _snackService;
    private readonly SnackMenu _snackMenu;
    private readonly CampaignService _campaignService;
    private readonly CampaignMenu _campaignMenu;
    private readonly SalesReportService _salesReportService;
    private readonly SalesReportMenu _salesReportMenu;

    public ConsoleApp(
        ILogger<ConsoleApp> logger,
        IHostApplicationLifetime lifetime,
        MainMenu mainMenu,
        MovieService movieService,
        MovieMenu movieMenu,
        ShowTimeService showTimeService,
        ShowTimeMenu showTimeMenu,
        TicketService ticketService,
        TicketMenu ticketMenu,
        SnackService snackService,
        SnackMenu snackMenu,
        CampaignService campaignService,
        CampaignMenu campaignMenu,
        SalesReportService salesReportService,
        SalesReportMenu salesReportMenu)
    {
        _logger = logger;
        _lifetime = lifetime;
        _mainMenu = mainMenu;
        _movieService = movieService;
        _movieMenu = movieMenu;
        _showTimeService = showTimeService;
        _showTimeMenu = showTimeMenu;
        _ticketService = ticketService;
        _ticketMenu = ticketMenu;
        _snackService = snackService;
        _snackMenu = snackMenu;
        _campaignService = campaignService;
        _campaignMenu = campaignMenu;
        _salesReportService = salesReportService;
        _salesReportMenu = salesReportMenu;
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
            _mainMenu.Display();
            var choice = _mainMenu.GetUserChoice();

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
        _movieMenu.DisplayMovies(movies);
    }

    private async Task ShowShowTimesAsync()
    {
        var showTimes = await _showTimeService.GetShowTimesAsync();
        _showTimeMenu.DisplayShowTimes(showTimes);
    }

    private async Task PurchaseTicketAsync()
    {
        var showTimes = await _showTimeService.GetShowTimesAsync();
        var showTimeId = _ticketMenu.PromptShowTimeSelection(showTimes);

        if (showTimeId == Guid.Empty)
            return;

        var quantity = _ticketMenu.PromptTicketQuantity();

        try
        {
            var result = await _ticketService.PurchaseTicketAsync(showTimeId, quantity);
            _ticketMenu.DisplayPurchaseResult(result);
        }
        catch (Exception ex)
        {
            _ticketMenu.DisplayError(ex.Message);
        }
    }

    private async Task PurchaseSnackAsync()
    {
        var snacks = await _snackService.GetSnacksAsync();
        var snackId = _snackMenu.PromptSnackSelection(snacks);

        if (snackId == Guid.Empty)
            return;

        var quantity = _snackMenu.PromptQuantity();

        try
        {
            var result = await _snackService.PurchaseSnackAsync(snackId, quantity);
            _snackMenu.DisplayPurchaseResult(
                result.Name,
                result.Quantity,
                result.UnitPrice,
                result.TotalPrice);
        }
        catch (Exception ex)
        {
            _snackMenu.DisplayError(ex.Message);
        }
    }

    private async Task ShowCampaignsAsync()
    {
        var campaigns = await _campaignService.GetCampaignsAsync();
        _campaignMenu.DisplayCampaigns(campaigns);
    }

    private async Task ShowSalesReportAsync()
    {
        var report = await _salesReportService.GetSalesReportAsync();
        _salesReportMenu.DisplayReport(report);
    }

    private void ShowPlaceholder(string moduleName)
    {
        ConsoleHelper.PrintHeader(moduleName);
        Console.WriteLine("This module will be implemented in the next stages.");
        InputHelper.Pause();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("CinemaSales Console UI is shutting down.");
        return Task.CompletedTask;
    }
}
