using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class StorageLocationController : ControllerBase
{
    private readonly IStorageLocationService _storageLocationService;

    public StorageLocationController(IStorageLocationService storageLocationService)
    {
        _storageLocationService = storageLocationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetStorageLocationsResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStorageLocations(CancellationToken ctx = default)
    {
        return Ok(await _storageLocationService.GetStorageLocations(ctx));
    }

    [HttpGet("types")]
    [ProducesResponseType(typeof(List<StorageLocationTypeDto>), StatusCodes.Status200OK)]
    public IActionResult GetStorageLocationTypes()
    {
        return Ok(_storageLocationService.GetStorageLocationTypes());
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStorageLocation([FromBody] CreateStorageLocationRequestDto requestDto, CancellationToken ctx = default)
    {
        Guid storageCaseId = await _storageLocationService.CreateStorageLocation(requestDto, ctx);

        if (storageCaseId == Guid.Empty)
        {
            return BadRequest();
        }

        return Created($"/api/storagelocations/{storageCaseId}", storageCaseId);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetStorageLocationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStorageLocation([FromRoute] Guid id, CancellationToken ctx = default)
    {
        GetStorageLocationResponseDto? storageCase = await _storageLocationService.GetStorageLocation(id, ctx);

        if (storageCase == default)
        {
            return NotFound();
        }
        
        return Ok(storageCase);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateStorageLocation([FromRoute] Guid id, CancellationToken ctx = default)
    {
        return NotFound();
    }
    
    [HttpPut("{id:guid}/{x:int}/{y:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PutContainerInStorageLocation([FromRoute] Guid id, [FromRoute] int x, [FromRoute] int y, [FromBody] Guid containerId,
        CancellationToken ctx = default)
    {
        // TODO: Replace with proper responses
        return Ok(await _storageLocationService.PlaceContainerInStorageLocation(id, x, y, containerId, ctx));
    }
    
    [HttpDelete("{id:guid}/{x:int}/{y:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveContainerFromStorageLocation([FromRoute] Guid id, [FromRoute] int x, [FromRoute] int y, CancellationToken ctx = default)
    {
        // TODO: Replace with proper responses
        return Ok(await _storageLocationService.RemoveContainerFromStorageLocation(id, x, y, ctx));
    }
    
    [HttpGet("{id:guid}/lid")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLidInsertForStorageLocation([FromRoute] Guid id, CancellationToken ctx = default)
    {
        (MemoryStream? lidPdf, string? fileName) = await _storageLocationService.GenerateLidPdf(id, ctx);

        if (lidPdf == default)
        {
            return NotFound();
        }
        
        FileStreamResult fileStreamResult = new FileStreamResult(lidPdf, "application/pdf")
        {
            FileDownloadName = fileName,
        };

        return fileStreamResult;
    }

    [HttpGet("{id:guid}/labels")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLabelsForStorageLocation([FromRoute] Guid id, CancellationToken ctx = default)
    {
        (MemoryStream? labelsPdf, string? fileName) = await _storageLocationService.GenerateLabelsPdf(id, ctx);

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
    
    [HttpPost("{id:guid}/labels/print")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PrintLabelsForStorageLocation([FromRoute] Guid id, CancellationToken ctx = default)
    {
        bool printed = await _storageLocationService.PrintLabelsOnLabelPrinter(id, ctx);

        if (printed)
        {
            return Ok();
        }

        return NotFound();
    }
}