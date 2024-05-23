using InventoryManager.Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InventoryManager.Reports;

public class ReportGenerator : IReportGenerator
{
    public ReportGenerator()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    /// <inheritdoc />
    public MemoryStream GenerateCaseLidSheet(StorageLocation storageLocation)
    {
        MemoryStream result = new MemoryStream();
        
        // Page dimensions can be retrieved with PageSizes.A4.Width
        PageSize pageSize = PageSizes.A4.Landscape();

        // Case contains 6x7 containers max
        int caseContainerCountHorizontal = storageLocation.SizeX;
        int caseContainerCountVertical = storageLocation.SizeY;
        float caseColumnWidth = Convert.ToSingle(Math.Floor(pageSize.Width / caseContainerCountHorizontal));
        float caseRowHeight = Convert.ToSingle(Math.Floor(pageSize.Height / caseContainerCountVertical));

        float caseColumnMargin = (pageSize.Width - (caseContainerCountHorizontal * caseColumnWidth)) / 2;
        float caseRowMargin = (pageSize.Height - (caseContainerCountVertical * caseRowHeight)) / 2;

        LidCellExtensions.SetInventoryHeight(caseRowHeight);
        
        Document.Create(container =>
        {
            // Case lid page
            container.Page(page =>
            {
                page.Size(pageSize);
                page.Margin(0);
                page.MarginHorizontal(caseColumnMargin);
                page.PageColor(Colors.White);

                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < caseContainerCountHorizontal; i++)
                            {
                                columns.ConstantColumn(caseColumnWidth);
                            }
                        });

                        List<int> existingRows = storageLocation.Containers.Select(c => c.PositionY).Distinct().ToList();

                        if (existingRows.Count < caseContainerCountVertical)
                        {
                            for (int i = 1; i <= caseContainerCountVertical; i++)
                            {
                                if (!existingRows.Contains(i))
                                {
                                    table.Cell()
                                        .Row(Convert.ToUInt32(i))
                                        .Column(1)
                                        .HiddenLidCell();
                                    existingRows.Add(i);
                                }
                            }
                        }

                        foreach (StorageLocationContainerPosition containerPosition in storageLocation.Containers)
                        {
                            table.Cell()
                                .Column(Convert.ToUInt32(containerPosition.PositionX))
                                .Row(Convert.ToUInt32(containerPosition.PositionY))
                                .RowSpan(containerPosition.Container.Height())
                                .ColumnSpan(containerPosition.Container.Width())
                                .LidCell(containerPosition.Container);
                        }
                    });
            });
        })
        .GeneratePdf(result);

        return result;
    }

    /// <inheritdoc />
    public MemoryStream GenerateContainerLabelsSheet(StorageLocation storageLocation)
    {
        MemoryStream result = new MemoryStream();
        
        // Page dimensions can be retrieved with PageSizes.A4.Width
        PageSize pageSize = PageSizes.A4;

        // Container label generation
        int columnCount = 210 / 19;
        int rowCount = 297 / 9;
        
        CellExtensions.SetLabelHeight(9, Unit.Millimetre);
        
        Document.Create(container =>
        {
            // Labels page
            container.Page(page =>
            {
                page.Size(pageSize);
                page.Margin(0);
                page.MarginHorizontal(0.5f, Unit.Millimetre);
                page.PageColor(Colors.White);
            
                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for(int i = 0; i < columnCount; i++)
                            {
                                columns.ConstantColumn(19, Unit.Millimetre);
                            }
                        });

                        foreach (StorageLocationContainerPosition containerPosition in storageLocation.Containers)
                        {
                            table.Cell()
                                .LabelCell(containerPosition.Container);
                        }
                    });
            });
        })
        .GeneratePdf(result);

        return result;
    }

    /// <inheritdoc />
    public MemoryStream GenerateContainerLabelsSheet(ICollection<Container> storageContainers)
    {
        MemoryStream result = new MemoryStream();
        
        // Page dimensions can be retrieved with PageSizes.A4.Width
        PageSize pageSize = PageSizes.A4;

        // Container label generation
        int columnCount = 210 / 19;
        int rowCount = 297 / 9;
        
        CellExtensions.SetLabelHeight(9, Unit.Millimetre);
        
        Document.Create(container =>
            {
                // Labels page
                container.Page(page =>
                {
                    page.Size(pageSize);
                    page.Margin(0);
                    page.MarginHorizontal(0.5f, Unit.Millimetre);
                    page.PageColor(Colors.White);
            
                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                for(int i = 0; i < columnCount; i++)
                                {
                                    columns.ConstantColumn(19, Unit.Millimetre);
                                }
                            });

                            foreach (Container storageContainer in storageContainers)
                            {
                                table.Cell()
                                    .LabelCell(storageContainer);
                            }
                        });
                });
            })
            .GeneratePdf(result);

        return result;
    }
}