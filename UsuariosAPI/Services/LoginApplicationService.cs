using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginApplicationService
    {
        private SignInManager<IdentityUser<int>> _signManager;
        private TokenApplicationServices _tokenServices;
        public LoginApplicationService(SignInManager<IdentityUser<int>> signManager, TokenApplicationServices tokenServices)
        {
            _signManager = signManager;
            _tokenServices = tokenServices;
        }
        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user => user.NormalizedUserName == request.Username.ToUpper());

                Token token = _tokenServices.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            } 
            return Result.Fail("Login Falhou!");
        }


    }
}
