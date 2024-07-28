using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

public interface IPolicialRepository : IRepository<Policial>
{
    Task<bool> IsCPFDuplicated(string CPF);
}
