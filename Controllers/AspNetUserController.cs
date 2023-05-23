using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class AspNetUserController : ControllerBase
    {
        AspNetUsersService _usersService;
        public AspNetUserController(AspNetUsersService aspNetUsersService) {
            _usersService = aspNetUsersService;
        }

        [HttpPost] //creacion de usuario
        public async Task<ActionResult> AddUser([FromBody] SingUpReq user)
        {
            await _usersService.AddUser(user);
            return Ok("usuario añadido");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("client/{id}")] //obtener un usuario (solo nombre y apellidos)
        public async Task<ActionResult> GetUserNames(string id)
        {
            var user = await _usersService.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("update/{id}")] //actaulizar un usuario
        public async Task<ActionResult> UpdateUser(string id, [FromBody] UpdateUserReq user)
        {
            var updatedUser = await _usersService.UpdateUser(id, user);
            if(updatedUser == null)
            {
                return BadRequest("El usuario no existe o se ha eliminado");
            }
            return Ok(updatedUser);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("updatepass/{id}")] //actualizar contraseña
        public async Task<ActionResult> UpdatePassword(string id, [FromBody] UpdatePasswordReq passwordReq)
        {   
            var updatePassword = await _usersService.UpdatePassword(id, passwordReq);
            if(updatePassword == null)
            {
                return BadRequest("la contraseña es incorrecta");
            }
            return Ok();
        }
    }
}
