namespace InventoryManager.Models;

public class ContainerWithLocationResponseDto : ContainerResponseDto
{
    public int PositionX { get; set; }

    public int PositionY { get; set; }
    
    public ContentReponseDto? Content { get; set; }
}