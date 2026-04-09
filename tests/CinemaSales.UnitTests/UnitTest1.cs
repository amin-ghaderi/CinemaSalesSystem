using CinemaSales.Domain.Entities;

namespace CinemaSales.UnitTests;

public class UnitTest1
{
    private sealed class TestEntity : BaseEntity
    {
    }

    [Fact]
    public void BaseEntity_HasNonEmptyId()
    {
        var entity = new TestEntity();
        Assert.NotEqual(Guid.Empty, entity.Id);
    }
}
