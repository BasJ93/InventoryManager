namespace InventoryManager.Models;

public class LabelDefinitionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string CommandText { get; set; } = string.Empty;

    public int Type { get; set; }
}