using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface IStorageLocationService
{
    Task<Guid> CreateStorageLocation(CreateStorageLocationRequestDto requestDto, CancellationToken ctx = default);
    
    Task<List<GetStorageLocationsResponseDto>> GetStorageLocations(CancellationToken ctx = default);
    
    Task<GetStorageLocationResponseDto?> GetStorageLocation(Guid id, CancellationToken ctx = default);

    Task<bool> PlaceContainerInStorageLocation(Guid id, int x, int y, Guid containerId, CancellationToken ctx = default);
    
    Task<bool> RemoveContainerFromStorageLocation(Guid id, int x, int y, CancellationToken ctx = default);

    Task<(MemoryStream?, string?)> GenerateLabelsPdf(Guid id, CancellationToken ctx = default);

    Task<(MemoryStream?, string?)> GenerateLidPdf(Guid id, CancellationToken ctx = default);

    Task<string> GetStorageLocationName(Guid id, CancellationToken ctx = default);
}