namespace InventoryManager.Domain;

public class CaseContainerPosition
{
    public Guid CaseId { get; set; }

    public Guid ContainerId { get; set; }

    public int PositionX { get; set; }

    public int PositionY { get; set; }

    public StorageCase Case { get; set; } = null!;

    public Container Container { get; set; } = null!;
}