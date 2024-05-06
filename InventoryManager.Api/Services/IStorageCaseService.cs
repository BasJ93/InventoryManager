using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface IStorageCaseService
{
    Task<Guid> CreateStorageCase(CreateStorageCaseRequestDto requestDto, CancellationToken ctx = default);
    
    Task<List<GetStorageCasesResponseDto>> GetStorageCases(CancellationToken ctx = default);
    
    Task<GetStorageCaseResponseDto?> GetStorageCase(Guid id, CancellationToken ctx = default);

    Task<bool> PlaceContainerInStorageCase(Guid id, int x, int y, Guid containerId, CancellationToken ctx = default);
    
    Task<bool> RemoveContainerFromStorageCase(Guid id, int x, int y, CancellationToken ctx = default);

    Task<(MemoryStream?, string?)> GenerateLidPdf(Guid id, CancellationToken ctx = default);

    Task<string> GetStorageCaseName(Guid id, CancellationToken ctx = default);
}