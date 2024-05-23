namespace InventoryManager.Models;

public class GetStorageLocationResponseDto : GetStorageLocationsResponseDto
{
    public int SizeX { get; set; }

    public int SizeY { get; set; }

    public List<ContainerWithLocationResponseDto> Containers { get; set; } = new();
}