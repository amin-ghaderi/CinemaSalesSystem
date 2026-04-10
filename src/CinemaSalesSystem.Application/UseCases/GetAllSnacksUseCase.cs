using CinemaSales.Application.Abstractions.Persistence;
using CinemaSales.Application.DTOs;
using CinemaSales.Application.Mappings;
using CinemaSales.Domain.Entities;

namespace CinemaSales.Application.UseCases;

public sealed class GetAllSnacksUseCase
{
    private readonly ISnackRepository _snackRepository;

    public GetAllSnacksUseCase(ISnackRepository snackRepository)
    {
        _snackRepository = snackRepository;
    }

    public async Task<IReadOnlyList<SnackDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Snack> snacks = await _snackRepository.GetAllAsync(cancellationToken);
        return snacks.Select(SnackMapper.ToDto).ToList();
    }
}
