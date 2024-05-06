namespace InventoryManager.Models;

public class CreateStorageCaseRequestDto
{
    public string Name { get; set; } = string.Empty;

    public int SizeX { get; set; }

    public int SizeY { get; set; }
}