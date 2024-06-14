using InventoryManager.Domain.Enums;

namespace InventoryManager.Domain;

/// <summary>
/// Class defining a label for printing on a label printer.
/// </summary>
public class LabelDefinition
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public LabelType Type { get; set; }
    
    /// <summary>
    /// The command text to print the label, for example in ZPL for Zebra printers.
    /// Can contain tokens that the processor will replace.
    /// </summary>
    public string CommandText { get; set; } = string.Empty;
}