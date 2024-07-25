using EscalaSeguranca.Repositories;
using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public class PolicialService : IPolicialService
    {
        private readonly IUnitOfWork _uof;
        public PolicialService(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public Task<bool> Create(Policial policial)
        {
            var sucesso = _uof.PolicialRepository.Add(policial);
            _uof.Complete();

            return sucesso;
        }

        public Policial Delete(Policial policial)
        {
            _uof.PolicialRepository.Remove(policial);
            _uof.Complete();

            return policial;
        }

        public async Task<IEnumerable<Policial>> GetAll()
        {
            var policiais = await _uof.PolicialRepository.GetAll();
            if (policiais is null)
                throw new ArgumentNullException("Não existem policials.");

            return policiais;
        }

        public PagedList<Policial> GetAll(PagedParameters parameters)
        {
            PagedList<Policial> policiais = _uof.PolicialRepository.Get(parameters);
            if (policiais == null)
                throw new ArgumentNullException("Não existem policials.");

            return policiais;
        }

        public async Task<Policial> GetById(int id)
        {
            var policial = await _uof.PolicialRepository.GetById(id);
            if (policial is null)
                throw new ArgumentNullException("Policial não encontrado.");

            return policial;
        }

        public async Task<bool> Update(Policial policial)
        {
            var duplicatedCPF = await this.isCPFDuplicatedAsync(policial.PolicialId, policial.CPF);
            if (duplicatedCPF)
                throw new InvalidOperationException("CPF já cadastrado");

            var sucesso = _uof.PolicialRepository.Update(policial);
            _uof.Complete();

            return sucesso;
        }

        private async Task<bool> isCPFDuplicatedAsync(int id, string CPF)
        {
            var policiais = await this.GetAll();
            var duplicatedCPF = policiais.Any(p => p.PolicialId != id && p.CPF == CPF);
            return duplicatedCPF;
        }
    }
}