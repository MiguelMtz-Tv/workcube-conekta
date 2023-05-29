using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using workcube_pagos.ViewModel.Res;


namespace workcube_pagos.TokenHandler
{
    public class JwtTokenHandler
    {
        private const int JWT_TOKEN_VALIDITY_MINS = 10; // Tiempo de validez del token en minutos
        private const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";

        public JwtTokenHandler() { }

        public LoginRes GenerateToken(AspNetUser objAspNetUser)
        {
            var tokenIssuedAt =             DateTime.UtcNow;
            var tokenExpiration =           tokenIssuedAt.AddSeconds(JWT_TOKEN_VALIDITY_MINS);
            var securityKey =               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECURITY_KEY));
            var signingCredentials =        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,       objAspNetUser.UserName),
                new Claim("Id",                                     objAspNetUser.Id),
                new Claim("Nombre",                                 objAspNetUser.NombreCompleto.ToUpper()),
                new Claim(JwtRegisteredClaimNames.Jti,              Guid.NewGuid().ToString())
            };

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =                   new ClaimsIdentity(claims),
                IssuedAt =                  tokenIssuedAt,
                NotBefore =                 tokenIssuedAt,
                Expires =                   tokenExpiration,
                SigningCredentials =        signingCredentials,
            };

            var jwtSecurityTokenHandler =           new JwtSecurityTokenHandler();
            var securityToken =                     jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token =                             jwtSecurityTokenHandler.WriteToken(securityToken);

            return new LoginRes
            {
                Token =             token,
                Id =                objAspNetUser.Id,
                NombreCompleto =    objAspNetUser.NombreCompleto,
                IdCliente =         objAspNetUser.IdCliente
            };
        }
    }
}