using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

/// <summary>
/// Controller to interact with the label definitions.
/// </summary>
[ApiController]
[Route("api/[controller]s")]
public class LabelDefinitionController : ControllerBase
{
    private readonly ILabelDefinitionService _labelDefinitionService;
    
    
    public LabelDefinitionController(ILabelDefinitionService labelDefinitionService)
    {
        _labelDefinitionService = labelDefinitionService;
    }

    /// <summary>
    /// Get a list of defined label.
    /// </summary>
    [HttpGet()]
    [ProducesResponseType(typeof(List<LabelDefinitionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLabelDefinitions(CancellationToken ctx = default)
    {
        return Ok(await _labelDefinitionService.GetLabelDefinitions(ctx));
    }
    
    /// <summary>
    /// Get a list of available label types.
    /// </summary>
    [HttpGet("types")]
    [ProducesResponseType(typeof(List<LabelTypeDto>), StatusCodes.Status200OK)]
    public IActionResult GetLabelTypes()
    {
        return Ok(_labelDefinitionService.GetLabelTypes());
    }
    
    /// <summary>
    /// Get the details of a specific label definition.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LabelDefinitionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLabelDefinition([FromRoute] Guid id, CancellationToken ctx = default)
    {
        LabelDefinitionDto? labelDefinitionDto = await _labelDefinitionService.GetLabelDefinition(id, ctx);

        if (labelDefinitionDto == null)
        {
            return NotFound();
        }

        return Ok(labelDefinitionDto);
    }
    
    /// <summary>
    /// Update the details of a specific label definition.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LabelDefinitionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLabelDefinition([FromRoute] Guid id, [FromBody] LabelDefinitionDto dto, CancellationToken ctx = default)
    {
        LabelDefinitionDto? labelDefinitionDto = await _labelDefinitionService.SetLabelDefinition(id, dto, ctx);

        if (labelDefinitionDto == null)
        {
            return NotFound();
        }

        return Ok(labelDefinitionDto);
    }
}