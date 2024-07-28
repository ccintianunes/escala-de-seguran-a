using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EscalaSeguranca.Repositories;
using EscalaSegurancaAPI.DTOs;
using EscalaSegurancaAPI.Filters;
using Newtonsoft.Json;
using EscalaSegurancaAPI.Models;
using EscalaSegurancaAPI.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace EscalaSeguranca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcacaoEscalaController : ControllerBase
    {
        private readonly IMarcacaoEscalaService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<MarcacaoEscalaController> _logger;

        public MarcacaoEscalaController(IMarcacaoEscalaService service,
            IMapper mapper, ILogger<MarcacaoEscalaController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/MarcacaoEscala
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcacaoEscalaDTO>>> Get()
        {
            try
            {
                var marcacoesEscala = await _service.GetAll();
                var marcacoesEscalaDTO = _mapper.Map<IEnumerable<MarcacaoEscalaDTO>>(marcacoesEscala);

                return Ok(marcacoesEscalaDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem marcações.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar marcações.");
                return StatusCode(500);
            }
        }

        // GET: api/MarcacaoEscala/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcacaoEscalaDTO>> Get(int id)
        {
            try
            {
                var marcacaoEscala = await _service.GetById(id);
                var marcacaoEscalaDTO = _mapper.Map<MarcacaoEscalaDTO>(marcacaoEscala);

                return Ok(marcacaoEscalaDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Marcação não encontrada.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar marcação.");
                return StatusCode(500);
            }
        }

        // POST: api/MarcacaoEscala
        [HttpPost]
        public async Task<ActionResult<MarcacaoEscalaDTO>> Post(MarcacaoEscalaDTO marcacaoEscalaDTO)
        {
            if (marcacaoEscalaDTO is null)
                return BadRequest("Dados inválidos.");
            try
            {
                MarcacaoEscala marcacaoEscala = _mapper.Map<MarcacaoEscala>(marcacaoEscalaDTO);
                var sucesso = await _service.Create(marcacaoEscala);

                if (!sucesso)
                    return StatusCode(500, "Erro ao criar marcação.");

                return CreatedAtAction(nameof(Get),
                    new { id = marcacaoEscala.MarcacaoEscalaId }, marcacaoEscalaDTO);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Conflito de escala.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir marcação.");
                return StatusCode(500);
            }
        }

        // PUT: api/MarcacaoEscala/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MarcacaoEscalaDTO marcacaoEscalaDTO)
        {
            if (id != marcacaoEscalaDTO.MarcacaoEscalaId)
                return BadRequest("Dados inválidos.");

            try
            {
                var marcacaoEscalaExistente = await _service.GetById(id);
                var marcacaoEscala = _mapper.Map(marcacaoEscalaDTO, marcacaoEscalaExistente);

                var sucesso = await _service.Update(marcacaoEscala);

                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar a marcação de escala.");

                var marcacaoEscalaAtualizadaDto = _mapper.Map<MarcacaoEscalaDTO>(marcacaoEscala);

                return Ok(marcacaoEscalaAtualizadaDto);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Marcação de escala não encontrada.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Conflito de escala.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar marcação de escala.");
                return StatusCode(500);
            }
        }

        // DELETE: api/MarcacaoEscala/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var marcacaoEscala = await _service.GetById(id);
                _service.Delete(marcacaoEscala);

                return Ok(marcacaoEscala);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Marcação de escala não encontrada.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir marcação escala.");
                return StatusCode(500);
            }
        }

        // GET: api/MarcacaoEscala/pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<MarcacaoEscalaDTO>>> Get([FromQuery] PagedParameters parameters)
        {
            try
            {
                PagedList<MarcacaoEscala> marcacoesEscala = await _service.GetAll(parameters);
                return ObterMarcacoesEscala(marcacoesEscala);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem marcações de escala.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar escalas.");
                return StatusCode(500);
            }
        }

        // GET: api/MarcacaoEscala/5/UpdatePartial
        [HttpPatch("{id}/UpdatePartial")]
        public async Task<ActionResult<EscalaDTO>> Patch(int id, JsonPatchDocument<InativadoDTOPatch> patchDTO)
        {
            if (patchDTO is null)
                return BadRequest();

            try
            {
                var marcacao = await _service.GetById(id);
                var marcacaoUpdateRequest = _mapper.Map<InativadoDTOPatch>(marcacao);
                patchDTO.ApplyTo(marcacaoUpdateRequest, ModelState);

                if(!(ModelState.IsValid || TryValidateModel(marcacaoUpdateRequest)))
                    return BadRequest(ModelState);
                
                _mapper.Map(marcacaoUpdateRequest, marcacao);
                await _service.Update(marcacao);

                return Ok(_mapper.Map<InativadoDTOPatch>(marcacao));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Marcação não encontrada.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Conflito de escala.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao inativar marcacao.");
                return StatusCode(500);
            }
        }

        private ActionResult<IEnumerable<MarcacaoEscalaDTO>> ObterMarcacoesEscala(PagedList<MarcacaoEscala> marcacoesEscala)
        {
            var metadata = new
            {
                marcacoesEscala.TotalCount,
                marcacoesEscala.PageSize,
                marcacoesEscala.CurrentPage,
                marcacoesEscala.TotalPages,
                marcacoesEscala.HasNext,
                marcacoesEscala.HasPrevious
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var marcacoesEscalaDTO = _mapper.Map<IEnumerable<MarcacaoEscalaDTO>>(marcacoesEscala);
            return Ok(marcacoesEscalaDTO);
        }

    }
}