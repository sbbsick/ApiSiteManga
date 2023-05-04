using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TesteApi.Context;
using TesteApi.Models;

namespace TesteApi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MyContext _context;


    protected Repository(MyContext context)
    {
        _context = context;

    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> Get()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public async Task<T> GetById(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }
}