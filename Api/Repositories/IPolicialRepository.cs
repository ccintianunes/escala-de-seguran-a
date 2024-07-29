using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

public interface IPolicialRepository : IRepository<Policial>
{
    Task<bool> IsCPFDuplicated(string CPF);
    Task<PagedList<Policial>> GetPoliciaisFiltro(PoliciaisFiltro filtro);
}
