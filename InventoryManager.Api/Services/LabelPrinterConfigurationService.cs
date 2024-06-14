using InventoryManager.Database;
using InventoryManager.Domain.Configuration;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

public class LabelPrinterConfigurationService : ILabelPrinterConfigurationService
{
    private readonly InventoryManagerContext _db;

    public LabelPrinterConfigurationService(InventoryManagerContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public LabelPrinterConfiguration? GetConfiguration()
    {
        return _db.LabelPrinterConfigurations.AsNoTracking().FirstOrDefault();
    }

    public async Task<LabelPrinterConfiguration?> GetConfiguration(CancellationToken ctx = default)
    {
        return await GetConfiguration(QueryTrackingBehavior.TrackAll, ctx);
    }
    
    public async Task<LabelPrinterConfiguration?> GetConfiguration(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken ctx = default)
    {
        return await _db.LabelPrinterConfigurations.AsTracking(queryTrackingBehavior).FirstOrDefaultAsync(ctx);
    }

    public async Task<bool> SetConfiguration(LabelPrinterConfiguration configuration, CancellationToken ctx = default)
    {
        LabelPrinterConfiguration? labelPrinterConfiguration = await GetConfiguration(ctx);

        if (labelPrinterConfiguration == null)
        {
            return false;
        }
        
        labelPrinterConfiguration.LabelPrinterAddress = configuration.LabelPrinterAddress;
        labelPrinterConfiguration.LabelPrinterEnabled = configuration.LabelPrinterEnabled;
        labelPrinterConfiguration.NetworkLabelPrinter = configuration.NetworkLabelPrinter;
        labelPrinterConfiguration.HasCutter = configuration.HasCutter;
        labelPrinterConfiguration.UsesDelayedCut = configuration.UsesDelayedCut;
        labelPrinterConfiguration.DelayedCutterCommand = configuration.DelayedCutterCommand;

        int changes = await _db.SaveChangesAsync(ctx);

        return changes > 0;
    }

    public async Task<LabelPrinterEnabledDto?> GetQuickLabelPrinterSettings(CancellationToken ctx = default)
    {
        LabelPrinterConfiguration? labelPrinterConfiguration = await GetConfiguration(QueryTrackingBehavior.NoTracking, ctx);

        if (labelPrinterConfiguration == null)
        {
            return null;
        }

        return new LabelPrinterEnabledDto()
        {
            LabelPrinterEnabled = labelPrinterConfiguration.LabelPrinterEnabled,
        };
    }

    public async Task<bool> SetLabelPrinterEnabled(bool enabled, CancellationToken ctx = default)
    {
        LabelPrinterConfiguration? labelPrinterConfiguration = await GetConfiguration(ctx);

        if (labelPrinterConfiguration == null)
        {
            return false;
        }

        labelPrinterConfiguration.LabelPrinterEnabled = enabled;

        return await SetConfiguration(labelPrinterConfiguration, ctx);
    }
}