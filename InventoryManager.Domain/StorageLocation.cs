namespace InventoryManager.Domain;

public class StorageLocation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public int SizeX { get; set; }

    public int SizeY { get; set; }

    public virtual ICollection<StorageLocationContainerPosition> Containers { get; set; } = new HashSet<StorageLocationContainerPosition>();
}