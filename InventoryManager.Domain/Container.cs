using InventoryManager.Domain.Enums;

namespace InventoryManager.Domain;

public class Container
{
    public Guid Id { get; set; } = Guid.Empty;

    public ContainerSize Size { get; set; } = ContainerSize.Undefined;

    public Guid? ContentId { get; set; } = Guid.Empty;
    
    public virtual StorageLocationContainerPosition? Position { get; set; } = null!;

    public virtual Content? Content { get; set; } = null!;
    
    public uint Width() {
        switch (Size)
        {
            case ContainerSize.Size1X2:
            case ContainerSize.Size1X1:
            case ContainerSize.Undefined:
            default:
                return 1;
        }
    }
    
    public uint Height() {
        switch (Size)
        {
            case ContainerSize.Size1X2:
                return 2;
            case ContainerSize.Size1X1:
            case ContainerSize.Undefined:
            default:
                return 1;
        }
    }
}