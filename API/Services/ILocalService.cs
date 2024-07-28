using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public interface ILocalService
    {
        Task<IEnumerable<Local>> GetAll();
        Task<Local> GetById(int id);
        Task<bool> Create(Local local);
        Task<bool> Update(Local local);
        Task<Local> Delete(Local local);
        Task<PagedList<Local>> GetAll(PagedParameters parameters);
    }
}