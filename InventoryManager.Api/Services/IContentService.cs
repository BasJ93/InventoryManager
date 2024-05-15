using InventoryManager.Models;

namespace InventoryManager.Api.Services;

public interface IContentService
{
    Task<Guid> CreateContent(CreateContentRequestDto requestDto, CancellationToken ctx = default);

    Task<List<ContentResponseDto>> GetAllContents(bool withoutContainer = true, CancellationToken ctx = default);

    List<ContentTypeDto> GetContentTypes();
}