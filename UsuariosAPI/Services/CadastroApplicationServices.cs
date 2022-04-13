using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroApplicationServices
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EmailApplicationService _emailService;

        public CadastroApplicationServices(IMapper mapper, UserManager<CustomIdentityUser> userManager, EmailApplicationService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Result> CadastroUsuarioAsync(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            var resultadoIdentity = await _userManager
                .CreateAsync(usuarioIdentity, usuarioDto.Password);
            await _userManager.AddToRoleAsync(usuarioIdentity, "regular");
            if (resultadoIdentity.Succeeded)
            {
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);
                var encondeCode = HttpUtility.UrlEncode(code);

                _emailService
                    .EnviarEmail(new[] { usuarioIdentity.Email }, 
                    "Link de ativação", 
                    usuarioIdentity.Id,
                    encondeCode);
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário!");
        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuario!");
        }
    }
}
