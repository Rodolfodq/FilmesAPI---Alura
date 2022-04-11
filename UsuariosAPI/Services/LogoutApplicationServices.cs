using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosAPI.Services
{
    public class LogoutApplicationServices
    {
        private SignInManager<IdentityUser<int>> _signinManager;
        public LogoutApplicationServices(SignInManager<IdentityUser<int>> signinManager)
        {
            _signinManager = signinManager;
        }
        public Result DeslogaUsuario()
        {
            var resultadoIdentity = _signinManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout Falhou!");
        }
    }
}
