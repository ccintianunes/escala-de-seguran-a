namespace EscalaSeguranca.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    T? GetById(int id);
    bool Add(T entity);
    bool Update(T entity);
    void Remove(T entity);

}
