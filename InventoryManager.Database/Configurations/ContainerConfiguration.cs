using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class ContainerConfiguration : IEntityTypeConfiguration<Container>
{
    public void Configure(EntityTypeBuilder<Container> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<Container>(null);

        builder.ToTable(tableName);

        builder.HasKey(x => x.Id)
            .HasName($"PK_{tableName}");

        builder.Property(x => x.Id)
            .HasColumnType(DbTypes.Guid)
            .IsRequired();

        builder.Property(x => x.ContentId)
            .HasColumnType(DbTypes.Guid)
            .IsRequired();

        builder.Property(x => x.Size)
            .HasColumnType(DbTypes.Int)
            .IsRequired();

        builder.HasOne(x => x.Content)
            .WithMany(y => y.Containers)
            .HasForeignKey(x => x.ContentId)
            .HasConstraintName($"FK_{nameof(Container)}_{nameof(Content)}");

    }
}