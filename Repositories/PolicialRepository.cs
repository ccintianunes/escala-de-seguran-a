using ApiCatalogo.Context;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

    public class PolicialRepository : Repository<Policial>, IPolicialRepository
    {

        public PolicialRepository(AppDbContext context, ILogger<PolicialRepository> logger) : base(context)
        {

        }        
    }
