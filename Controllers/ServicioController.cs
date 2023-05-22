using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Nodes;
using Workcube.Libraries;
using workcube_pagos.Models;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        readonly ServiciosService _serviciosService;

        public ServicioController( ServiciosService serviciosService )
        {
            _serviciosService = serviciosService; 
        }

        [Authorize]
        [HttpPut("cancel")]
        public async Task<ActionResult> CancelService([FromBody] CancelServiceReq model)
        {
            var serviceToCancel = await _serviciosService.CancelService(model);
            if (serviceToCancel == null) {
                return BadRequest("servicio no encontrado");
            }
            return Ok(serviceToCancel);
        }

        [HttpPost("List")]
        [Authorize]
        public async Task<ActionResult> List(JsonObject data)
        {
            JsonReturn objReturn = new JsonReturn();
            
            try
            {
                dynamic argData = Globals.JsonData(data);

                int idCliente = Globals.ParseInt(argData.idUser);

                var services = await _serviciosService.GetClientServices(idCliente);

                objReturn.Result = services;
                //objReturn.Result = new { list = services };

                objReturn.Title = "Consulta satisfactoria";
                objReturn.Message = "Se encontraron datos";
            }
            catch (AppException exception)
            {
                objReturn.Exception(exception);
            }
            catch (Exception exception)
            {
                objReturn.Exception(ExceptionMessage.RawException(exception));
            }

            return objReturn.build();
        }
    }
}
