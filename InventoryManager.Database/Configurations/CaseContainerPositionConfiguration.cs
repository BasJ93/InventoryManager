using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class CaseContainerPositionConfiguration : IEntityTypeConfiguration<CaseContainerPosition>
{
    public void Configure(EntityTypeBuilder<CaseContainerPosition> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<CaseContainerPosition>(null);

        builder.ToTable(tableName);
        
        builder.HasKey(x => new { x.CaseId, x.ContainerId })
            .HasName($"PK_{tableName}");
        
        builder.Property(x => x.CaseId)
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

        builder.HasOne(x => x.Case)
            .WithMany(y => y.Containers)
            .HasForeignKey(x => x.CaseId)
            .HasConstraintName($"FK_{nameof(StorageCase)}_{nameof(CaseContainerPosition)}");

        builder.HasOne(x => x.Container)
            .WithOne(y => y.Position)
            .HasForeignKey<CaseContainerPosition>(x => x.ContainerId)
            .HasConstraintName($"FK_{nameof(Container)}_{nameof(CaseContainerPosition)}");
    }
}