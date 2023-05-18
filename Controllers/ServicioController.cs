using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using workcube_pagos.Models;
using workcube_pagos.Services;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        
        readonly ServiciosService _serviciosService;

        public ServicioController( ServiciosService serviciosService)
        {
            _serviciosService = serviciosService;
        }

        [HttpGet("clientservices/{id}")]
        public async Task<ActionResult> GetClientServices(int Id)
        {
            var services = await _serviciosService.GetClientServices(Id);

            if (services == null)
            {
                return null;
            }
            return Ok(services);
        }
    }
}
