using Elephant.Constants.SqlServer;
using InventoryManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Database.Configurations;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        string tableName = ConfigurationHelper.ToTableName<Content>(null);

        builder.ToTable(tableName);

        builder.HasKey(x => x.Id)
            .HasName($"PK_{tableName}");

        builder.Property(x => x.Id)
            .HasColumnType(DbTypes.Guid)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnType(DbTypes.Int)
            .IsRequired();

        builder.Property(x => x.Standard)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        builder.Property(x => x.Size)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        builder.Property(x => x.Length)
            .HasColumnType(DbTypes.NVarCharMax)
            .IsRequired();
        
        // TODO: Consider forcing the combination of type + standard + size + length to be unique 
    }
}