namespace InventoryManager.Models;

public class StandardResponseDto
{
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// The name of the standard.
    /// </summary>
    /// <example>ISO 4032</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the standard.
    /// </summary>
    /// <example>Hexagon Nuts</example>
    public string? Description { get; set; }
}