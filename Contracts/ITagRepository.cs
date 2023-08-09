using Entities.Models;

namespace Contracts;

public interface ITagRepository : IGenericRepo<Tag>
{
    Task<ICollection<Tag>> FindAllByMediaIdAsync(List<Guid> mediaIds);
}