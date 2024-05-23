using InventoryManager.Domain;

namespace InventoryManager.Reports;

public interface IReportGenerator
{
    MemoryStream GenerateCaseLidSheet(StorageCase storageCase);
    
    MemoryStream GenerateContainerLabelsSheet(StorageCase storageCase);

    MemoryStream GenerateContainerLabelsSheet(ICollection<Container> storageContainers);
}