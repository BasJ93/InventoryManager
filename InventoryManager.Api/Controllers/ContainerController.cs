using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ContainerController : ControllerBase
{
    private readonly IContainerService _containerService;

    public ContainerController(IContainerService containerService)
    {
        _containerService = containerService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ContainerOverviewResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContainers(CancellationToken ctx = default)
    {
        return Ok(await _containerService.GetAllContainers(ctx));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateContainer([FromBody] CreateContainerRequestDto requestDto, CancellationToken ctx = default)
    {
        Guid containerId = await _containerService.CreateContainer(requestDto, ctx);
        
        if (containerId == Guid.Empty)
        {
            return BadRequest();
        }

        return Created($"/api/containers/{containerId}", containerId);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContainer([FromRoute] Guid id, CancellationToken ctx = default)
    {
        ContainerResponseDto? responseDto = await _containerService.GetContainer(id, ctx);

        if (responseDto == default)
        {
            return NotFound();
        }

        return Ok(responseDto);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateContainer([FromRoute] Guid id, CancellationToken ctx = default)
    {
        return NotFound();
    }
    
    [HttpGet("unplaced")]
    [ProducesResponseType(typeof(List<ContainerWithLocationResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnplacedContainers(CancellationToken ctx = default)
    {
        return Ok(await _containerService.GetUnplacedContainers(ctx));
    }

    [HttpGet("sizes")]
    [ProducesResponseType(typeof(List<ContainerSizeDto>), StatusCodes.Status200OK)]
    public IActionResult GetContainerSizes()
    {
        return Ok(_containerService.GetContainerSizes());
    }

    [HttpPost("labels")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLabelsForContainers(CancellationToken ctx = default)
    {
        (MemoryStream? labelsPdf, string? fileName) = await _containerService.GetContainerLabels(ctx);
        
        if (labelsPdf == default)
        {
            return NotFound();
        }
        
        FileStreamResult fileStreamResult = new FileStreamResult(labelsPdf, "application/pdf")
        {
            FileDownloadName = fileName,
        };

        return fileStreamResult;
    }
}