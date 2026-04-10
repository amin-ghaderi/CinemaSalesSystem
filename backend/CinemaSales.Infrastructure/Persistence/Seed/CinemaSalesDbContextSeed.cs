using CinemaSales.Domain.Aggregates.Movies;
using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;
using CinemaSales.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CinemaSales.Infrastructure.Persistence.Seed;

/// <summary>
/// Workshop seed (May 2026, CEST). <see cref="ShowTime.Auditorium"/> = <see cref="WorkshopCinemaId"/>.ToString().
/// Ticket list prices from the PDF (105/130 SEK by slot, child/senior tiers) are not persisted as catalog rows in this model.
/// </summary>
public static class CinemaSalesDbContextSeed
{
    public static readonly Guid WorkshopCinemaId = Guid.Parse("6f2c8e9a-4b1d-4f0e-9c7a-2d8e1f3a5b6c");

    private static readonly TimeSpan WorkshopTimeZoneOffset = TimeSpan.FromHours(2);

    public static async Task SeedAsync(
        ApplicationDbContext context,
        CancellationToken cancellationToken = default)
    {
        await context.Database.EnsureCreatedAsync(cancellationToken);

        if (await context.Movies.AnyAsync(cancellationToken))
        {
            return;
        }

        var auditorium = WorkshopCinemaId.ToString();

        // May 2026 calendar anchor: 1 May = Friday → Tue 5, Wed 6, Thu 7, Fri 8, Sat 9.
        static DateTimeOffset At(int year, int month, int day, int hour, int minute) =>
            new(year, month, day, hour, minute, 0, WorkshopTimeZoneOffset);

        // --- Movies (title, duration minutes, rating) per workshop sheet ---
        var furiosa = new Movie("Furiosa: A Mad Max Saga (2024)", 148, MovieRating.R, genre: "Action");
        furiosa.ScheduleShowTime(At(2026, 5, 5, 18, 0), auditorium, ShowTimeSlot.Evening);
        furiosa.ScheduleShowTime(At(2026, 5, 9, 21, 0), auditorium, ShowTimeSlot.Night);

        var dogDay = new Movie("Dog Day Afternoon (1975)", 125, MovieRating.R, genre: "Crime");
        dogDay.ScheduleShowTime(At(2026, 5, 7, 21, 0), auditorium, ShowTimeSlot.Night);

        var fallGuy = new Movie("The Fall Guy (2024)", 126, MovieRating.Pg13, genre: "Action");
        fallGuy.ScheduleShowTime(At(2026, 5, 5, 21, 0), auditorium, ShowTimeSlot.Night);
        fallGuy.ScheduleShowTime(At(2026, 5, 8, 21, 0), auditorium, ShowTimeSlot.Night);

        var ironMan = new Movie("Iron Man 3 (2013)", 130, MovieRating.Pg13, genre: "Action");
        ironMan.ScheduleShowTime(At(2026, 5, 6, 18, 0), auditorium, ShowTimeSlot.Evening);
        ironMan.ScheduleShowTime(At(2026, 5, 9, 13, 0), auditorium, ShowTimeSlot.Afternoon);

        var civilWar = new Movie("Civil War (2024)", 109, MovieRating.R, genre: "Action");
        civilWar.ScheduleShowTime(At(2026, 5, 8, 18, 0), auditorium, ShowTimeSlot.Evening);
        civilWar.ScheduleShowTime(At(2026, 5, 9, 18, 0), auditorium, ShowTimeSlot.Evening);

        var roomNextDoor = new Movie("The Room Next Door (2024)", 107, MovieRating.Pg13, genre: "Drama");
        roomNextDoor.ScheduleShowTime(At(2026, 5, 6, 21, 0), auditorium, ShowTimeSlot.Night);
        roomNextDoor.ScheduleShowTime(At(2026, 5, 7, 18, 0), auditorium, ShowTimeSlot.Evening);

        await context.Movies.AddRangeAsync(
            new Movie[]
            {
                furiosa,
                dogDay,
                fallGuy,
                ironMan,
                civilWar,
                roomNextDoor,
            },
            cancellationToken);

        // Snacks: net SEK; workshop snack VAT 12% → VatType.Reduced (rate resolved by IVatCalculationService / config).
        await context.Snacks.AddRangeAsync(
            new Snack[]
            {
                new("Ahlgrens bilar (125 g)", new Money(22m, "SEK"), VatType.Reduced),
                new("Popcorn and Coca-Cola", new Money(43m, "SEK"), VatType.Reduced),
            },
            cancellationToken);

        // Campaign MAY26: 50% discount; valid through end of May 2026 (UTC). Applies to 18:00 sessions per workshop rules (application/domain).
        await context.Campaigns.AddAsync(
            new Campaign(
                "MAY26 — May 2026 (18:00 sessions)",
                50m,
                "MAY26",
                new DateTimeOffset(2026, 5, 31, 23, 59, 59, TimeSpan.Zero)),
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        // 54 seats per show time, seat numbers 1–54 (single row "A" per workshop numbering).
        var showTimeIds = await context.ShowTimes
            .AsNoTracking()
            .Select(st => st.Id)
            .ToListAsync(cancellationToken);

        const int seatsPerShowTime = 54;
        foreach (var showTimeId in showTimeIds)
        {
            for (var seatNumber = 1; seatNumber <= seatsPerShowTime; seatNumber++)
            {
                await context.Seats.AddAsync(
                    new Seat(showTimeId, new SeatNumber("A", seatNumber)),
                    cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
