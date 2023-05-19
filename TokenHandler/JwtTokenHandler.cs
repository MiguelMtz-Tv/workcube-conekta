using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workcube.JwtAutentication;
using workcube_pagos.ViewModel.Res;

namespace workcube_pagos.TokenHandler
{
    public class JwtTokenHandler
    {
        private const int JWT_TOKEN_VALIDITY_MINS = 1;

        public JwtTokenHandler() { }

        public LoginRes GenerateToken(AspNetUser objAspNetUser)
        {
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JwtAuthExtension.JWT_SECURITY_TOKEN);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,   objAspNetUser.UserName),
                new Claim("Id",                                 objAspNetUser.Id),
                new Claim("Nombre",                             objAspNetUser.NombreCompleto.ToUpper()),
                new Claim(JwtRegisteredClaimNames.Jti,          Guid.NewGuid().ToString())
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
               //Expires = tokenExpiryTimeStamp
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new LoginRes
            {
                Token = token,
                Id = objAspNetUser.Id,
                NombreCompleto = objAspNetUser.NombreCompleto,
                IdCliente = objAspNetUser.IdCliente,
                //Expiration = tokenExpiryTimeStamp,
            };
        }
    }
}