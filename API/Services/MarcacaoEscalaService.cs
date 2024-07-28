using EscalaSeguranca.Repositories;
using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.Services
{
    public class MarcacaoEscalaService : IMarcacaoEscalaService
    {
        private readonly IUnitOfWork _uof;
        public MarcacaoEscalaService(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public async Task<bool> Create(MarcacaoEscala marcacaoEscala)
        {
            marcacaoEscala = await CompletarMarcacaoEscala(marcacaoEscala);

            var marcacaoDuplicada = await ExisteMarcacaoDuplicada(marcacaoEscala.PolicialId, marcacaoEscala.EscalaId, 0);
            if (marcacaoDuplicada)
                throw new InvalidOperationException("Conflito de escala!");

            var sucesso = await _uof.MarcacaoEscalaRepository.Add(marcacaoEscala);
            _uof.Complete();

            return sucesso;
        }

        public MarcacaoEscala Delete(MarcacaoEscala marcacaoEscala)
        {
            _uof.MarcacaoEscalaRepository.Remove(marcacaoEscala);
            _uof.Complete();

            return marcacaoEscala;
        }

        public async Task<IEnumerable<MarcacaoEscala>> GetAll()
        {
            var marcacoesEscala = await _uof.MarcacaoEscalaRepository.GetAll();
            if (marcacoesEscala is null)
                throw new ArgumentNullException("Não existem marcações.");

            return marcacoesEscala;
        }

        public async Task<PagedList<MarcacaoEscala>> GetAll(PagedParameters parameters)
        {
            PagedList<MarcacaoEscala> marcacoesEscala = await _uof.MarcacaoEscalaRepository.Get(parameters);
            if (marcacoesEscala is null)
                throw new ArgumentNullException("Não existem marcações.");

            return marcacoesEscala;
        }

        public async Task<MarcacaoEscala> GetById(int id)
        {
            var marcacaoEscala = await _uof.MarcacaoEscalaRepository.GetById(id);
            if (marcacaoEscala is null)
                throw new ArgumentNullException("Marcação não encontrado.");

            return marcacaoEscala;
        }

        public async Task<bool> Update(MarcacaoEscala marcacaoEscala)
        {
            var marcacaoDuplicada = await ExisteMarcacaoDuplicada(
                marcacaoEscala.PolicialId, 
                marcacaoEscala.EscalaId, 
                marcacaoEscala.MarcacaoEscalaId);
            if (marcacaoDuplicada)
                throw new InvalidOperationException("Conflito de escala!");
         
            marcacaoEscala = await CompletarMarcacaoEscala(marcacaoEscala);
            
            var sucesso = _uof.MarcacaoEscalaRepository.Update(marcacaoEscala);
            _uof.Complete();

            return sucesso;
        }

        private async Task<MarcacaoEscala> CompletarMarcacaoEscala(MarcacaoEscala marcacaoEscala){
            var escala = await _uof.EscalaRepository.GetById(marcacaoEscala.EscalaId);
            marcacaoEscala.Escala = escala;

            var policial = await _uof.PolicialRepository.GetById(marcacaoEscala.PolicialId);
            marcacaoEscala.Policial = policial;

            var local = await _uof.LocalRepository.GetById(marcacaoEscala.LocalId);
            marcacaoEscala.Local = local;

            if (policial is null || local is null || escala is null)
                throw new InvalidOperationException("Dados inválidos");

            return marcacaoEscala;
        }

        private async Task<bool> ExisteMarcacaoDuplicada(int policialId, int escalaId, int id){
            var escala = await _uof.EscalaRepository.GetById(escalaId);
            if (escala is null)
                throw new ArgumentNullException("Escala não encontrada.");

            var marcacoes = await GetAll();
            marcacoes = marcacoes.Where(m => m.PolicialId == policialId);
            if(id != 0)
                marcacoes = marcacoes.Where(m => m.MarcacaoEscalaId != id);

            var result = marcacoes.Any(m => 
                (m.Escala.DataHoraEntrada < escala.DataHoraEntrada && m.Escala.DataHoraSaida > escala.DataHoraEntrada) || 
                (m.Escala.DataHoraEntrada < escala.DataHoraSaida && m.Escala.DataHoraSaida > escala.DataHoraSaida)
            );

            return result;
        }
    }
}