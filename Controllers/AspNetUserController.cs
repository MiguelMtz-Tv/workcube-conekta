using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using Workcube.Libraries;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Usuario;
using workcube_pagos.ViewModel.Statics;

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
        public async Task<ActionResult> AddUser([FromBody] SingUpReq model)
        {
            var result = await _usersService.AddUser(model);

             if(result == "Este número de contrato ya está en uso")
            {
                return BadRequest(result);
            }
             if(result == "Este número de contrato no existe")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("user")] //obtener un usuario (solo nombre y apellidos)
        public async Task<ActionResult> GetUserNames([FromBody] JsonObject id)
        {
            var user = await _usersService.GetUser(id["Id"].ToString());
            if (user == null) { return NotFound("No se encontraron los datos del usuario " + id); }
            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("update")] //actualizar un usuario
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserReq user)
        {
            JsonReturn objReturn = new JsonReturn();

            try
            {
                var updatedUser = await _usersService.UpdateUser(user);
                if (updatedUser == null)
                {
                    return BadRequest("El usuario no existe o se ha eliminado");
                    // throw new ArgumentException("El usuario no existe o se ha eliminado");
                }

                objReturn.Result = updatedUser;

                objReturn.Title     = "Actualización";
                objReturn.Message   = "El usuario se ha actualizado correctamente";

            }
            catch(AppException app)
            {
                objReturn.Exception(app);
            }
            catch (Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return objReturn.build();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("updatepass")] //actualizar contraseña
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordReq passwordReq)
        {   
            var updatePassword = await _usersService.UpdatePassword(passwordReq);
            if(updatePassword == null)
            {
                return BadRequest("la contraseña es incorrecta");
            }
            return Ok();
        }
    }
}
