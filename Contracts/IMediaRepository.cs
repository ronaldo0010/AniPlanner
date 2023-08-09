using Entities.Models;

namespace Contracts;

public interface IMediaRepository : IGenericRepo<Media>
{
    Task<List<Media>> FindBatchWithTagsAsync();
}