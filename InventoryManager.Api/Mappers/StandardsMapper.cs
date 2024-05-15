using InventoryManager.Domain;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class StandardsMapper
{
    public static partial IQueryable<StandardResponseDto> ProjectToDto(this IQueryable<Standard> s);
}