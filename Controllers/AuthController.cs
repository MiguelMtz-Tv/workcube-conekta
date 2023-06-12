using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.TokenHandler;
using workcube_pagos.ViewModel.Req.Usuario;
using workcube_pagos.ViewModel.Res.Usuario;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly AspNetUsersService _aspNetUsersService;
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AuthController(SignInManager<AspNetUser> signInManager, AspNetUsersService aspNetUsersService, JwtTokenHandler jwtTokenHandler)
        {
            _signInManager = signInManager;
            _aspNetUsersService = aspNetUsersService;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginRes>> Login([FromBody] LoginReq model)
        {
            var user = await _aspNetUsersService.FindLogin(model.UserName);

            if (user == null)
            {
                return Unauthorized("Usuario incorrecto o no encontrado");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return _jwtTokenHandler.GenerateToken(user);
            }
                return Unauthorized("Contraseña incorrecta");
        }
    }
}