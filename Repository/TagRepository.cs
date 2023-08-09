using Contracts;
using Entities.Data;
using Entities.Models;

namespace Repository;

public class TagRepository : GenericRepo<Tag>, ITagRepository 
{
    public TagRepository(DataContext context) : base(context) { }
    public async Task<ICollection<Tag>> FindAllByMediaIdAsync(List<Guid> Media)
    {
        throw new NotImplementedException();
    }
}