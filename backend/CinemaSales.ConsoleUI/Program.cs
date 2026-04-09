using CinemaSales.Application;
using CinemaSales.Infrastructure;
using CinemaSales.Infrastructure.Persistence.Context;
using CinemaSales.Infrastructure.Persistence.Seed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await CinemaSalesDbContextSeed.SeedAsync(
        context,
        CancellationToken.None);
}

Console.WriteLine("Cinema Sales System is ready.");
Console.ReadLine();
