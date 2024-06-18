namespace InventoryManager.Models;

public class ContainerWithLocationResponseDto : ContainerResponseDto
{
    public int PositionX { get; set; }

    public int PositionY { get; set; }
    
    public ContentResponseDto? Content { get; set; }
}