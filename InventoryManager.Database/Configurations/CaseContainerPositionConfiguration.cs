using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class CaseContainerPositionConfiguration : IEntityTypeConfiguration<StorageLocationContainerPosition>
{
    public void Configure(EntityTypeBuilder<StorageLocationContainerPosition> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<StorageLocationContainerPosition>(null);

        builder.ToTable(tableName);
        
        builder.HasKey(x => new { CaseId = x.StorageLocationId, x.ContainerId })
            .HasName($"PK_{tableName}");
        
        builder.Property(x => x.StorageLocationId)
            .HasColumnType(DbTypes.Guid)
            .IsRequired();

        builder.Property(x => x.ContainerId)
            .HasColumnType(DbTypes.Guid)
            .IsRequired();
        
        builder.Property(x => x.PositionX)
            .HasColumnType(DbTypes.Int)
            .IsRequired();

        builder.Property(x => x.PositionY)
            .HasColumnType(DbTypes.Int)
            .IsRequired();

        builder.HasOne(x => x.Location)
            .WithMany(y => y.Containers)
            .HasForeignKey(x => x.StorageLocationId)
            .HasConstraintName($"FK_{nameof(StorageLocation)}_{nameof(StorageLocationContainerPosition)}");

        builder.HasOne(x => x.Container)
            .WithOne(y => y.Position)
            .HasForeignKey<StorageLocationContainerPosition>(x => x.ContainerId)
            .HasConstraintName($"FK_{nameof(Container)}_{nameof(StorageLocationContainerPosition)}");
    }
}