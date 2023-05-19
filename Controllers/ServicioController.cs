using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Workcube.Libraries;
using workcube_pagos.Models;
using workcube_pagos.Services;

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

        [HttpGet("clientservices/{id}")]
        public async Task<ActionResult> GetClientServices(int Id)
        {
            var services = await _serviciosService.GetClientServices(Id);
            return Ok(services);

            //JsonReturn objReturn = new JsonReturn();
            //try
            //{
            //    var services = await _serviciosService.GetClientServices(Id);

            //    objReturn.Result = services;
            //    objReturn.Title     = "Consulta satisfactoria";
            //    objReturn.Message   = "Se encontraron datos";
            //}
            //catch (AppException exception)
            //{
            //    objReturn.Exception(exception);
            //}
            //catch (Exception exception)
            //{
            //    objReturn.Exception(ExceptionMessage.RawException(exception));
            //}

            //return objReturn.build();
        }
    }
}
