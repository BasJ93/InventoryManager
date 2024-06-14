using InventoryManager.Database.Configurations;
using InventoryManager.Domain;
using InventoryManager.Domain.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Database;

public class InventoryManagerContext : DbContext
{
    public DbSet<StorageLocation> StorageCases => Set<StorageLocation>();

    public DbSet<Container> Containers => Set<Container>();

    public DbSet<Content> Contents => Set<Content>();

    public DbSet<StorageLocationContainerPosition> CaseContainerPositions => Set<StorageLocationContainerPosition>();

    public DbSet<Standard> Standards => Set<Standard>();

    public DbSet<LabelDefinition> LabelDefinitions => Set<LabelDefinition>();

    public DbSet<LabelPrinterConfiguration> LabelPrinterConfigurations => Set<LabelPrinterConfiguration>();
    
    public InventoryManagerContext(DbContextOptions<InventoryManagerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContainerConfiguration).Assembly);
    }
}