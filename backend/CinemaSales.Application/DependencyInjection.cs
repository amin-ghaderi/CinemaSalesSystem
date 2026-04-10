using CinemaSales.Application.Abstractions.Services;
using CinemaSales.Application.Services;
using CinemaSales.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSales.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Application layer services and use cases.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieApplicationService>();
        services.AddScoped<IShowTimeService, ShowTimeApplicationService>();
        services.AddScoped<ITicketService, TicketApplicationService>();
        services.AddScoped<ISnackService, SnackApplicationService>();
        services.AddScoped<ICampaignService, CampaignApplicationService>();
        services.AddScoped<ISalesReportService, SalesReportApplicationService>();

        // Use Cases
        services.AddScoped<GetAllMoviesUseCase>();
        services.AddScoped<GetMovieByIdUseCase>();
        services.AddScoped<GetAllShowTimesUseCase>();
        services.AddScoped<GetShowTimesByMovieIdUseCase>();
        services.AddScoped<GetShowTimesByCinemaIdUseCase>();
        services.AddScoped<PurchaseTicketUseCase>();
        services.AddScoped<PurchaseTicketsForShowTimeUseCase>();
        services.AddScoped<GetAllSnacksUseCase>();
        services.AddScoped<PurchaseSnackUseCase>();
        services.AddScoped<GetAllCampaignsUseCase>();

        return services;
    }
}
