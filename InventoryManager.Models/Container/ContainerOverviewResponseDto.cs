namespace InventoryManager.Models;

public class ContainerOverviewResponseDto : ContainerResponseDto
{
    public GetStorageLocationsResponseDto? Location { get; set; }
    
    public ContentResponseDto? Content { get; set; }
}