using Contracts;
using Entities.Data;
using Entities.Models;

namespace Repository;

public class TagRepository : GenericRepo<Tag>, ITagRepository 
{
    public TagRepository(DataContext context) : base(context) { }
}