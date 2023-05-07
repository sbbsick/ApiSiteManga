using System.Linq.Expressions;
using TesteApi.Models;

namespace TesteApi.Repository;

public interface IRepository<T>
{
    IQueryable<T> Get();
    Task<T> GetById(Expression<Func<T, bool>> predicate);
    Task<string> ImgurImageUpload(IFormFile imageFile);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);

}