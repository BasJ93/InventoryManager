namespace InventoryManager.Models;

public class ContainerOverviewResponseDto : ContainerResponseDto
{
    public GetStorageCasesResponseDto? Location { get; set; }
    
    public ContentResponseDto? Content { get; set; }
}