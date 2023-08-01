using System.Linq.Expressions;
using AniPlannerApi.Data;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class GenericRepo<T> : IGenericRepo<T> where T : class
{
    public DataContext _context { get; set; }
    public DbSet<T> _tabel { get; set; }

    public GenericRepo(DataContext context)
    {
        _context = context;
        _tabel = context.Set<T>();
    }
    public IQueryable<T> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public bool Create(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }
}