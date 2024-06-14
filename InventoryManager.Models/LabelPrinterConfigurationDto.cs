namespace InventoryManager.Models;

public class LabelPrinterConfigurationDto : LabelPrinterEnabledDto
{
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