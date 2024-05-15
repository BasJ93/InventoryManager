namespace InventoryManager.Domain;

/// <summary>
/// A representation of a standard in the database.
/// </summary>
/// <example>ISO 4032</example>
public class Standard
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

    /// <summary>
    /// Path to an image of the standard (preferably in svg format).
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Collection of alternative names for the standard.
    /// </summary>
    /// <example>DIN 934</example>
    public List<string> AlternativeNames { get; set; } = new();

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
}