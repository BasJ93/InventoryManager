namespace InventoryManager.Models;

public class CreateStandardRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<string> AlternativeNames { get; set; } = new();
    
    // TODO: Add the option to upload a file for the optional image.
}