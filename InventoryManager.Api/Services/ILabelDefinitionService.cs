using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface ILabelDefinitionService
{
    ICollection<LabelTypeDto> GetLabelTypes();

    /// <summary>
    /// Get a list of all label definitions.
    /// </summary>
    /// <remarks>The objects are not tracked by the ORM.</remarks>
    Task<ICollection<LabelDefinitionDto>> GetLabelDefinitions(CancellationToken ctx = default);

    Task<LabelDefinitionDto?> GetLabelDefinition(Guid id, CancellationToken ctx = default);
    
    Task<LabelDefinitionDto?> SetLabelDefinition(Guid id, LabelDefinitionDto definition, CancellationToken ctx = default);
}