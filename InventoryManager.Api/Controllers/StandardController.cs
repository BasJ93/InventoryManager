using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class StandardController : ControllerBase
{
    private readonly IStandardsService _standardsService;

    public StandardController(IStandardsService standardsService)
    {
        _standardsService = standardsService;
    }

    /// <summary>
    /// Get a list of all standards known in the system.
    /// </summary>
    /// <param name="ctx">Cancellation token.</param>
    /// <returns>A list of standards.</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<StandardResponseDto>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStandards(CancellationToken ctx = default)
    {
        return Ok(await _standardsService.GetAllStandards(ctx));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStandard([FromBody] CreateStandardRequestDto requestDto, CancellationToken ctx = default)
    {
        Guid standardId = await _standardsService.CreateStandard(requestDto, ctx);

        if (standardId == default)
        {
            return BadRequest();
        }

        return Created($"/api/standards/{standardId}", standardId);
    }
}