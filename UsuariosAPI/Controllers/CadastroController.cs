using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroApplicationServices _services;
        public CadastroController(CadastroApplicationServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuarioAsync(CreateUsuarioDto usuarioDto)
        {
            Result result = await _services.CadastroUsuarioAsync(usuarioDto);
            if (result.IsFailed) return BadRequest(result.Errors[0].Message);            
            return Ok(result.Successes);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery]AtivaContaRequest request)
        {
            Result result = _services.AtivaContaUsuario(request);
            if(result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }
    }
}
