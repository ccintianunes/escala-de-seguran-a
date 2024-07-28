
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
        var entities = await _context.Set<T>().ToListAsync();
        var propertyInfo = typeof(T).GetProperty("Inativado");

        if (propertyInfo is null)
            return entities;

        return entities.Where(e => 
        {
            var value = propertyInfo.GetValue(e);
            return value is null || !(bool)value;
        }).ToList();
    }

    public async Task<T?> GetById(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        var propertyInfo = typeof(T).GetProperty("Inativado");

        if (propertyInfo is null)
            return entity;

        var value = propertyInfo.GetValue(entity);

        if ((bool)value)
            return null;

        return entity;
    }

    public async Task<bool> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
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

    public async Task<PagedList<T>> Get(PagedParameters parameters)
    {
        IEnumerable<T> items = await GetAll();

        return PagedList<T>
            .ToPagedList(items.AsQueryable(), parameters.PageNumber, parameters.PageSize);
    }
}
