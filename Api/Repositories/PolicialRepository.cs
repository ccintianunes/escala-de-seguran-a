using ApiCatalogo.Context;
using EscalaSegurancaAPI.Filters;
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

        public async Task<PagedList<Policial>> GetPoliciaisFiltro(PoliciaisFiltro filtro)
        {
            IEnumerable<Policial> policiaisList = await GetAll();
            IQueryable<Policial> policiais = policiaisList.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                policiais = policiais
                    .Where(p => p.Nome.ToLower().Contains(filtro.Nome.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filtro.CPF)){
                policiais = policiais
                    .Where(p => p.CPF.ToLower().Contains(filtro.CPF.ToLower()));
            }

            return PagedList<Policial>.ToPagedList(policiais, filtro.PageNumber, filtro.PageSize);
        }
    }
