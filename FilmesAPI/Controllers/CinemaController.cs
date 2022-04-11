using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Model;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private CinemaApplicationServices _services;

        public CinemaController(CinemaApplicationServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaCinema([FromBody] Cinema cinema)
        {
            int id = await _services.AdicionaCinema(cinema);
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = id }, cinema);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            IEnumerable<ReadCinemaDto> dto = await _services.RecuperaCinemas(nomeDoFilme);
            if(dto == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaCinemasPorId(int id)
        {
            ReadCinemaDto cinema = await _services.RecuperaCinemasPorId(id);
            if (cinema != null)
            {
                return Ok(cinema);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result result = await _services.AtualizaCinema(id, cinemaDto);
            if (result.IsFailed)
            {
                return NoContent();
            }

            return NotFound();            
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaCinema(int id)
        {
            Result result = await _services.DeletaCinema(id);
            if (result.IsFailed)
            {
                return NoContent();
                
            }
            return NotFound();

        }
    }
}
