using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class StorageCaseConfiguration : IEntityTypeConfiguration<StorageCase>
{
    public void Configure(EntityTypeBuilder<StorageCase> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<StorageCase>(null);

        builder.ToTable(tableName);
        
        builder.HasKey(x => x.Id)
            .HasName($"PK_{tableName}");
        
        builder.Property(x => x.Id)
            .HasColumnType(DbTypes.Guid)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        builder.Property(x => x.SizeX)
            .HasColumnType(DbTypes.Int)
            .IsRequired();

        builder.Property(x => x.SizeY)
            .HasColumnType(DbTypes.Int)
            .IsRequired();

    }
}