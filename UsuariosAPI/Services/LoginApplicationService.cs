using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginApplicationService
    {
        private SignInManager<CustomIdentityUser> _signManager;
        private TokenApplicationServices _tokenServices;
        public LoginApplicationService(SignInManager<CustomIdentityUser> signManager, TokenApplicationServices tokenServices)
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

                Token token = _tokenServices
                    .CreateToken(identityUser, 
                    _signManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());
                return Result.Ok().WithSuccess(token.Value);
            } 
            return Result.Fail("Login Falhou!");
        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);
            IdentityResult identityResult = _signManager
                .UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            if (identityResult.Succeeded) return Result.Ok().WithSuccess("Senha redefinida com sucesso!");
            return Result.Fail("Houve um erro na operação!");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);

            if(identityUser != null)
            {
                string codigoRecuperacao = _signManager
                    .UserManager
                    .GeneratePasswordResetTokenAsync(identityUser)
                    .Result;
                return Result.Ok().WithSuccess(codigoRecuperacao);
            }
            return Result.Fail("Falha ao solicitar redefinição");
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            return _signManager
                            .UserManager
                            .Users
                            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
