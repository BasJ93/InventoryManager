namespace InventoryManager.Models;

public class GetStorageCasesResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;
}