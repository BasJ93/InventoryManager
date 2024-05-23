namespace InventoryManager.Domain;

public class StorageLocationContainerPosition
{
    public Guid StorageLocationId { get; set; }

    public Guid ContainerId { get; set; }

    public int PositionX { get; set; }

    public int PositionY { get; set; }

    public StorageLocation Location { get; set; } = null!;

    public Container Container { get; set; } = null!;
}