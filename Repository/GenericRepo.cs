using System.Linq.Expressions;
using Contracts;
using Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class GenericRepo<T> : IGenericRepo<T> where T : class
{
    public DataContext _context { get; set; }

    public GenericRepo(DataContext context)
    {
        _context = context;
    }
    public IQueryable<T> FindAll()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
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