using InventoryManager.Domain.Configuration;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

public interface ILabelPrinterConfigurationService
{
    /// <summary>
    /// Returns the current configuration from the database. The result is not tracked by EF.
    /// </summary>
    /// <returns></returns>
    LabelPrinterConfiguration? GetConfiguration();
    
    Task<LabelPrinterConfiguration?> GetConfiguration(CancellationToken ctx = default);

    Task<LabelPrinterConfiguration?> GetConfiguration(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken ctx = default);

    Task<bool> SetConfiguration(LabelPrinterConfiguration configuration, CancellationToken ctx = default);

    Task<LabelPrinterEnabledDto?> GetQuickLabelPrinterSettings(CancellationToken ctx = default);

    Task<bool> SetLabelPrinterEnabled(bool enabled, CancellationToken ctx = default);

    // TODO: Get and Set for a DTO that contains all settings for the printer
}