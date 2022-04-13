using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LogoutApplicationServices
    {
        private SignInManager<CustomIdentityUser> _signinManager;
        public LogoutApplicationServices(SignInManager<CustomIdentityUser> signinManager)
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
