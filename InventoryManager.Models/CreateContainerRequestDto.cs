using InventoryManager.Domain.Enums;

namespace InventoryManager.Models;

public class CreateContainerRequestDto
{
    public Guid ContentId { get; set; } = Guid.Empty;
    
    public ContainerSize Size { get; set; }
}