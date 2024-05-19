namespace InventoryManager.Models;

public class GetStorageCaseResponseDto
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public int SizeX { get; set; }

    public int SizeY { get; set; }

    public List<ContainerWithLocationResponseDto> Containers { get; set; } = new();
}