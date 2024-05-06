using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

public class ContainerService : IContainerService
{
    private readonly ILogger<ContainerService> _logger;
    private readonly InventoryManagerContext _db;

    public ContainerService(ILogger<ContainerService> logger, InventoryManagerContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<List<ContainerResponseDto>> GetAllContainers(CancellationToken ctx = default)
    {
        return await _db.Containers.Select(x => ContainerMapper.ToContainerResponseDto(x)).ToListAsync(ctx);
    }

    public List<ContainerSizeDto> GetContainerSizes()
    {
        List<ContainerSizeDto> sizes = new();

        foreach (int i in Enum.GetValues<ContainerSize>())
        {
            sizes.Add(new()
            {
                Index = i,
                Size = Enum.GetName(typeof(ContainerSize), i) ?? string.Empty,
            });
        }

        return sizes;
    }

    public async Task<List<ContainerWithLocationResponseDto>> GetUnplacedContainers(CancellationToken ctx = default)
    {
        return await _db.Containers.Where(x => x.Position == null).Include(x => x.Content).Select(x => ContainerMapper.ContainerToResponseWithLocationDto(x)).ToListAsync(ctx);
    }

    public async Task<ContainerResponseDto?> GetContainer(Guid id, CancellationToken ctx = default)
    {
        Container? container = await _db.Containers.Where(x => x.Id == id)
            .FirstOrDefaultAsync(ctx);

        if (container == default)
        {
            return null;
        }

        return ContainerMapper.ToContainerResponseDto(container);
    }

    public async Task<Guid> CreateContainer(CreateContainerRequestDto requestDto, CancellationToken ctx = default)
    {
        // TODO: Validate the content actually exists.
        
        Container newContainer = new()
        {
            ContentId = requestDto.ContentId,
            Size = requestDto.Size
        };

        try
        {
            await _db.Containers.AddAsync(newContainer, ctx);
            await _db.SaveChangesAsync(ctx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating container");
        }

        return newContainer.Id;
    }
}