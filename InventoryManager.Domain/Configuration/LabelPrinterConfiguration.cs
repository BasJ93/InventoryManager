namespace InventoryManager.Domain.Configuration;

/// <summary>
/// Configuration class to enable (or disable) using a label printer.
/// </summary>
public class LabelPrinterConfiguration
{
    // TODO: Consider adding a name for the printer, and add a db id, allowing for multiple printers.
    
    /// <summary>
    /// Indicator if the label printer is enabled.
    /// </summary>
    public bool LabelPrinterEnabled { get; set; }

    /// <summary>
    /// Indicator if the label printer is network connected.
    /// </summary>
    public bool NetworkLabelPrinter { get; set; }
    
    /// <summary>
    /// The address of the printer.
    /// </summary>
    /// <example>
    /// /dev/usb/lp3
    /// </example>
    /// <example>
    /// /dev/ttyUSB0
    /// </example>
    /// <example>
    /// 192.168.0.25:9001
    /// </example>
    public string? LabelPrinterAddress { get; set; }

    /// <summary>
    /// Indicator if the printer has a label cutter.
    /// </summary>
    public bool HasCutter { get; set; }

    /// <summary>
    /// Indicator if the printer is set to delay cutting until a command is issued.
    /// </summary>
    public bool UsesDelayedCut { get; set; }
    
    /// <summary>
    /// Command to trigger a delayed cut.
    /// </summary>
    public string? DelayedCutterCommand { get; set; }
}