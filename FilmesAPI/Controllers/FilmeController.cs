using FilmesAPI.Data.Dtos;
using FilmesAPI.Model;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmesApplicationServices _service;
        public FilmeController(FilmesApplicationServices service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdicionaFilme([FromBody] CreateFilmeDto filme)
        {
            int id = await _service.AddFilme(filme);
            return CreatedAtAction(nameof(RecuperaFilmeById), new {id = id}, filme);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular", Policy = "IdadeMinima")]
        public async Task<IActionResult> RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> dto = await _service.RecuperaFilmes(classificacaoEtaria);
            if (dto.Count > 0)
                return Ok(dto);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaFilmeById(int id)
        {
            ReadFilmeDto filme = await _service.RecuperaFilmesById(id);
            if (filme != null)
                return Ok(filme);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeNovo)
        {
            Result result = await _service.AtualizaFilme(id, filmeNovo);
            if (result.IsSuccess)
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmeById(int id)
        {
            Result result = await _service.DeletaFilmeById(id);
            if(result.IsSuccess)
                return NoContent();
            return NotFound();
        }
    }
}
