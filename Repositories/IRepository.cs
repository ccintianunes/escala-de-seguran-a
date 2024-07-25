using EscalaSegurancaAPI.Filters;

namespace EscalaSeguranca.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<bool> Add(T entity);
    bool Update(T entity);
    void Remove(T entity);
    PagedList<T> Get(PagedParameters parameters);
}
