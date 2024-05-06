using InventoryManager.Domain;
using InventoryManager.Models;
using Riok.Mapperly.Abstractions;

namespace InventoryManager.Api.Mappers;

[Mapper]
public static partial class StorageCaseMapper
{
    private static partial GetStorageCasesResponseDto MapGetStorageCasesResponseDto(StorageCase storageCase);

    public static GetStorageCasesResponseDto ToGetStorageCasesResponseDto(StorageCase storageCase)
    {
        GetStorageCasesResponseDto dto = MapGetStorageCasesResponseDto(storageCase);

        dto.Size = $"{storageCase.SizeX}x{storageCase.SizeY}";

        return dto;
    }
    
    [MapperIgnoreTarget(nameof(GetStorageCaseResponseDto.Containers))]
    private static partial GetStorageCaseResponseDto MapGetStorageCaseResponseDto(StorageCase storageCase);
    
    public static GetStorageCaseResponseDto ToGetStorageCaseReponseDto(StorageCase storageCase)
    {
        GetStorageCaseResponseDto dto = MapGetStorageCaseResponseDto(storageCase);
        
        dto.Size = $"{storageCase.SizeX}x{storageCase.SizeY}";

        dto.Containers = storageCase.Containers.Select(x => x.Container).Select(ContainerMapper.ContainerToResponseWithLocationDto).ToList();

        return dto;
    }
}