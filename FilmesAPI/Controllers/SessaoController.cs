using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Model;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly SessaoApplicationServices _services;
        public SessaoController(SessaoApplicationServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaSessao(CreateSessaoDto dto)
        {
            Sessao sessao = await _services.AdicionarSessao(dto);
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = sessao.Id }, sessao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaSessoesPorId(int id)
        {
            ReadSessaoDto dto = await _services.RecuperaSessoesPorId(id);
            if(dto == null)
                return NotFound();
            return Ok(dto);
        }
    }
}
