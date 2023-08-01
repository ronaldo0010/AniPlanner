using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TagRepository : GenericRepo<Tag>, ITagRepository 
{
    public TagRepository(DbContext context) : base(context) { }
}