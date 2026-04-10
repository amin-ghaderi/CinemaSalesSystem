using CinemaSales.Infrastructure.Persistence.Context;

using Microsoft.EntityFrameworkCore;

namespace CinemaSalesSystem.Infrastructure.Tests.Common;

public static class TestDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
