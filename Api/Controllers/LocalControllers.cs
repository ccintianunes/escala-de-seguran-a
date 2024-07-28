using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EscalaSegurancaAPI.DTOs;
using EscalaSegurancaAPI.Models;
using EscalaSegurancaAPI.Filters;
using Newtonsoft.Json;
using EscalaSegurancaAPI.Services;
using System.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace EscalaSeguranca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LocalController> _logger;
        private readonly ILocalService _service;

        public LocalController(ILocalService service,
            IMapper mapper, ILogger<LocalController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Local
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalDTO>>> Get()
        {
            try
            {
                var locais = await _service.GetAll();
                var locaisDTO = _mapper.Map<IEnumerable<LocalDTO>>(locais);

                return Ok(locaisDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem locais.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar locais.");
                return StatusCode(500);
            }
        }

        // GET: api/Local/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalDTO>> Get(int id)
        {
            try
            {
                var local = await _service.GetById(id);
                var localDTO = _mapper.Map<LocalDTO>(local);

                return Ok(localDTO);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Local não encontrado.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar local.");
                return StatusCode(500);
            }

        }

        // POST: api/Local
        [HttpPost]
        public async Task<ActionResult<LocalDTO>> Post(LocalDTO localDTO)
        {
            if (localDTO == null)
                return BadRequest("Dados inválidos.");

            try
            {
                var local = _mapper.Map<Local>(localDTO);
                var sucesso = await _service.Create(local);

                if (!sucesso)
                    return StatusCode(500, "Erro ao criar local.");

                return CreatedAtAction(nameof(Get),
                    new { id = local.LocalId }, localDTO);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Local já cadastrado.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir local.");
                return StatusCode(500);
            }
        }

        // PUT: api/Local/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LocalDTO localDTO)
        {
            if (id != localDTO.LocalId)
                return BadRequest("Dados inválidos.");

            try
            {
                var localExistente = await _service.GetById(id);
                var local = _mapper.Map(localDTO, localExistente);

                var sucesso = await _service.Update(local);

                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar o local.");

                var localAtualizadoDto = _mapper.Map<LocalDTO>(local);

                return Ok(localAtualizadoDto);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Local não encontrado.");
            }
            catch (DuplicateNameException)
            {
                return BadRequest("Local já cadastrado");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Local vinculado a uma marcação ativa.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar local.");
                return StatusCode(500);
            }
        }

        // DELETE: api/Local/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var local = await _service.GetById(id);
                await _service.Delete(local);

                return Ok(local);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Local não encontrado.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Local vinculado a uma marcação ativa.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir local.");
                return StatusCode(500);
            }
        }

        // GET: api/Local/pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<LocalDTO>>> Get([FromQuery] PagedParameters parameters)
        {
            try
            {
                PagedList<Local> locais = await _service.GetAll(parameters);
                return ObterLocais(locais);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Não existem locais.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar locais.");
                return StatusCode(500);
            }
        }

        // GET: api/Local/5/UpdatePartial
        [HttpPatch("{id}/UpdatePartial")]
        public async Task<ActionResult<LocalDTO>> Patch(int id, JsonPatchDocument<InativadoDTOPatch> patchDTO)
        {
            if (patchDTO is null)
                return BadRequest();

            try
            {
                var local = await _service.GetById(id);
                var localUpdateRequest = _mapper.Map<InativadoDTOPatch>(local);
                patchDTO.ApplyTo(localUpdateRequest, ModelState);

                if(!(ModelState.IsValid || TryValidateModel(localUpdateRequest)))
                    return BadRequest(ModelState);
                
                _mapper.Map(localUpdateRequest, local);
                await _service.Update(local);

                return Ok(_mapper.Map<InativadoDTOPatch>(local));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Local não encontrada.");
            }
            catch (DuplicateNameException)
            {
                return BadRequest("Local já cadastrado");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Local vinculado a uma marcação ativa.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao inativar local.");
                return StatusCode(500);
            }
        }

        private ActionResult<IEnumerable<LocalDTO>> ObterLocais(PagedList<Local> locais)
        {
            var metadata = new
            {
                locais.TotalCount,
                locais.PageSize,
                locais.CurrentPage,
                locais.TotalPages,
                locais.HasNext,
                locais.HasPrevious
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var locaisDTO = _mapper.Map<IEnumerable<LocalDTO>>(locais);
            return Ok(locaisDTO);
        }
    }
}