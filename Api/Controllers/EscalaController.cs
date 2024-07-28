using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EscalaSegurancaAPI.DTOs;
using EscalaSegurancaAPI.Models;
using EscalaSegurancaAPI.Filters;
using Newtonsoft.Json;
using EscalaSegurancaAPI.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace EscalaSeguranca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscalaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<EscalaController> _logger;
        private readonly IEscalaService _service;

        public EscalaController(IEscalaService service,
            IMapper mapper, ILogger<EscalaController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }

        // GET: api/Escala
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscalaDTO>>> Get()
        {
            try
            {
                var escalas = await _service.GetAll();
                var escalasDTO = _mapper.Map<IEnumerable<EscalaDTO>>(escalas);

                return Ok(escalasDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem escalas.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escalas.");
                return StatusCode(500);
            }
        }

        // GET: api/Escala/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EscalaDTO>> Get(int id)
        {
            try
            {
                var escala = await _service.GetById(id);
                var escalaDTO = _mapper.Map<EscalaDTO>(escala);

                return Ok(escalaDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Escala não encontrada.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escala.");
                return StatusCode(500);
            }
        }

        // POST: api/Escala
        [HttpPost]
        public async Task<ActionResult<EscalaDTO>> Post(EscalaDTO escalaDTO)
        {
            if (escalaDTO is null)
                return BadRequest("Dados inválidos.");

            try
            {
                var escala = _mapper.Map<Escala>(escalaDTO);
                var sucesso = await _service.Create(escala);

                if (!sucesso)
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
        public async Task<IActionResult> Put(int id, EscalaDTO escalaDTO)
        {
            if (id != escalaDTO.EscalaId)
                return BadRequest("Dados inválidos.");

            try
            {
                var escalaExistente = await _service.GetById(id);
                var escala = _mapper.Map(escalaDTO, escalaExistente);
                var sucesso = await _service.Update(escala);

                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar a escala.");

                var escalaAtualizadaDto = _mapper.Map<EscalaDTO>(escala);

                return Ok(escalaAtualizadaDto);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Escala não encontrada.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Escala possui marcação ativa vinculada.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar escala.");
                return StatusCode(500);
            }
        }

        // DELETE: api/Escala/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var escala = await _service.GetById(id);
                await _service.Delete(escala);

                return Ok(escala);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Escala não encontrada.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Escala possui marcação ativa vinculada.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir escala.");
                return StatusCode(500);
            }
        }

        // GET: api/Escala/pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<EscalaDTO>>> Get([FromQuery] PagedParameters parameters)
        {
            try
            {
                PagedList<Escala> escalas = await _service.GetAll(parameters);
                return ObterEscalas(escalas);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem escalas.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escalas.");
                return StatusCode(500);
            }
        }

        // GET: api/Escala/5/UpdatePartial
        [HttpPatch("{id}/UpdatePartial")]
        public async Task<ActionResult<EscalaDTO>> Patch(int id, JsonPatchDocument<InativadoDTOPatch> patchDTO)
        {
            if (patchDTO is null)
                return BadRequest();

            try
            {
                var escala = await _service.GetById(id);
                var escalaUpdateRequest = _mapper.Map<InativadoDTOPatch>(escala);
                patchDTO.ApplyTo(escalaUpdateRequest, ModelState);

                if(!(ModelState.IsValid || TryValidateModel(escalaUpdateRequest)))
                    return BadRequest(ModelState);
                
                _mapper.Map(escalaUpdateRequest, escala);
                await _service.Update(escala);

                return Ok(_mapper.Map<InativadoDTOPatch>(escala));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Escala não encontrada.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Escala possui marcação ativa vinculada.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao inativar escala.");
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