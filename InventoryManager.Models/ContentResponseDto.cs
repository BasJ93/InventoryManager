namespace InventoryManager.Models;

public class ContentResponseDto
{
    public Guid Id { get; set; }

    public string Standard { get; set; } = string.Empty;

    public string Definition { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}