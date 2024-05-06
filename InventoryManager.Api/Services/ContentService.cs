using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace InventoryManager.Api.Services;

public class ContentService : IContentService
{
    private readonly ILogger<ContentService> _logger;
    private readonly InventoryManagerContext _db;

    public ContentService(ILogger<ContentService> logger, InventoryManagerContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Guid> CreateContent(CreateContentRequestDto requestDto, CancellationToken ctx = default)
    {
        Content newContent = new()
        {
            Type = requestDto.Type,
            Standard = requestDto.Standard,
            Size = requestDto.Size,
            Length = requestDto.Length,
        };

        try
        {
            await _db.Contents.AddAsync(newContent, ctx);
            await _db.SaveChangesAsync(ctx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new content");
        }

        return newContent.Id;
    }

    public async Task<List<ContentReponseDto>> GetAllContents(bool withoutContainer = true, CancellationToken ctx = default)
    {
        IIncludableQueryable<Content,ICollection<Container>> queryable = _db.Contents.Include(x => x.Containers);

        if (withoutContainer)
        {
            return await queryable.Where(x => !x.Containers.Any()).Select(x => ContentMapper.ToContentResponseDto(x)).ToListAsync(ctx);
        }
        
        return await queryable.Select(x => ContentMapper.ToContentResponseDto(x)).ToListAsync(ctx);
    }

    public List<ContentTypeDto> GetContentTypes()
    {
        List<ContentTypeDto> sizes = new();

        foreach (int i in Enum.GetValues<ContentType>())
        {
            sizes.Add(new()
            {
                Index = i,
                Type = Enum.GetName(typeof(ContentType), i) ?? string.Empty,
            });
        }

        return sizes;
    }
}