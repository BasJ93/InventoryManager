using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ContentController : Controller
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    /// <summary>
    /// Get a list of all contents.
    /// </summary>
    /// <param name="withoutContainer">Optional argument that limits the response to contents without an assigned container.</param>
    /// <param name="ctx">Cancellation token.</param>
    /// <returns>A list of contents.</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ContentReponseDto>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContents([FromQuery] bool withoutContainer = true, CancellationToken ctx = default)
    {
        return Ok(await _contentService.GetAllContents(withoutContainer, ctx));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateContent([FromBody] CreateContentRequestDto requestDto, CancellationToken ctx = default)
    {
        Guid contentId = await _contentService.CreateContent(requestDto, ctx);

        if (contentId == default)
        {
            return BadRequest();
        }

        return Created($"/api/content/{contentId}", contentId);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetContent([FromRoute] Guid id, CancellationToken ctx = default)
    {
        return NotFound();
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateContent([FromRoute] Guid id, CancellationToken ctx = default)
    {
        return NotFound();
    }
    
    [HttpGet("types")]
    [ProducesResponseType(typeof(List<ContentTypeDto>), StatusCodes.Status200OK)]
    public IActionResult GetContentTypes()
    {
        return Ok(_contentService.GetContentTypes());
    }
}