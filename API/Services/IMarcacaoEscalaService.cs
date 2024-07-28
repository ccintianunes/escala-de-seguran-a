using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public interface IMarcacaoEscalaService
    {
        Task<IEnumerable<MarcacaoEscala>> GetAll();
        Task<MarcacaoEscala> GetById(int id);
        Task<bool> Create(MarcacaoEscala marcacaoEscala);
        Task<bool> Update(MarcacaoEscala marcacaoEscala);
        MarcacaoEscala Delete(MarcacaoEscala marcacaoEscala);
        Task<PagedList<MarcacaoEscala>> GetAll(PagedParameters parameters);
        
    }
}