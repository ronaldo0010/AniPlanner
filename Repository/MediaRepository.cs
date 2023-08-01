using System.Linq.Expressions;
using Contracts;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class MediaRepository : GenericRepo<Media>, IMediaRepository
{
    public MediaRepository(DataContext context) : base(context) { }
    public IQueryable<Media> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Media> FindByCondition(Expression<Func<Media, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public bool Create(Media entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(Media entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Media entity)
    {
        throw new NotImplementedException();
    }
}