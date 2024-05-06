using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class ContentMapper
{
    private static partial ContentReponseDto MapToContentResponseDto(Content content);

    public static ContentReponseDto ToContentResponseDto(Content content)
    {
        ContentReponseDto dto = MapToContentResponseDto(content);

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