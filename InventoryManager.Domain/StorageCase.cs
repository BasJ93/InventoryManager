namespace InventoryManager.Domain;

public class StorageCase
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public int SizeX { get; set; }

    public int SizeY { get; set; }

    public virtual ICollection<CaseContainerPosition> Containers { get; set; } = new HashSet<CaseContainerPosition>();
}