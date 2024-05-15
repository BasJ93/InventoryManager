using InventoryManager.Domain.Enums;

namespace InventoryManager.Models;

public class CreateContentRequestDto
{
    public ContentType Type { get; set; }

    /// <summary>
    /// The standard use to define this item. i.e. ISO/DIN
    /// </summary>
    public Guid StandardId { get; set; } = Guid.Empty;

    public string Size { get; set; } = string.Empty;

    public string Length { get; set; } = string.Empty;
}