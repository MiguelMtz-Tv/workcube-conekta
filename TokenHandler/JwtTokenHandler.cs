using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workcube.JwtAutentication;
using workcube_pagos.ViewModel.Res;
using System.Text.Json;
using System.Security.Cryptography;

namespace workcube_pagos.TokenHandler
{
    public class JwtTokenHandler
    {
        private const int JWT_TOKEN_VALIDITY_MINS = 60; // Tiempo de validez del token en minutos
        private const string JWT_SECURITY_KEY = "jdiosajdoks";

        public JwtTokenHandler() { }

        public LoginRes GenerateToken(AspNetUser objAspNetUser)
        {
            var tokenIssuedAt = DateTime.UtcNow;
            var tokenExpiration = tokenIssuedAt.AddMinutes(JWT_TOKEN_VALIDITY_MINS);

            using var rsa = RSA.Create();

            // Generar una clave privada RSA válida
            var privateKey = new RSACryptoServiceProvider();
            privateKey.ImportParameters(rsa.ExportParameters(true));

            var signingCredentials = new SigningCredentials(new RsaSecurityKey(privateKey), SecurityAlgorithms.RsaSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, objAspNetUser.UserName),
                new Claim("Id", objAspNetUser.Id),
                new Claim("Nombre", objAspNetUser.NombreCompleto.ToUpper()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = tokenIssuedAt,
                NotBefore = tokenIssuedAt,
                Expires = tokenExpiration,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            var loginResJson = JsonSerializer.Serialize(new LoginRes
            {
                Token = token,
                Id = objAspNetUser.Id,
                NombreCompleto = objAspNetUser.NombreCompleto,
                IdCliente = objAspNetUser.IdCliente
            });

            var loginRes = JsonSerializer.Deserialize<LoginRes>(loginResJson);

            return loginRes;
        }
    }
}
