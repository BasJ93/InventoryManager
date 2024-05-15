using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using QuestPDF.Elements.Table;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InventoryManager.Reports;

public static class LidCellExtensions
{
    private static float _cellHeight = 0;
    private static Unit _cellHeightUnit = Unit.Point;
    
    public static void SetInventoryHeight(float height, Unit unit = Unit.Point)
    {
        _cellHeight = height;
        _cellHeightUnit = unit;
    }
    
    public static void LidCell(this IContainer container, Container storageContainer)
    {
        switch (storageContainer.Content?.Type)
        {
            case ContentType.Screw:
                container.LidScrew(storageContainer.Content, _cellHeight * storageContainer.Height());
                break;
            default:
                container.LidSimple(storageContainer.Content, _cellHeight * storageContainer.Height());
                break;
        }
    }
    
    public static void LidScrew(this IContainer container, string specification, string kind, string lenght, float height = 0)
    {
        container.LidCell(specification, $"{kind}x{lenght}", height);
    }

    private static void LidScrew(this IContainer container, Content content, float height = 0)
    {
        container.LidCell(content.Standard?.Name, content.Screw, height);
    }
    
    // TODO: Come up with a better name.
    private static void LidSimple(this IContainer container, Content content, float height = 0)
    {
        container.LidCell(content.Standard?.Name, content.Size, height);
    }
    
    public static IContainer HiddenLidCell(this ITableCellContainer container)
    {
        return container
            .Border(0)
            .Background(Colors.White)
            .AlignCenter()
            .AlignMiddle()
            .Height(_cellHeight, _cellHeightUnit);
    }

    private static IContainer BaseLidCell(this IContainer container, float height = 0)
    {
        if (height == 0)
        {
            height = _cellHeight;
        }
        
        return container
            .Border(1)
            .Background(Colors.White)
            .AlignCenter()
            .AlignMiddle()
            .Height(height, _cellHeightUnit);
    }

    private static void LidCell(this IContainer container, string line1, string line2, float height = 0)
    {
        container.BaseLidCell(height)
            .Text(text =>
            {
                // Set alignment
                text.AlignCenter();
                // Set content
                text.Span(line1).Style(LidTypography.Specification);
                text.EmptyLine();
                text.EmptyLine();
                text.Span(line2).Style(LidTypography.Dimension);
            });
    }
}