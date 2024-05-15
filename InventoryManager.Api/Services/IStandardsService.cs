using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface IStandardsService
{
    Task<List<StandardResponseDto>> GetAllStandards(CancellationToken ctx = default);

    Task<Guid> CreateStandard(CreateStandardRequestDto requestDto, CancellationToken ctx = default);
}