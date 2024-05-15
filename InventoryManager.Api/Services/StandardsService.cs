using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

public class StandardsService : IStandardsService
{
    private readonly ILogger<StandardsService> _logger;
    private readonly InventoryManagerContext _db;

    public StandardsService(ILogger<StandardsService> logger, InventoryManagerContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<List<StandardResponseDto>> GetAllStandards(CancellationToken ctx = default)
    {
        return await _db.Standards.ProjectToDto().ToListAsync(ctx);
    }

    public async Task<Guid> CreateStandard(CreateStandardRequestDto requestDto, CancellationToken ctx = default)
    {
        throw new NotImplementedException();
    }
}