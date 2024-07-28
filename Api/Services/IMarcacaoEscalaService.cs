using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;
using EscalaSegurancaAPI.DTOs;

namespace EscalaSegurancaAPI.Services
{
    public interface IMarcacaoEscalaService
    {
        Task<IEnumerable<MarcacaoEscalaDTOResponse>> GetAll();
        Task<MarcacaoEscalaDTOResponse> GetById(int id);
        Task<bool> Create(MarcacaoEscala marcacaoEscala);
        Task<bool> Update(MarcacaoEscala marcacaoEscala);
        MarcacaoEscala Delete(MarcacaoEscala marcacaoEscala);
        Task<PagedList<MarcacaoEscalaDTOResponse>> GetAll(PagedParameters parameters);
        
    }
}
