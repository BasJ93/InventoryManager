using InventoryManager.Domain;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class ContainerMapper
{
    [MapProperty("Position.PositionX", nameof(ContainerWithLocationResponseDto.PositionX))]
    [MapProperty("Position.PositionY", nameof(ContainerWithLocationResponseDto.PositionY))]
    private static partial ContainerWithLocationResponseDto ContainerToResponseDto(Container container);

    public static partial ContainerResponseDto ToContainerResponseDto(Container container);
    
    public static ContainerWithLocationResponseDto ContainerToResponseWithLocationDto(Container container)
    {
        ContainerWithLocationResponseDto dto = ContainerToResponseDto(container);

        dto.Size = container.Size.ToString();
        if (container.Content != null)
        {
            dto.Content = ContentMapper.ToContentResponseDto(container.Content);
        }

        return dto;
    }
    
    public static partial IQueryable<ContainerOverviewResponseDto> ProjectToDto(this IQueryable<Container> q);

    [MapProperty("Position.Location" ,nameof(ContainerOverviewResponseDto.Location))]
    private static partial ContainerOverviewResponseDto Map(Container container);

    private static ContentResponseDto MapContentStandard(Content content)
        => ContentMapper.ToContentResponseDto(content);
}