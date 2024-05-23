using InventoryManager.Domain;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class StorageCaseMapper
{
    private static partial GetStorageLocationsResponseDto MapGetStorageLocationsResponseDto(StorageLocation storageLocation);

    public static GetStorageLocationsResponseDto ToGetStorageLocationsResponseDto(StorageLocation storageLocation)
    {
        GetStorageLocationsResponseDto dto = MapGetStorageLocationsResponseDto(storageLocation);

        dto.Size = $"{storageLocation.SizeX}x{storageLocation.SizeY}";

        return dto;
    }
    
    [MapperIgnoreTarget(nameof(GetStorageLocationResponseDto.Containers))]
    private static partial GetStorageLocationResponseDto MapGetStorageLocationResponseDto(StorageLocation storageLocation);
    
    public static GetStorageLocationResponseDto ToGetStorageLocationReponseDto(StorageLocation storageLocation)
    {
        GetStorageLocationResponseDto dto = MapGetStorageLocationResponseDto(storageLocation);
        
        dto.Size = $"{storageLocation.SizeX}x{storageLocation.SizeY}";

        dto.Containers = storageLocation.Containers.Select(x => x.Container).Select(ContainerMapper.ContainerToResponseWithLocationDto).ToList();

        return dto;
    }
}