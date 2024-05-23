using InventoryManager.Domain.Enums;

namespace InventoryManager.Domain;

public class StorageLocation
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;
    
    public int SizeX { get; set; }

    public int SizeY { get; set; }

    public StorageLocationType Type { get; set; }
    
    public virtual ICollection<StorageLocationContainerPosition> Containers { get; set; } = new HashSet<StorageLocationContainerPosition>();
}