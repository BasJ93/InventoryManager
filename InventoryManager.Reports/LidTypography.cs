using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace InventoryManager.Reports;

public static class LidTypography
{
    public static TextStyle Dimension => TextStyle
        .Default
        .FontFamily("DejaVu Sans Mono")
        .FontSize(25)
        .Bold();
    
    public static TextStyle Specification => TextStyle
        .Default
        .FontFamily("DejaVu Sans Mono")
        .FontSize(18);
}