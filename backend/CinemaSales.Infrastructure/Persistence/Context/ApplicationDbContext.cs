using System.Reflection;
using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Aggregates.Orders;
using CinemaSales.Domain.Common;
using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();

    public DbSet<ShowTime> ShowTimes => Set<ShowTime>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Ticket> Tickets => Set<Ticket>();

    public DbSet<OrderSnackLine> OrderSnackLines => Set<OrderSnackLine>();

    public DbSet<Seat> Seats => Set<Seat>();

    public DbSet<Snack> Snacks => Set<Snack>();

    public DbSet<Campaign> Campaigns => Set<Campaign>();

    public DbSet<SalesReport> SalesReports => Set<SalesReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<BaseDomainEvent>();
        modelBuilder.Ignore<DiscountAppliedEvent>();
        modelBuilder.Ignore<OrderCreatedEvent>();
        modelBuilder.Ignore<SeatReservedEvent>();
        modelBuilder.Ignore<TicketReservedEvent>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
