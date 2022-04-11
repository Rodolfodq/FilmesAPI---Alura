using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenApplicationServices
    {
        public Token CreateToken(IdentityUser<int> usuario)
        {
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            SymmetricSecurityKey chave = new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes("0asdjas09djsa09djasadsa09asfd09sabsdf"));
            SigningCredentials credentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                                        claims: direitosUsuario,
                                        signingCredentials: credentials,
                                        expires: DateTime.UtcNow.AddHours(1));
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
