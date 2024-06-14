using InventoryManager.Domain;
using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface IStorageLocationService
{
    Task<Guid> CreateStorageLocation(CreateStorageLocationRequestDto requestDto, CancellationToken ctx = default);
    
    Task<List<GetStorageLocationsResponseDto>> GetStorageLocations(CancellationToken ctx = default);
    
    Task<GetStorageLocationResponseDto?> GetStorageLocation(Guid id, CancellationToken ctx = default);

    List<StorageLocationTypeDto> GetStorageLocationTypes();
    
    Task<bool> PlaceContainerInStorageLocation(Guid id, int x, int y, Guid containerId, CancellationToken ctx = default);
    
    Task<bool> RemoveContainerFromStorageLocation(Guid id, int x, int y, CancellationToken ctx = default);

    Task<(MemoryStream?, string?)> GenerateLabelsPdf(Guid id, CancellationToken ctx = default);

    Task<(MemoryStream?, string?)> GenerateLidPdf(Guid id, CancellationToken ctx = default);

    Task<string> GetStorageLocationName(Guid id, CancellationToken ctx = default);

    /// <summary>
    /// Print the labels for the <see cref="Container"/>s in the <see cref="StorageLocation"/> on a label printer.
    /// </summary>
    /// <param name="id">Id of the <see cref="StorageLocation"/>.</param>
    /// <param name="ctx">Cancellation token.</param>
    Task<bool> PrintLabelsOnLabelPrinter(Guid id, CancellationToken ctx = default);
}