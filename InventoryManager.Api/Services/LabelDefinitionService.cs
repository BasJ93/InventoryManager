using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Domain.Enums;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Api.Services;

/// <inheritdoc />
public class LabelDefinitionService : ILabelDefinitionService
{
    private readonly InventoryManagerContext _db;

    public LabelDefinitionService(InventoryManagerContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public ICollection<LabelTypeDto> GetLabelTypes()
    {
        List<LabelTypeDto> sizes = new();

        foreach (int i in Enum.GetValues<LabelType>())
        {
            sizes.Add(new()
            {
                Index = i,
                Type = Enum.GetName(typeof(LabelType), i) ?? string.Empty,
            });
        }

        return sizes;
    }

    /// <inheritdoc />
    public async Task<LabelDefinitionDto?> GetLabelDefinition(Guid id, CancellationToken ctx = default)
    {
        return await _db.LabelDefinitions.ProjectToDto().FirstOrDefaultAsync(x => x.Id == id, ctx);
    }
}