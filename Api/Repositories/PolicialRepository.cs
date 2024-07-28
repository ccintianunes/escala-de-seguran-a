using ApiCatalogo.Context;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

public class PolicialRepository : Repository<Policial>, IPolicialRepository
{

    public PolicialRepository(AppDbContext context, ILogger<PolicialRepository> logger) : base(context)
    {

    }

    public async Task<bool> IsCPFDuplicated(string CPF)
    {
        var policiais = await GetAll();
        return policiais.Any(p => p.CPF == CPF);
    }
}
