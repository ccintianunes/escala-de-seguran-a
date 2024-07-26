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
    public class LocalController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ILogger<LocalController> _logger;

        public LocalController(IUnitOfWork uof,
            IMapper mapper, ILogger<LocalController> logger)
        {
            _uof = uof;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Local
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalDTO>>> Get()
        {
            try
            {
            var locais = await _uof.LocalRepository.GetAll();
            if (locais == null)
                return NotFound("Não existem locais.");

            var locaisDTO = _mapper.Map<IEnumerable<LocalDTO>>(locais);
            return Ok(locaisDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar locais.");
                return StatusCode(500);
            }
        }

        // GET: api/Local/5
        [HttpGet("{id}")]
        public ActionResult<LocalDTO> Get(int id)
        {
            try
            {
            var local = _uof.LocalRepository.GetById(id);

            if (local == null)
            {
                return NotFound("Local não encontrado...");
            }

            var localDTO = _mapper.Map<LocalDTO>(local);
            return Ok(localDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar local.");
                return StatusCode(500);
            }

        }

        // POST: api/Local
        [HttpPost]
        public ActionResult<LocalDTO> Post(LocalDTO localDTO)
        {
            if (localDTO == null)
                return BadRequest("Dados inválidos.");

            try
            {
            var local = _mapper.Map<Local>(localDTO);
            var sucesso = _uof.LocalRepository.Add(local);
            _uof.Complete();

            if(!sucesso)
                return StatusCode(500, "Erro ao criar local.");

            return CreatedAtAction(nameof(Get),
                new { id = local.LocalId }, localDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir local.");
                return StatusCode(500);
            }
        }

        // PUT: api/Local/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, LocalDTO localDTO)
        {
            if (id != localDTO.LocalId)
                return BadRequest("Dados inválidos.");

            try
            {
                var localExistente = _uof.LocalRepository.GetById(id);
                if (localExistente == null)
                    return NotFound("Local não encontrado...");

                var local = _mapper.Map(localDTO, localExistente);

                var sucesso = _uof.LocalRepository.Update(local);
                _uof.Complete();

                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar o local.");

                var localAtualizadoDto = _mapper.Map<LocalDTO>(local);

                return Ok(localAtualizadoDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar local.");
                return StatusCode(500);
            }
        }

        // DELETE: api/Local/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
            var local = _uof.LocalRepository.GetById(id);
            if (local == null)
            {
                return NotFound("Local não encontrado...");
            }

            _uof.LocalRepository.Remove(local);
            _uof.Complete();

            return Ok(local);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir local.");
                return StatusCode(500);
            }
        }

        // GET: api/Local/pagination
        [HttpGet("pagination")]
        public ActionResult<IEnumerable<LocalDTO>> Get([FromQuery] PagedParameters parameters)
        {
            try
            {
                PagedList<Local> locais = _uof.LocalRepository.Get(parameters);
                if (locais == null)
                    return NotFound("Não existem locais.");

                return ObterLocais(locais);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar locais.");
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
