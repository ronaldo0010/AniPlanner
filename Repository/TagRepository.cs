using AniPlannerApi.Data;
using Contracts;
using Entities.Models;

namespace Repository;

public class TagRepository : GenericRepo<Tag>, ITagRepository 
{
    public TagRepository(DataContext context) : base(context) { }
}