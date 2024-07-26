
using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;
using EscalaSegurancaAPI.Filters;

namespace EscalaSeguranca.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public bool Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return true;
    }

    public bool Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return true;
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public PagedList<T> Get(PagedParameters parameters)
    {
        IQueryable<T> items = _context.Set<T>().AsNoTracking().AsQueryable();
        return PagedList<T>
            .ToPagedList(items, parameters.PageNumber, parameters.PageSize);
    }
}
