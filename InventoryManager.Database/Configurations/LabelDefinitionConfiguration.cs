using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class LabelDefinitionConfiguration : IEntityTypeConfiguration<LabelDefinition>
{
    public void Configure(EntityTypeBuilder<LabelDefinition> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<LabelDefinition>(null);
        
        builder.ToTable(tableName);

        builder.HasKey(x => x.Id)
            .HasName($"PK_{tableName}");

        builder.Property(x => x.Id)
            .HasColumnType(DbTypes.Guid)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnType(DbTypes.Int)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        builder.Property(x => x.CommandText)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
    }
}