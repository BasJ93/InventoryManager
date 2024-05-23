namespace InventoryManager.Models;

public class CreateStorageLocationRequestDto
{
    public string Name { get; set; } = string.Empty;

    public int SizeX { get; set; }

    public int SizeY { get; set; }
}