using CinemaSales.Application.DependencyInjection;
using CinemaSales.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

Console.WriteLine("Cinema Sales System Initialized Successfully!");

await app.RunAsync();
