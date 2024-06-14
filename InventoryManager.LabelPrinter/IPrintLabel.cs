using InventoryManager.Domain;

namespace InventoryManager.LabelPrinter;

public interface IPrintLabel
{
    public bool Print(LabelDefinition label, Container container);
    public bool Print(LabelDefinition label, ICollection<Container> containers);
}