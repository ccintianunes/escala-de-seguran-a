using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EscalaSeguranca.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscalaSegurancaAPI.DTOs;

namespace EscalaSeguranca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcacaoEscalaController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ILogger<MarcacaoEscalaController> _logger;

        public MarcacaoEscalaController(IUnitOfWork uof, 
            IMapper mapper, ILogger<MarcacaoEscalaController> logger)
        {
            _uof = uof;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/MarcacaoEscala
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcacaoEscalaDTO>>> Get()
        {
            try
            {
            var marcacoesEscala = await _uof.MarcacaoEscalaRepository.GetAll();
            if (marcacoesEscala == null)
                return NotFound("Não existem marcações de escala.");

            var marcacoesEscalaDTO = _mapper.Map<IEnumerable<MarcacaoEscalaDTO>>(marcacoesEscala);
            return Ok(marcacoesEscalaDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar marcações.");
                return StatusCode(500);
            }
        }

        // GET: api/MarcacaoEscala/5
        [HttpGet("{id}")]
        public ActionResult<MarcacaoEscalaDTO> Get(int id)
        {
            try
            {
            var marcacaoEscala = _uof.MarcacaoEscalaRepository.GetById(id);

            if (marcacaoEscala == null)
            {
                return NotFound("Marcação não encontrada...");
            }

            var marcacaoEscalaDTO = _mapper.Map<MarcacaoEscalaDTO>(marcacaoEscala);
            return Ok(marcacaoEscalaDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar marcação.");
                return StatusCode(500);
            }
        }

        // POST: api/MarcacaoEscala
        [HttpPost]
        public ActionResult<MarcacaoEscalaDTO> Post(MarcacaoEscalaDTO marcacaoEscalaDTO)
        {
            if (marcacaoEscalaDTO == null)
                return BadRequest("Dados inválidos.");
            try
            {
            var marcacaoEscala = _mapper.Map<MarcacaoEscala>(marcacaoEscalaDTO);
            var sucesso = _uof.MarcacaoEscalaRepository.Add(marcacaoEscala);
            _uof.Complete();

            if (!sucesso)
                return StatusCode(500, "Erro ao criar marcação.");

            return CreatedAtAction(nameof(Get),
                new { id = marcacaoEscala.MarcacaoEscalaId }, marcacaoEscalaDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir marcação.");
                return StatusCode(500);
            }
        }

        // PUT: api/MarcacaoEscala/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, MarcacaoEscalaDTO marcacaoEscalaDTO)
        {
            if (id != marcacaoEscalaDTO.MarcacaoEscalaId)
                return BadRequest("Dados inválidos.");

            try
            {
                var marcacaoEscalaExistente = _uof.MarcacaoEscalaRepository.GetById(id);
                if (marcacaoEscalaExistente == null)
                    return NotFound("Marcação de escala não encontrada...");

                var marcacaoEscala = _mapper.Map(marcacaoEscalaDTO, marcacaoEscalaExistente);

                var sucesso = _uof.MarcacaoEscalaRepository.Update(marcacaoEscala);
                _uof.Complete();

                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar a marcação de escala.");

                var marcacaoEscalaAtualizadaDto = _mapper.Map<MarcacaoEscalaDTO>(marcacaoEscala);

                return Ok(marcacaoEscalaAtualizadaDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar marcação de escala.");
                return StatusCode(500);
            }
        }

        // DELETE: api/MarcacaoEscala/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
            var marcacaoEscala = _uof.MarcacaoEscalaRepository.GetById(id);
            if (marcacaoEscala == null)
            {
                return NotFound();
            }

            _uof.MarcacaoEscalaRepository.Remove(marcacaoEscala);
            _uof.Complete();

            return Ok(marcacaoEscala);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir marcação escala.");
                return StatusCode(500);
            }
        }
    }
}
