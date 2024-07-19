using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscalaSeguranca.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscalaSegurancaAPI.DTOs;
using EscalaSegurancaAPI.Models;

namespace EscalaSeguranca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicialController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ILogger<PolicialController> _logger;

        public PolicialController(IUnitOfWork uof, 
            IMapper mapper, ILogger<PolicialController> logger)
        {
            _uof = uof;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Policial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicialDTO>>> Get()
        {
            try
            {
                var policiais = await _uof.PolicialRepository.GetAll();
            if (policiais is null)
                return NotFound("Não existem policials.");

            var policiaisDTO = _mapper.Map<IEnumerable<PolicialDTO>>(policiais);
            return Ok(policiaisDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar policiais.");
                return StatusCode(500);
            }
        }

        // GET: api/Policial/5
        [HttpGet("{id}")]
        public ActionResult<PolicialDTO> Get(int id)
        {
            try
            {
                var policial = _uof.PolicialRepository.GetById(id);

            if (policial == null)
            {
                return NotFound("Policial não encontrada...");
            }

            var policialDTO = _mapper.Map<PolicialDTO>(policial);
            return Ok(policialDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar policial.");
                return StatusCode(500);
            }
        }

        // POST: api/Policial
        [HttpPost]
        public ActionResult<PolicialDTO> Post(PolicialDTO policialDTO)
        {
            if (policialDTO is null)
                return BadRequest("Dados inválidos.");

            try
            {
            var policial = _mapper.Map<Policial>(policialDTO);
            var sucesso = _uof.PolicialRepository.Add(policial);
            _uof.Complete();

            if(!sucesso)
                return StatusCode(500, "Erro ao criar policial.");

            return CreatedAtAction(nameof(Get), 
                new { id = policial.PolicialId }, policialDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao incluir policial.");
                return StatusCode(500);
            }
        }

        // PUT: api/Policial/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PolicialDTO policialDTO)
        {
            if (id != policialDTO.PolicialId)
                return BadRequest("Dados inválidos.");

        try
        {
            var policialExistente = _uof.PolicialRepository.GetById(id);
            if (policialExistente is null)
                return NotFound("policial não encontrado...");

            var policial = _mapper.Map(policialDTO, policialExistente);

            var sucesso = _uof.PolicialRepository.Update(policial);
            _uof.Complete();

            if (!sucesso)
                return StatusCode(500, "Erro ao atualizar o policial.");

            var policialAtualizadoDto = _mapper.Map < PolicialDTO > (policial);

            return Ok(policialAtualizadoDto);
        }
        catch (Exception e)
            {
                _logger.LogError(e, "Erro ao editar policial.");
                return StatusCode(500);
            }
        }

        // DELETE: api/Policial/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
            var policial = _uof.PolicialRepository.GetById(id);
            if (policial == null)
            {
                return NotFound("Policial não encontrado...");
            }

            _uof.PolicialRepository.Remove(policial);
            _uof.Complete();

            return Ok(policial);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao excluir policial.");
                return StatusCode(500);
            }
        }
    }
}
