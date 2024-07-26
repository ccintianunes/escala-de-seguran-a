using ApiCatalogo.Context;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

    public class MarcacaoEscalaRepository : Repository<MarcacaoEscala>, IMarcacaoEscalaRepository
    {

        public MarcacaoEscalaRepository(AppDbContext context, ILogger<MarcacaoEscalaRepository> logger) : base(context)
        {

        }        
    }