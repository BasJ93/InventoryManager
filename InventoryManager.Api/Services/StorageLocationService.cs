using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using InventoryManager.Reports;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

public class StorageLocationService : IStorageLocationService
{
    private readonly ILogger<StorageLocationService> _logger;
    private readonly InventoryManagerContext _db;
    private readonly IReportGenerator _reportGenerator;

    public StorageLocationService(ILogger<StorageLocationService> logger, InventoryManagerContext db,
        IReportGenerator reportGenerator)
    {
        _logger = logger;
        _db = db;
        _reportGenerator = reportGenerator;
    }

    public async Task<List<GetStorageLocationsResponseDto>> GetStorageLocations(CancellationToken ctx = default)
    {
        return await _db.StorageCases.Select(x => StorageCaseMapper.ToGetStorageLocationsResponseDto(x))
            .ToListAsync(ctx);
    }

    public async Task<GetStorageLocationResponseDto?> GetStorageLocation(Guid id, CancellationToken ctx = default)
    {
        StorageLocation? storageCase = await _db.StorageCases.Where(x => x.Id == id)
            .Include(x => x.Containers)
            .ThenInclude(y => y.Container)
            .ThenInclude(z => z.Content)
            .ThenInclude(c => c.Standard)
            .FirstOrDefaultAsync(ctx);

        if (storageCase == default)
        {
            return null;
        }

        return StorageCaseMapper.ToGetStorageLocationReponseDto(storageCase);
    }

    public List<StorageLocationTypeDto> GetStorageLocationTypes()
    {
        List<StorageLocationTypeDto> sizes = new();

        foreach (int i in Enum.GetValues<StorageLocationType>())
        {
            sizes.Add(new()
            {
                Index = i,
                Type = Enum.GetName(typeof(StorageLocationType), i) ?? string.Empty,
            });
        }

        return sizes;
    }

    public async Task<bool> PlaceContainerInStorageLocation(Guid id, int x, int y, Guid containerId,
        CancellationToken ctx = default)
    {
        Container? container = await _db.Containers.FirstOrDefaultAsync(o => o.Id == containerId, ctx);

        if (container == default)
        {
            return false;
        }

        StorageLocation? storageCase = await _db.StorageCases.Where(o => o.Id == id).Include(o => o.Containers)
            .FirstOrDefaultAsync(ctx);

        if (storageCase == default)
        {
            return false;
        }

        StorageLocationContainerPosition? existingPosition =
            storageCase.Containers.FirstOrDefault(o => o.ContainerId == container.Id);

        if (existingPosition != null)
        {
            existingPosition.PositionX = x;
            existingPosition.PositionY = y;

            await _db.SaveChangesAsync(ctx);

            return true;
        }

        if (!storageCase.Containers.Any(o => o.PositionX == x && o.PositionY == y))
        {
            // TODO: Check for overlapping containers

            storageCase.Containers.Add(new()
            {
                Location = storageCase,
                StorageLocationId = storageCase.Id,
                Container = container,
                ContainerId = containerId,
                PositionX = x,
                PositionY = y
            });

            await _db.SaveChangesAsync(ctx);

            return true;
        }

        return false;
    }

    public async Task<bool> RemoveContainerFromStorageLocation(Guid id, int x, int y, CancellationToken ctx = default)
    {
        StorageLocation? storageCase = await _db.StorageCases.Where(o => o.Id == id).Include(o => o.Containers)
            .FirstOrDefaultAsync(ctx);

        if (storageCase == default)
        {
            return false;
        }

        StorageLocationContainerPosition? existingPosition =
            storageCase.Containers.FirstOrDefault(o => o.PositionX == x && o.PositionY == y);

        if (existingPosition != null)
        {
            storageCase.Containers.Remove(existingPosition);

            await _db.SaveChangesAsync(ctx);

            return true;
        }

        return false;
    }

    public async Task<(MemoryStream?, string?)> GenerateLabelsPdf(Guid id, CancellationToken ctx = default)
    {
        StorageLocation? storageCase = await _db.StorageCases.Where(x => x.Id == id)
            .Include(x => x.Containers)
            .ThenInclude(y => y.Container)
            .ThenInclude(z => z.Content)
            .ThenInclude(c => c.Standard)
            .FirstOrDefaultAsync(ctx);

        if (storageCase == default)
        {
            return (null, null);
        }

        MemoryStream labelsSheet = _reportGenerator.GenerateContainerLabelsSheet(storageCase);

        labelsSheet.Position = 0;

        return (labelsSheet, $"{DateTime.Now:yyyy-MM-dd}-{storageCase.Name}-labels.pdf");
    }

    public async Task<(MemoryStream?, string?)> GenerateLidPdf(Guid id, CancellationToken ctx = default)
    {
        StorageLocation? storageCase = await _db.StorageCases.Where(x => x.Id == id)
            .Include(x => x.Containers)
            .ThenInclude(y => y.Container)
            .ThenInclude(z => z.Content)
            .ThenInclude(c => c.Standard)
            .FirstOrDefaultAsync(ctx);

        if (storageCase == default)
        {
            return (null, null);
        }

        MemoryStream lidSheet = _reportGenerator.GenerateCaseLidSheet(storageCase);

        lidSheet.Position = 0;

        return (lidSheet, $"{DateTime.Now:yyyy-MM-dd}-{storageCase.Name}-lid.pdf");
    }

    public async Task<string> GetStorageLocationName(Guid id, CancellationToken ctx = default)
    {
        string? name = await _db.StorageCases.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync(ctx);

        return name ?? string.Empty;
    }

    public async Task<Guid> CreateStorageLocation(CreateStorageLocationRequestDto requestDto,
        CancellationToken ctx = default)
    {
        _logger.LogInformation("Creating new storage location [{name}]", requestDto.Name);

        StorageLocation newLocation = new()
        {
            Name = requestDto.Name,
            SizeX = requestDto.SizeX,
            SizeY = requestDto.SizeY,
            Type = requestDto.Type,
        };

        try
        {
            await _db.StorageCases.AddAsync(newLocation, ctx);
            await _db.SaveChangesAsync(ctx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during storage location creation.");
        }

        return newLocation.Id;
    }
}