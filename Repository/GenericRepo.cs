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