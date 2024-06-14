using System.Collections.Generic;
using InventoryManager.Domain;
using InventoryManager.Domain.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace InventoryManager.LabelPrinter.Tests;

public class ReplacementTests
{
    [Fact]
    public void ReplacedStandard_IsSuccessful()
    {
        string template = "^XA" +
            "^FO32,4" +
            "^FB160,1,0,C,0" +
            "^AAN,18,10" +
            "^FD%Standard%" +
            "^FS" +
            "^XZ";

        Dictionary<string, string> variables = new Dictionary<string, string>()
        {
            {"Standard", "DIN912"},
        };

        IOptions<LabelPrinterConfiguration> options = Options.Create(new LabelPrinterConfiguration());
        
        PrintLabel printLabel = new(options);

        string parsed = printLabel.ParseTemplate(template, variables);
        
        Assert.DoesNotContain("%", parsed);
    }
}