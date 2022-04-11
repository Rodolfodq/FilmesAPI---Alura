using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutApplicationServices _service;
        public LogoutController(LogoutApplicationServices service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult DeslogaUsuario()
        {
            Result result = _service.DeslogaUsuario();
            if (result.IsSuccess)
                return Ok();
            return Unauthorized(result.Errors);
        }
    }
}
