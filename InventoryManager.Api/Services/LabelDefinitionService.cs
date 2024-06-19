using InventoryManager.Api.Mappers;
using InventoryManager.Database;
using InventoryManager.Domain;
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
    public async Task<ICollection<LabelDefinitionDto>> GetLabelDefinitions(CancellationToken ctx = default)
    {
        return await _db.LabelDefinitions.ProjectToDto().AsNoTracking().ToListAsync(ctx);
    }

    /// <inheritdoc />
    public async Task<LabelDefinitionDto?> GetLabelDefinition(Guid id, CancellationToken ctx = default)
    {
        return await _db.LabelDefinitions.ProjectToDto().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ctx);
    }

    public async Task<LabelDefinitionDto?> SetLabelDefinition(Guid id, LabelDefinitionDto definition, CancellationToken ctx = default)
    {
        LabelDefinition? labelDefinition = await _db.LabelDefinitions.FirstOrDefaultAsync(x => x.Id == id, ctx);

        if (labelDefinition == default)
        {
            return null;
        }

        labelDefinition.Name = definition.Name;
        labelDefinition.Type = (LabelType)definition.Type;
        labelDefinition.CommandText = definition.CommandText;

        if (await _db.SaveChangesAsync(ctx) > 0)
        {
            return labelDefinition.ToDto();
        }

        return null;
    }
}