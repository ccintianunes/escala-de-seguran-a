using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public interface IPolicialService
    {
        Task<IEnumerable<Policial>> GetAll();
        Task<Policial> GetById(int id);
        Task<bool> Create(Policial policial);
        Task<bool> Update(Policial policial);
        Task<Policial> Delete(Policial policial);
        Task<PagedList<Policial>> GetAll(PagedParameters parameters);
        
    }
}