using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Model;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private readonly GerenteApplicationServices _services;
        public GerenteController(GerenteApplicationServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaGerente(CreateGerenteDto dto)
        {
            ReadGerenteDto readGerenteDto = await _services.AdicionarGerente(dto);
            return CreatedAtAction(nameof(RecuperaGerentesPorId), new { Id = readGerenteDto.Id }, readGerenteDto);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaGerentesPorId(int id)
        {
            ReadGerenteDto dto = await _services.RecuperaGerentePorId(id);
            if(dto == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaGerente(int id)
        {
            Result result = await _services.DeletaGerente(id);
            if (result.IsFailed)
                return NoContent();
            return NotFound();
        }
    }
}
