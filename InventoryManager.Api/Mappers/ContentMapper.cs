using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class ContentMapper
{
    [MapProperty("Standard.Name", nameof(ContentResponseDto.Standard))]
    [MapProperty("Standard.Description", nameof(ContentResponseDto.Description))]
    private static partial ContentResponseDto MapToContentResponseDto(Content content);

    public static ContentResponseDto ToContentResponseDto(Content content)
    {
        ContentResponseDto dto = MapToContentResponseDto(content);

        switch (content.Type)
        {
            case ContentType.Screw:
                dto.Definition = content.Screw;
                break;
            case ContentType.Nut:
            case ContentType.Washer:
                dto.Definition = content.Size;
                break;
        }

        return dto;
    }
}