using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public interface IEscalaService
    {
        Task<IEnumerable<Escala>> GetAll();
        Task<Escala> GetById(int id);
        Task<bool> Create(Escala escala);
        Task<bool> Update(Escala escala);
        Task<Escala> Delete(Escala escala);
        Task<PagedList<Escala>> GetAll(PagedParameters parameters);
        
    }
}