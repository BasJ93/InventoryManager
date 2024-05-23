using InventoryManager.Domain;

namespace InventoryManager.Reports;

public interface IReportGenerator
{
    MemoryStream GenerateCaseLidSheet(StorageLocation storageLocation);
    
    MemoryStream GenerateContainerLabelsSheet(StorageLocation storageLocation);

    MemoryStream GenerateContainerLabelsSheet(ICollection<Container> storageContainers);
}