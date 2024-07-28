using EscalaSeguranca.Repositories;
using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public class EscalaService : IEscalaService
    {
        private readonly IUnitOfWork _uof;
        public EscalaService(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public async Task<bool> Create(Escala escala)
        {
            if (escala.DataHoraSaida <= escala.DataHoraEntrada)
                throw new InvalidOperationException("Horário de saída deve ser maior que o horário de entrada.");

            var sucesso = await _uof.EscalaRepository.Add(escala);
            _uof.Complete();

            return sucesso;
        }

        public async Task<Escala> Delete(Escala escala)
        {
            var marcacaoVinculada = await ExisteMarcacaoVinculada(escala.EscalaId);
            if (marcacaoVinculada)
                throw new InvalidOperationException("Existe marcacao vinculada");

            _uof.EscalaRepository.Remove(escala);
            _uof.Complete();

            return escala;
        }

        public async Task<IEnumerable<Escala>> GetAll()
        {
            var escalas = await _uof.EscalaRepository.GetAll();
            if (escalas is null)
                throw new ArgumentNullException("Não existem escalas.");

            return escalas;
        }

        public async Task<PagedList<Escala>> GetAll(PagedParameters parameters)
        {
            PagedList<Escala> escalas = await _uof.EscalaRepository.Get(parameters);
            if (escalas is null)
                throw new ArgumentNullException("Não existem escalas.");

            return escalas;
        }

        public async Task<Escala> GetById(int id)
        {
            var escala = await _uof.EscalaRepository.GetById(id);
            if (escala is null)
                throw new ArgumentNullException("Escala não encontrado.");

            return escala;
        }

        public async Task<bool> Update(Escala escala)
        {
            if (escala.Inativado == true )
            {
                var marcacaoVinculada = await ExisteMarcacaoVinculada(escala.EscalaId);
                if (marcacaoVinculada)
                    throw new InvalidOperationException("Existe marcacao vinculada");
            }

            var sucesso = _uof.EscalaRepository.Update(escala);
            _uof.Complete();

            return sucesso;
        }

        private async Task<bool> ExisteMarcacaoVinculada(int escalaId)
        {
            var marcacoes = await _uof.MarcacaoEscalaRepository.GetAll();
            
            return marcacoes.Any(m => m.EscalaId == escalaId);
        }
    }
}