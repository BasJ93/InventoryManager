using System.Text;
using InventoryManager.Domain;
using InventoryManager.Domain.Configuration;
using InventoryManager.Domain.Enums;
using Microsoft.Extensions.Options;

namespace InventoryManager.LabelPrinter;

public class PrintLabel : IPrintLabel
{
    private readonly LabelPrinterConfiguration _printerConfiguration;

    public PrintLabel(IOptions<LabelPrinterConfiguration> printerConfiguration)
    {
        _printerConfiguration = printerConfiguration.Value;
    }

    // TODO: Consider moving this to a different class, and/or make it protected
    public string ParseTemplate(string template, Dictionary<string, string> variables)
    {
        string parsedString = template;
        
        foreach (KeyValuePair<string,string> replacementTarget in variables)
        {
            parsedString = parsedString.Replace("%" + replacementTarget.Key + "%", replacementTarget.Value);
        }

        return parsedString;
    }

    public bool Print(LabelDefinition label, ICollection<Container> containers)
    {
        foreach (Container container in containers)
        {
            // TODO: Keep track of the label results
            Print(label, container);
        }

        // Cut is triggered to early. Delay somehow?
        
        if (_printerConfiguration.HasCutter && _printerConfiguration.UsesDelayedCut)
        {
            using FileStream printer = File.OpenWrite(_printerConfiguration.LabelPrinterAddress!);

            printer.Write(Encoding.ASCII.GetBytes(_printerConfiguration.DelayedCutterCommand!));
        }

        return true;
    }
    
    public bool Print(LabelDefinition label, Container container)
    {
        if (_printerConfiguration.LabelPrinterEnabled == false)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(_printerConfiguration.LabelPrinterAddress))
        {
            return false;
        }
        
        if (_printerConfiguration.NetworkLabelPrinter)
        {
            // TODO: Implement network printer
            return false;
        }
        
        return PrintDirect(label, container);
    }

    private bool PrintDirect(LabelDefinition label, Container container)
    {
        if (container.Content?.Standard == null)
        {
            return false;
        }

        string labelCode = ParseTemplate(label.CommandText, ExtractVariables(container.Content));

        using FileStream printer = File.OpenWrite(_printerConfiguration.LabelPrinterAddress!);

        printer.Write(Encoding.ASCII.GetBytes(labelCode));

        return true;
    }

    private Dictionary<string, string> ExtractVariables(Content content)
    {
        Dictionary<string, string> variables = new();
        
        variables.Add("Standard", content.Standard?.Name ?? string.Empty);

        switch (content.Type)
        {
            case ContentType.Screw:
                variables.Add("Content", content.Screw);
                break;
            default:
                variables.Add("Content", content.Size);
                break;
        }

        return variables;
    }
}