using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public interface IPolicialService
    {
        public Task<IEnumerable<Policial>> GetAll();
        public Task<Policial> GetById(int id);
        public Task<bool> Create(Policial policial);
        public Task<bool> Update(Policial policial);
        public Policial Delete(Policial policial);
        public PagedList<Policial> GetAll(PagedParameters parameters);
    }
}