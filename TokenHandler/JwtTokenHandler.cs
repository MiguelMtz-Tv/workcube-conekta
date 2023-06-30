using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using workcube_pagos.ViewModel.Res.Usuario;

namespace workcube_pagos.TokenHandler
{
    public class JwtTokenHandler
    {
        private const int JWT_TOKEN_VALIDITY_MINS = 60; // Tiempo de validez del token en minutos
        private const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
        private readonly IWebHostEnvironment _env;

        public JwtTokenHandler(IWebHostEnvironment env) 
        {
            _env = env;
        }

        public string PK(){
            var pk = _env.IsProduction() 
                ? "pk_live_51NDAKSHo0a7qOJb8E18QKcih0kvvW4kgYtRDFbk2SoNYN8Zo4yrPsHMlEvlvg2ZMc2mPaMInR3T8RLUogOGwFGbU003Sqyrz6b" 
                : "pk_test_51NDAKSHo0a7qOJb87jobsvr8AyT9MVHgf3a4vzvhkDLZIHuOUDnpYATHh7tkR2vQftqJBFkwJsvrxQuDOYGs4qyE00zIK6GN26";
            return pk;
        }
        public LoginRes GenerateToken(AspNetUser objAspNetUser)
        {
            var tokenIssuedAt =             DateTime.UtcNow;
            var tokenExpiration =           tokenIssuedAt.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var securityKey =               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECURITY_KEY));
            var signingCredentials =        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var pk = this.PK();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,       objAspNetUser.UserName),
                new Claim("Id",                                     objAspNetUser.Id),
                new Claim("Nombre",                                 objAspNetUser.NombreCompleto.ToUpper()),
                new Claim("Email",                                  objAspNetUser.Email.ToLower()),
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
                Email =             objAspNetUser.Email,
                IdCliente =         objAspNetUser.IdCliente,
                StripePK =          pk,    
            };
        }
    }
}