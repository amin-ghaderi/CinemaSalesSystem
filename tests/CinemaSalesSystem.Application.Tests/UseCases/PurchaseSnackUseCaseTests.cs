using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.UseCases;
using CinemaSales.Domain.Entities;
using CinemaSales.Domain.Enums;
using CinemaSales.Domain.ValueObjects;

using FluentAssertions;

using Moq;

namespace CinemaSalesSystem.Application.Tests.UseCases;

public class PurchaseSnackUseCaseTests
{
    private readonly Mock<ISnackRepository> _snackRepositoryMock = new();

    [Fact]
    public async Task ExecuteAsyncShouldReturnLineTotalsWhenSnackExists()
    {
        var snackId = Guid.NewGuid();
        var snack = new Snack(snackId, "Popcorn", new Money(5.5m, "SEK"), VatType.Reduced);
        _snackRepositoryMock
            .Setup(r => r.GetByIdAsync(snackId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(snack);

        var sut = new PurchaseSnackUseCase(_snackRepositoryMock.Object);

        SnackPurchaseResultDto result = await sut.ExecuteAsync(snackId, 3, CancellationToken.None);

        result.SnackName.Should().Be("Popcorn");
        result.Quantity.Should().Be(3);
        result.UnitPrice.Should().Be(5.5m);
        result.TotalPrice.Should().Be(16.5m);
        _snackRepositoryMock.Verify(r => r.GetByIdAsync(snackId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsyncShouldThrowWhenSnackMissing()
    {
        _snackRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Snack?)null);

        var sut = new PurchaseSnackUseCase(_snackRepositoryMock.Object);

        var act = async () => await sut.ExecuteAsync(Guid.NewGuid(), 1, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Snack was not found.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public async Task ExecuteAsyncShouldRejectNonPositiveQuantity(int quantity)
    {
        var sut = new PurchaseSnackUseCase(_snackRepositoryMock.Object);

        var act = async () => await sut.ExecuteAsync(Guid.NewGuid(), quantity, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(quantity));
    }
}
