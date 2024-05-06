using InventoryManager.Domain.Enums;

namespace InventoryManager.Domain;

public class Content
{
    public Guid Id { get; set; }

    public ContentType Type { get; set; }

    /// <summary>
    /// The standard use to define this item. i.e. ISO/DIN
    /// </summary>
    public string Standard { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Length { get; set; } = string.Empty;

    public virtual ICollection<Container> Containers { get; set; } = new HashSet<Container>();

    /// <summary>
    /// Return the screw designation for this item.
    /// </summary>
    public string Screw => $"{Size}x{Length}";
}