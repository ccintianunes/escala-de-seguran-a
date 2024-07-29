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
    public class PolicialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PolicialController> _logger;
        private readonly IPolicialService _service;
        public PolicialController(
            IMapper mapper, ILogger<PolicialController> logger,
            IPolicialService service)
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }

        // GET: api/Policial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicialDTO>>> GetAll()
        {
            try
            {
                var policiais = await _service.GetAll();
                var policiaisDTO = _mapper.Map<IEnumerable<PolicialDTO>>(policiais);

                return Ok(policiaisDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem policials.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar policiais.");
                return StatusCode(500);
            }
        }

        // GET: api/Policial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PolicialDTO>> Get(int id)
        {
            try
            {
                var policial = await _service.GetById(id);
                var policialDTO = _mapper.Map<PolicialDTO>(policial);

                return Ok(policialDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Policial não encontrado.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar policial.");
                return StatusCode(500);
            }
        }

        // POST: api/Policial
        [HttpPost]
        public async Task<ActionResult<PolicialDTO>> Post(PolicialDTO policialDTO)
        {
            if (policialDTO is null)
                return BadRequest("Dados inválidos.");

            try
            {
                var policial = _mapper.Map<Policial>(policialDTO);
                var sucesso = await _service.Create(policial);

                if (!sucesso)
                    return StatusCode(500, "Erro ao criar policial.");

                return CreatedAtAction(nameof(Get),
                    new { id = policial.PolicialId }, policialDTO);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("CPF já cadastrado");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir policial.");
                return StatusCode(500);
            }
        }

        // PUT: api/Policial/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PolicialDTO policialDTO)
        {
            if (id != policialDTO.PolicialId)
                return BadRequest("Dados inválidos.");

            try
            {
                var policialExistente = await _service.GetById(id);
                var policial = _mapper.Map(policialDTO, policialExistente);
                var sucesso = await _service.Update(policial);
                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar o policial.");

                var policialAtualizadoDto = _mapper.Map<PolicialDTO>(policial);

                return Ok(policialAtualizadoDto);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Policial não encontrado.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("CPF já cadastrado");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar policial.");
                return StatusCode(500);
            }
        }

        // DELETE: api/Policial/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var policial = await _service.GetById(id);
                await _service.Delete(policial);

                return Ok(policial);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Policial não encontrado.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Policial vinculado a uma marcação ativa.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir policial.");
                return StatusCode(500);
            }
        }

        // GET: api/Policial/pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<PolicialDTO>>> Get([FromQuery] PagedParameters parameters)
        {
            try
            {
                PagedList<Policial> policiais = await _service.GetAll(parameters);
                return ObterPoliciais(policiais);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existen policiais.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar policiais.");
                return StatusCode(500);
            }
        }

        // GET: api/Policial/5/UpdatePartial
        [HttpPatch("{id}/UpdatePartial")]
        public async Task<ActionResult<PolicialDTO>> Patch(int id, JsonPatchDocument<InativadoDTOPatch> patchPolicialDTO)
        {
            if (patchPolicialDTO is null)
                return BadRequest();

            try
            {
                var policial = await _service.GetById(id);
                var policialUpdateRequest = _mapper.Map<InativadoDTOPatch>(policial);
                patchPolicialDTO.ApplyTo(policialUpdateRequest, ModelState);

                if(!(ModelState.IsValid || TryValidateModel(policialUpdateRequest)))
                    return BadRequest(ModelState);
                
                _mapper.Map(policialUpdateRequest, policial);
                await _service.Update(policial);

                return Ok(_mapper.Map<InativadoDTOPatch>(policial));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Policial não encontrado.");
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao inativar policial.");
                return StatusCode(500);
            }
        }

        // GET: api/Policial/filter
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PolicialDTO>>> GetPoliciaisFiltro([FromQuery] PoliciaisFiltro filtro)
        {
            try
            {
                PagedList<Policial> policiais = await _service.GetPoliciaisFiltro(filtro);
                return ObterPoliciais(policiais);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existen policiais.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar policiais.");
                return StatusCode(500);
            }
        }

        private ActionResult<IEnumerable<PolicialDTO>> ObterPoliciais(PagedList<Policial> policiais)
        {
            var metadata = new
            {
                policiais.TotalCount,
                policiais.PageSize,
                policiais.CurrentPage,
                policiais.TotalPages,
                policiais.HasNext,
                policiais.HasPrevious
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var policiaisDTO = _mapper.Map<IEnumerable<PolicialDTO>>(policiais);
            return Ok(policiaisDTO);
        }
    }
}