using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface IContainerService
{
    Task<List<ContainerOverviewResponseDto>> GetAllContainers(CancellationToken ctx = default);

    List<ContainerSizeDto> GetContainerSizes();
    
    Task<List<ContainerWithLocationResponseDto>> GetUnplacedContainers(CancellationToken ctx = default);

    Task<ContainerResponseDto?> GetContainer(Guid id, CancellationToken ctx = default);

    Task<Guid> CreateContainer(CreateContainerRequestDto requestDto, CancellationToken ctx = default);
}