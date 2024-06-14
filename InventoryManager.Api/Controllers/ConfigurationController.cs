using InventoryManager.Api.Services;
using InventoryManager.Domain.Configuration;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ConfigurationController : ControllerBase
{
    private readonly ILabelPrinterConfigurationService _labelPrinterConfiguration;

    public ConfigurationController(ILabelPrinterConfigurationService labelPrinterConfiguration)
    {
        _labelPrinterConfiguration = labelPrinterConfiguration;
    }

    [HttpGet]
    [ProducesResponseType(typeof(LabelPrinterConfigurationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLabelPrinterConfiguration(CancellationToken ctx = default)
    {
        LabelPrinterConfiguration? labelPrinterConfiguration = await _labelPrinterConfiguration.GetConfiguration(ctx);
        if (labelPrinterConfiguration == null)
        {
            return NotFound();
        }

        return Ok(new LabelPrinterConfigurationDto
        {
            LabelPrinterEnabled = labelPrinterConfiguration.LabelPrinterEnabled,
            DelayedCutterCommand = labelPrinterConfiguration.DelayedCutterCommand,
            HasCutter = labelPrinterConfiguration.HasCutter,
            LabelPrinterAddress = labelPrinterConfiguration.LabelPrinterAddress,
            NetworkLabelPrinter = labelPrinterConfiguration.NetworkLabelPrinter,
            UsesDelayedCut = labelPrinterConfiguration.UsesDelayedCut,
        });
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(LabelPrinterConfigurationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetLabelPrinterConfiguration([FromBody] LabelPrinterConfigurationDto labelPrinterConfiguration, CancellationToken ctx = default)
    {
        LabelPrinterConfiguration newConfig = new()
        {
            LabelPrinterEnabled = labelPrinterConfiguration.LabelPrinterEnabled,
            DelayedCutterCommand = labelPrinterConfiguration.DelayedCutterCommand,
            HasCutter = labelPrinterConfiguration.HasCutter,
            LabelPrinterAddress = labelPrinterConfiguration.LabelPrinterAddress,
            NetworkLabelPrinter = labelPrinterConfiguration.NetworkLabelPrinter,
            UsesDelayedCut = labelPrinterConfiguration.UsesDelayedCut,
        };

        if (await _labelPrinterConfiguration.SetConfiguration(newConfig, ctx))
        {
            return Ok(labelPrinterConfiguration);
        }

        return BadRequest();
    }
    
    [HttpGet("quick")]
    [ProducesResponseType(typeof(LabelPrinterEnabledDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuickLabelPrinterConfiguration(CancellationToken ctx = default)
    {
        LabelPrinterEnabledDto? labelPrinterEnabledDto =
            await _labelPrinterConfiguration.GetQuickLabelPrinterSettings(ctx);

        if (labelPrinterEnabledDto != null)
        {
            return Ok(labelPrinterEnabledDto);
        }

        return NotFound();
    }

    [HttpPost("quick")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetQuickLabelPrinterConfiguration([FromBody] LabelPrinterEnabledDto dto, CancellationToken ctx = default)
    {
        bool success = await _labelPrinterConfiguration.SetLabelPrinterEnabled(dto.LabelPrinterEnabled, ctx);

        if (success)
        {
            return Ok();
        }

        return NotFound();
    }
}