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
        // Use Cases
        services.AddScoped<GetAllMoviesUseCase>();
        services.AddScoped<GetMovieByIdUseCase>();
        services.AddScoped<GetShowTimesByMovieIdUseCase>();
        services.AddScoped<PurchaseTicketUseCase>();

        return services;
    }
}
