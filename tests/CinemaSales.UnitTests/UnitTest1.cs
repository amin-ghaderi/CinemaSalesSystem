using CinemaSales.Domain.Common;
using CinemaSales.Domain.ValueObjects;

namespace CinemaSales.UnitTests;

public class UnitTest1
{
    private sealed class TestEntity : Entity
    {
    }

    [Fact]
    public void EntityHasNonEmptyId()
    {
        var entity = new TestEntity();
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    [Fact]
    public void MoneyRejectsNegativeAmount()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1, "EUR"));
    }
}
