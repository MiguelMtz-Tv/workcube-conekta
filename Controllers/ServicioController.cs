using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("cancel")]
        public async Task<ActionResult> CancelService([FromBody] CancelServiceReq model)
        {
            var serviceToCancel = await _serviciosService.CancelService(model);
            
            if (serviceToCancel == null) {
                return BadRequest("servicio no encontrado");
            }
            return Ok(serviceToCancel);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("List/{id}")]
        public async Task<ActionResult> List(int id)
        {
            var result = await _serviciosService.GetClientServices(id);
            
            if (result == null) { 
                return NotFound("al parecer el usuario no tiene servicios contratados");
            }

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetService(int id)
        {
            var result = await _serviciosService.GetService(id);

            if (result == null)
            {
                return NotFound("servicio no encontrado");
            }

            return Ok(result);
        }


    }
}
