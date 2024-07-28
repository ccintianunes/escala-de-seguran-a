using EscalaSeguranca.Repositories;
using EscalaSegurancaAPI.Filters;
using EscalaSegurancaAPI.Models;
using EscalaSegurancaAPI.DTOs;

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

        public async Task<IEnumerable<MarcacaoEscalaDTOResponse>> GetAll()
        {
            var marcacoesEscala = await _uof.MarcacaoEscalaRepository.GetAll();
            if (marcacoesEscala is null)
                throw new ArgumentNullException("Não existem marcações.");
            
            IList<MarcacaoEscalaDTOResponse> responseDTO = new List<MarcacaoEscalaDTOResponse>();
            foreach (MarcacaoEscala m in marcacoesEscala) {
                var marcacaoDTO = await CompletarMarcacaoEscala(m);
                responseDTO.Add(marcacaoDTO);
            }

            return responseDTO.AsEnumerable();
        }

        public async Task<PagedList<MarcacaoEscalaDTOResponse>> GetAll(PagedParameters parameters)
        {
            IEnumerable<MarcacaoEscalaDTOResponse> items = await GetAll();

            var marcacoesEscala = PagedList<MarcacaoEscalaDTOResponse>
                .ToPagedList(items.AsQueryable(), parameters.PageNumber, parameters.PageSize);

            if (marcacoesEscala is null)
                throw new ArgumentNullException("Não existem marcações.");

            return marcacoesEscala;
        }

        public async Task<MarcacaoEscalaDTOResponse> GetById(int id)
        {
            var marcacaoEscala = await _uof.MarcacaoEscalaRepository.GetById(id);
            if (marcacaoEscala is null)
                throw new ArgumentNullException("Marcação não encontrado.");

            return await CompletarMarcacaoEscala(marcacaoEscala);
        }

        public async Task<bool> Update(MarcacaoEscala marcacaoEscala)
        {
            var marcacaoDuplicada = await ExisteMarcacaoDuplicada(
                marcacaoEscala.PolicialId, 
                marcacaoEscala.EscalaId, 
                marcacaoEscala.MarcacaoEscalaId);
            if (marcacaoDuplicada)
                throw new InvalidOperationException("Conflito de escala!");
         
            var sucesso = _uof.MarcacaoEscalaRepository.Update(marcacaoEscala);
            _uof.Complete();

            return sucesso;
        }

        private async Task<MarcacaoEscalaDTOResponse> CompletarMarcacaoEscala(MarcacaoEscala marcacaoEscala){
            var escala = await _uof.EscalaRepository.GetById(marcacaoEscala.EscalaId);
            var policial = await _uof.PolicialRepository.GetById(marcacaoEscala.PolicialId);
            var local = await _uof.LocalRepository.GetById(marcacaoEscala.LocalId);

            var responseDTO = new MarcacaoEscalaDTOResponse(marcacaoEscala);
            responseDTO.PolicialNome = policial.Nome;
            responseDTO.DataHoraEntrada = escala.DataHoraEntrada;
            responseDTO.DataHoraSaida = escala.DataHoraSaida;
            responseDTO.LocalNome = local.Nome;

            return responseDTO;
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
                (m.DataHoraEntrada <= escala.DataHoraEntrada && m.DataHoraSaida >= escala.DataHoraEntrada) || 
                (m.DataHoraEntrada <= escala.DataHoraSaida && m.DataHoraSaida >= escala.DataHoraSaida)
            );

            return result;
        }
    }
}
