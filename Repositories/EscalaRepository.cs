using ApiCatalogo.Context;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

    public class EscalaRepository : Repository<Escala>, IEscalaRepository
    {

        public EscalaRepository(AppDbContext context, ILogger<EscalaRepository> logger) : base(context)
        {

        }        
    }