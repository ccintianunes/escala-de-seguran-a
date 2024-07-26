using ApiCatalogo.Context;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Repositories;

    public class LocalRepository : Repository<Local>, ILocalRepository
    {

        public LocalRepository(AppDbContext context, ILogger<LocalRepository> logger) : base(context)
        {

        }        
    }