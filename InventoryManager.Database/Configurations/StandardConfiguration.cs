using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class StandardConfiguration : IEntityTypeConfiguration<Standard>
{
    public void Configure(EntityTypeBuilder<Standard> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<Standard>(null);

        builder.ToTable(tableName);

        builder.HasKey(x => x.Id)
            .HasName($"PK_{tableName}");
        
        builder.Property(x => x.Id)
            .HasColumnType(DbTypes.Guid)
            .HasDefaultValueSql("NEWID()")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired(false);
        
        builder.Property(x => x.Path)
            .HasColumnType(DbTypes.NVarCharMax)
            .HasDefaultValue(string.Empty)
            .IsRequired(false);

        builder.Property(x => x.AlternativeNames)
            .HasColumnType(DbTypes.NVarCharMax)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .IsRequired(false);
    }
}