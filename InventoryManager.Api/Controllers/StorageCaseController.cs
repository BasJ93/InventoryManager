using InventoryManager.Api.Services;
using InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class StorageCaseController : ControllerBase
{
    private readonly IStorageCaseService _storageCaseService;

    public StorageCaseController(IStorageCaseService storageCaseService)
    {
        _storageCaseService = storageCaseService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetStorageCasesResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCases(CancellationToken ctx = default)
    {
        return Ok(await _storageCaseService.GetStorageCases(ctx));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCase([FromBody] CreateStorageCaseRequestDto requestDto, CancellationToken ctx = default)
    {
        Guid storageCaseId = await _storageCaseService.CreateStorageCase(requestDto, ctx);

        if (storageCaseId == Guid.Empty)
        {
            return BadRequest();
        }

        return Created($"/api/storagecases/{storageCaseId}", storageCaseId);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetStorageCaseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCase([FromRoute] Guid id, CancellationToken ctx = default)
    {
        GetStorageCaseResponseDto? storageCase = await _storageCaseService.GetStorageCase(id, ctx);

        if (storageCase == default)
        {
            return NotFound();
        }
        
        return Ok(storageCase);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateCase([FromRoute] Guid id, CancellationToken ctx = default)
    {
        return NotFound();
    }
    
    [HttpPut("{id:guid}/{x:int}/{y:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PutContainerInCase([FromRoute] Guid id, [FromRoute] int x, [FromRoute] int y, [FromBody] Guid containerId,
        CancellationToken ctx = default)
    {
        // TODO: Replace with proper responses
        return Ok(await _storageCaseService.PlaceContainerInStorageCase(id, x, y, containerId, ctx));
    }
    
    [HttpDelete("{id:guid}/{x:int}/{y:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveContainerFromCase([FromRoute] Guid id, [FromRoute] int x, [FromRoute] int y, CancellationToken ctx = default)
    {
        // TODO: Replace with proper responses
        return Ok(await _storageCaseService.RemoveContainerFromStorageCase(id, x, y, ctx));
    }
    
    [HttpGet("{id:guid}/lid")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLidInsertForCase([FromRoute] Guid id, CancellationToken ctx = default)
    {
        (MemoryStream? lidPdf, string? fileName) = await _storageCaseService.GenerateLidPdf(id, ctx);

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
    public async Task<IActionResult> GetLabelsForCase([FromRoute] Guid id, CancellationToken ctx = default)
    {
        (MemoryStream? labelsPdf, string? fileName) = await _storageCaseService.GenerateLabelsPdf(id, ctx);

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