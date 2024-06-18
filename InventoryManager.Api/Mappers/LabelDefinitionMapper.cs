using InventoryManager.Domain;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class LabelDefinitionMapper
{
    public static partial IQueryable<LabelDefinitionDto> ProjectToDto(this IQueryable<LabelDefinition> q);

    public static partial LabelDefinitionDto ToDto(this LabelDefinition d);
}