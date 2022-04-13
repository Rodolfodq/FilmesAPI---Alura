using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginApplicationService _service;
        public LoginController(LoginApplicationService service)
        {
            _service = service;
    }

        [HttpPost]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result resultado = _service.LogaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            Result result = _service.SolicitaResetSenhaUsuario(request);
            if(result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

        [HttpPost("/efetua-reset")]
        public IActionResult ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            Result result = _service.ResetaSenhaUsuario(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

    }
}
