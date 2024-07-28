using System.Data;
using EscalaSeguranca.Repositories;
using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public class LocalService : ILocalService
    {
        private readonly IUnitOfWork _uof;
        public LocalService(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public async Task<bool> Create(Local local)
        {
            var nomeDuplicado = await ExisteNomeDuplicado(0, local.Nome);
            if (nomeDuplicado)
                throw new InvalidOperationException("Local já cadastrado.");

            var sucesso = await _uof.LocalRepository.Add(local);
            _uof.Complete();

            return sucesso;
        }

        public async Task<Local> Delete(Local local)
        {
            var marcacaoVinculada = await ExisteMarcacaoVinculada(local.LocalId);
            if (marcacaoVinculada)
                throw new InvalidOperationException("Existe marcacao vinculada");

            _uof.LocalRepository.Remove(local);
            _uof.Complete();

            return local;
        }

        public async Task<IEnumerable<Local>> GetAll()
        {
            var locais = await _uof.LocalRepository.GetAll();
            if (locais is null)
                throw new ArgumentNullException("Não existem locais.");

            return locais;
        }

        public async Task<PagedList<Local>> GetAll(PagedParameters parameters)
        {
            PagedList<Local> locais = await _uof.LocalRepository.Get(parameters);
            if (locais is null)
                throw new ArgumentNullException("Não existem locais.");

            return locais;
        }

        public async Task<Local> GetById(int id)
        {
            var local = await _uof.LocalRepository.GetById(id);
            if (local is null)
                throw new ArgumentNullException("Local não encontrado.");

            return local;
        }

        public async Task<bool> Update(Local local)
        {
            var nomeDuplicado = await ExisteNomeDuplicado(local.LocalId, local.Nome);
            if (nomeDuplicado)
                throw new DuplicateNameException("Local já cadastrado.");

            if (local.Inativado == true)
            {
                var marcacaoVinculada = await ExisteMarcacaoVinculada(local.LocalId);
                if (marcacaoVinculada)
                    throw new InvalidOperationException("Existe marcacao vinculada");
            }

            var sucesso = _uof.LocalRepository.Update(local);
            _uof.Complete();

            return sucesso;
        }

        private async Task<bool> ExisteNomeDuplicado(int id, string nome){
            var locais = await GetAll();
            if ( id == 0 )
                return locais.Any(l => l.Nome.Equals(nome, StringComparison.CurrentCultureIgnoreCase));
            
            return locais.Any(l => l.Nome == nome && l.LocalId != id);
        }

        private async Task<bool> ExisteMarcacaoVinculada(int id)
        {
            var marcacoes = await _uof.MarcacaoEscalaRepository.GetAll();
            
            return marcacoes.Any(m => m.LocalId == id);
        }
    }
}