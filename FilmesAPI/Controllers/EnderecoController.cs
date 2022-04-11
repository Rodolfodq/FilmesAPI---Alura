using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoApplicationServices _services;
        public EnderecoController(EnderecoApplicationServices service)
        {
            _services = service;
        }
        [HttpPost]
        public async Task<IActionResult> AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            int id = await _services.AdicionaEndereco(enderecoDto);
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = id }, enderecoDto);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperaEnderecos()
        {
            List<ReadEnderecoDto> readDto = await _services.RecuperaEnderecos();
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaEnderecosPorId(int id)
        {
            ReadEnderecoDto readDto = await _services.RecuperaEnderecosPorId(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaEnderecoAsync(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            if (await _services.AtualizaEndereco(id, enderecoDto)) return NoContent();
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaEndereco(int id)
        {
            Result result = await _services.DeletaEndereco(id);
            if (result.IsSuccess) return NoContent();
            return NotFound();
        }
    }
}