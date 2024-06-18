using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class LabelDefinitionController : ControllerBase
{
    private readonly ILabelService _labelService;
    
    
    public LabelDefinitionController(ILabelService labelService)
    {
        _labelService = labelService;
    }

    /// <summary>
    /// Get a list of available label types.
    /// </summary>
    [HttpGet("types")]
    [ProducesResponseType(typeof(List<LabelTypeDto>), StatusCodes.Status200OK)]
    public IActionResult GetStorageLocationTypes()
    {
        return Ok(_labelService.GetLabelTypes());
    }
    
    // TODO: Endpoint to get all label definitions for the label printer
    
    // TODO: Endpoint to update a label definition
    
    // TODO: Endpoint to get a specific label definition
}