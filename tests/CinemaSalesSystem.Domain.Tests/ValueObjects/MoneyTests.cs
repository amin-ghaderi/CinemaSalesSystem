using CinemaSales.Domain.ValueObjects;

using FluentAssertions;

namespace CinemaSalesSystem.Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void ConstructorShouldNormalizeCurrencyAndRoundAmount()
    {
        var money = new Money(10.126m, "sek");

        money.Amount.Should().Be(10.13m);
        money.Currency.Should().Be("SEK");
    }

    [Fact]
    public void AddShouldSumWhenCurrencyMatches()
    {
        var a = new Money(10m, "SEK");
        var b = new Money(5.5m, "SEK");

        var sum = a + b;

        sum.Amount.Should().Be(15.5m);
        sum.Currency.Should().Be("SEK");
    }

    [Fact]
    public void ConstructorShouldRejectNegativeAmount()
    {
        var act = () => new Money(-1m, "SEK");

        act.Should().Throw<ArgumentException>()
            .WithParameterName("amount");
    }
}
