using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EscalaSeguranca.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscalaSegurancaAPI.DTOs;
using EscalaSegurancaAPI.Models;
using EscalaSegurancaAPI.Filters;
using Newtonsoft.Json;

namespace EscalaSeguranca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscalaController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ILogger<EscalaController> _logger;

        public EscalaController(IUnitOfWork uof,
            IMapper mapper, ILogger<EscalaController> logger)
        {
            _uof = uof;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Escala
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscalaDTO>>> Get()
        {
            try
            {
            var escalas = await _uof.EscalaRepository.GetAll();
            if (escalas == null)
                return NotFound("Não existem escalas.");

            var escalasDTO = _mapper.Map<IEnumerable<EscalaDTO>>(escalas);
            return Ok(escalasDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escalas.");
                return StatusCode(500);
            }
        }

        // GET: api/Escala/5
        [HttpGet("{id}")]
        public ActionResult<EscalaDTO> Get(int id)
        {
            try
            {
            var escala = _uof.EscalaRepository.GetById(id);

            if (escala == null)
            {
                return NotFound("Escala não encontrada...");
            }

            var escalaDTO = _mapper.Map<EscalaDTO>(escala);
            return Ok(escalaDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escala.");
                return StatusCode(500);
            }
        }

        // POST: api/Escala
        [HttpPost]
        public ActionResult<EscalaDTO> Post(EscalaDTO escalaDTO)
        {
            if (escalaDTO is null)
                return BadRequest("Dados inválidos.");

            try
            {
            var escala = _mapper.Map<Escala>(escalaDTO);
            var sucesso = _uof.EscalaRepository.Add(escala);
            _uof.Complete();

            if(!sucesso)
                return StatusCode(500, "Erro ao criar escala.");

            return CreatedAtAction(nameof(Get),
                new { id = escala.EscalaId }, escalaDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir escala.");
                return StatusCode(500);
            }
        }

        // PUT: api/Escala/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, EscalaDTO escalaDTO)
        {
            if (id != escalaDTO.EscalaId)
                return BadRequest("Dados inválidos.");

            try
            {
                var escalaExistente = _uof.EscalaRepository.GetById(id);
                if (escalaExistente == null)
                    return NotFound("Escala não encontrada...");

                var escala = _mapper.Map(escalaDTO, escalaExistente);

                var sucesso = _uof.EscalaRepository.Update(escala);
                _uof.Complete();

                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar a escala.");

                var escalaAtualizadaDto = _mapper.Map<EscalaDTO>(escala);

                return Ok(escalaAtualizadaDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar escala.");
                return StatusCode(500);
            }
        }

        // DELETE: api/Escala/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
            var escala = _uof.EscalaRepository.GetById(id);
            if (escala == null)
            {
                return NotFound("Escala não encontrada...");
            }

            _uof.EscalaRepository.Remove(escala);
            _uof.Complete();

            return Ok(escala);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir escala.");
                return StatusCode(500);
            }
        }

        // GET: api/Escala/pagination
        [HttpGet("pagination")]
        public ActionResult<IEnumerable<EscalaDTO>> Get([FromQuery] PagedParameters parameters)
        {
            try
            {
                PagedList<Escala> escalas = _uof.EscalaRepository.Get(parameters);
                if (escalas == null)
                    return NotFound("Não existem escalas.");

                return ObterEscalas(escalas);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escalas.");
                return StatusCode(500);
            }
        }

        private ActionResult<IEnumerable<EscalaDTO>> ObterEscalas(PagedList<Escala> escalas)
        {
            var metadata = new
            {
                escalas.TotalCount,
                escalas.PageSize,
                escalas.CurrentPage,
                escalas.TotalPages,
                escalas.HasNext,
                escalas.HasPrevious
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var escalasDTO = _mapper.Map<IEnumerable<EscalaDTO>>(escalas);
            return Ok(escalasDTO);
        }
    }
}
