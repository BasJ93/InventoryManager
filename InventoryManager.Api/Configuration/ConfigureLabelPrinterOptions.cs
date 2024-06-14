using InventoryManager.Api.Services;
using InventoryManager.Domain.Configuration;
using Microsoft.Extensions.Options;

namespace InventoryManager.Api.Configuration;

public class ConfigureLabelPrinterOptions : IConfigureOptions<LabelPrinterConfiguration>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ConfigureLabelPrinterOptions(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Configure(LabelPrinterConfiguration options)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IServiceProvider provider = scope.ServiceProvider;

        ILabelPrinterConfigurationService labelPrinterConfigurationService = provider.GetRequiredService<ILabelPrinterConfigurationService>();
        
        // Get the configuration from the database

        LabelPrinterConfiguration? labelPrinterConfiguration = labelPrinterConfigurationService.GetConfiguration();

        if (labelPrinterConfiguration != default)
        {
            options.LabelPrinterEnabled = labelPrinterConfiguration.LabelPrinterEnabled;
            options.HasCutter = labelPrinterConfiguration.HasCutter;
            options.DelayedCutterCommand = labelPrinterConfiguration.DelayedCutterCommand;
            options.UsesDelayedCut = labelPrinterConfiguration.UsesDelayedCut;
            options.NetworkLabelPrinter = labelPrinterConfiguration.NetworkLabelPrinter;
            options.LabelPrinterAddress = labelPrinterConfiguration.LabelPrinterAddress;
        }
    }
}