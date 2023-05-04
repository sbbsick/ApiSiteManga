using System.Linq.Expressions;
using TesteApi.Models;

namespace TesteApi.Repository;

public interface IRepository<T>
{
    public IQueryable<T> Get();
    Task<T> GetById(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}