using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Infrastructure.Persistence.Context;
using CinemaSales.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSales.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CinemaSales"));

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ISnackRepository, SnackRepository>();
        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<IOrderSnackLineRepository, OrderSnackLineRepository>();

        return services;
    }
}
