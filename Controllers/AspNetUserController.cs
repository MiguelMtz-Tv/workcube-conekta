using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost("add")] //creacion de usuario
        public async Task<ActionResult<dynamic>> AddUser([FromBody] SingUpReq model)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _usersService.AddUser(model);
                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch (AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch(Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return objReturn.build();
        }

        [HttpPost("GetUserNames")] //obtener un usuario (solo nombre y apellidos)
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<dynamic>> GetUserNames([FromBody] JsonObject data)
        {
            JsonReturn objReturn = new JsonReturn();

            try
            {
                dynamic dynamicDFata = Globals.JsonData(data);

                var id = Globals.ParseGuid(dynamicDFata.id);

                var result = await _usersService.GetUser(id);
                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch (AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch (Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return objReturn.build();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("update")] //actualizar un usuario
        public async Task<ActionResult<dynamic>> UpdateUser([FromBody] UpdateUserReq user)
        {
            JsonReturn objReturn = new JsonReturn();

            try
            {
                var updatedUser = await _usersService.UpdateUser(user);

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
        public async Task<ActionResult<dynamic>> UpdatePassword([FromBody] UpdatePasswordReq passwordReq)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                await _usersService.UpdatePassword(passwordReq);
                objReturn.Title = "Contraseña actualizada";
            }
            catch (AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch (Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }
            return objReturn.build();
        }
    }
}
