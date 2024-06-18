using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface ILabelDefinitionService
{
    ICollection<LabelTypeDto> GetLabelTypes();

    Task<LabelDefinitionDto?> GetLabelDefinition(Guid id, CancellationToken ctx = default);
}