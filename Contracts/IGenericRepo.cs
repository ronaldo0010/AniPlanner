using System.Linq.Expressions;

namespace Contracts;

public interface IGenericRepo<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    bool Create(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    //T? FindOne(Expression<Func<T, bool>> expression);
}
