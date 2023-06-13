using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Servicio;

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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("cancel")]
        public async Task<ActionResult> CancelService([FromBody] CancelServiceReq model)
        {
            var serviceToCancel = await _serviciosService.CancelService(model);
            
            if (serviceToCancel == null) {return BadRequest("servicio no encontrado");}
            return Ok(serviceToCancel);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("List")]
        public async Task<ActionResult> List([FromBody] int Id)
        {
            var result = await _serviciosService.GetClientServices(Id);
            
            if (result == null) {return NotFound("al parecer el usuario no tiene servicios contratados");}

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> GetService([FromBody] GetServiceReq model)
        {
            var result = await _serviciosService.GetService(model);

            if (result == null) {return NotFound("servicio no encontrado");}

            return Ok(result);
        }


    }
}
