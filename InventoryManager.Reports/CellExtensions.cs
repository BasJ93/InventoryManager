using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using QuestPDF.Elements.Table;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InventoryManager.Reports;

public static class CellExtensions
{
    private static float _labelHeight = 0;
    private static Unit _labelHeightUnit = Unit.Point;
    
    
    private static IContainer BaseLabelCell(this ITableCellContainer container)
    {
        return container
            .Border(1)
            .Background(Colors.White)
            .Height(_labelHeight, _labelHeightUnit);
    }

    /// <summary>
    /// Set the height of the label.
    /// </summary>
    public static void SetLabelHeight(float height, Unit unit = Unit.Point)
    {
        _labelHeight = height;
        _labelHeightUnit = unit;
    }

    public static void LabelCell(this ITableCellContainer container, string line)
    {
        container.BaseLabelCell()
            .Text(text =>
            {
                // Set alignment
                text.AlignCenter();
                // Set content
                text.Span(line).Style(LabelTypography.Dimension);
            });
    }
    
    public static void LabelCell(this ITableCellContainer container, string line1, string line2)
    {
        container.BaseLabelCell()
            .Text(text =>
            {
                // Set alignment
                text.AlignCenter();
                // Set content
                text.Span(line1).Style(LabelTypography.Specification);
                text.EmptyLine();
                text.Span(line2).Style(LabelTypography.Dimension);
            });
    }
    
    public static void LabelCell(this ITableCellContainer container, Container storageContainer)
    {
        switch (storageContainer.Content.Type)
        {
            case ContentType.Screw:
                container.BaseLabelCell()
                    .Text(text =>
                    {
                        // Set alignment
                        text.AlignCenter();
                        // Set content
                        text.Span(storageContainer.Content.Standard).Style(LabelTypography.Specification);
                        text.EmptyLine();
                        text.Span(storageContainer.Content.Screw).Style(LabelTypography.Dimension);
                    });
                break;
            default:
                container.BaseLabelCell()
                    .Text(text =>
                    {
                        // Set alignment
                        text.AlignCenter();
                        // Set content
                        text.Span(storageContainer.Content.Standard).Style(LabelTypography.Specification);
                        text.EmptyLine();
                        text.Span(storageContainer.Content.Size).Style(LabelTypography.Dimension);
                    });
                break;
        }
    }

    public static void ScrewLabel(this ITableCellContainer container, string specification, string kind, string lenght)
    {
        container.LabelCell(specification, $"{kind}x{lenght}");
    }
}