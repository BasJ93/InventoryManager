using Elephant.Constants.SqlServer;
using InventoryManager.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class LabelPrinterConfigurationConfiguration : IEntityTypeConfiguration<LabelPrinterConfiguration>
{
    public void Configure(EntityTypeBuilder<LabelPrinterConfiguration> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<LabelPrinterConfiguration>(null);
        
        builder.ToTable(tableName);

        builder.HasNoKey();
        
        builder.Property(x => x.HasCutter)
            .HasColumnType(DbTypes.Bool)
            .IsRequired();

        builder.Property(x => x.LabelPrinterEnabled)
            .HasColumnType(DbTypes.Bool)
            .IsRequired();
        
        builder.Property(x => x.NetworkLabelPrinter)
            .HasColumnType(DbTypes.Bool)
            .IsRequired();
        
        builder.Property(x => x.UsesDelayedCut)
            .HasColumnType(DbTypes.Bool)
            .IsRequired();
        
        builder.Property(x => x.DelayedCutterCommand)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        builder.Property(x => x.LabelPrinterAddress)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
    }
}