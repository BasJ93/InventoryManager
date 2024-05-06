using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace InventoryManager.Reports;

public static class LabelTypography
{
    public static TextStyle Dimension => TextStyle
        .Default
        .FontFamily("DejaVu Sans Mono")
        .FontSize(8)
        .Bold();
    
    public static TextStyle Specification => TextStyle
        .Default
        .FontFamily("DejaVu Sans Mono")
        .FontSize(8);
}