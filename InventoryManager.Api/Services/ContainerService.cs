using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using InventoryManager.Reports;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

public class ContainerService : IContainerService
{
    private readonly ILogger<ContainerService> _logger;
    private readonly InventoryManagerContext _db;
    private readonly IReportGenerator _reportGenerator;

    public ContainerService(ILogger<ContainerService> logger, InventoryManagerContext db, IReportGenerator reportGenerator)
    {
        _logger = logger;
        _db = db;
        _reportGenerator = reportGenerator;
    }

    public async Task<List<ContainerOverviewResponseDto>> GetAllContainers(CancellationToken ctx = default)
    {
        return await _db.Containers
            .Include(x => x.Content)
            .ThenInclude(c => c.Standard)
            .Include(x => x.Position)
            .ThenInclude(p => p.Case)
            .ProjectToDto()
            .ToListAsync(ctx);
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

    public async Task<(MemoryStream?, string?)> GetContainerLabels(CancellationToken ctx = default)
    {
        List<Container> containers = await _db.Containers.Include(z => z.Content)
            .ThenInclude(c => c.Standard)
            .ToListAsync(ctx);

        if (!containers.Any())
        {
            return (null, null);
        }
        
        MemoryStream labelsSheet = _reportGenerator.GenerateContainerLabelsSheet(containers);

        labelsSheet.Position = 0;

        return (labelsSheet, $"{DateTime.Now:yyyy-MM-dd}-labels.pdf");
    }
}