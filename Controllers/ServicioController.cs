﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Servicio;
using Workcube.Libraries;

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
        [HttpPost]
        public async Task<ActionResult<dynamic>> GetService([FromBody] GetServiceReq model)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _serviciosService.GetService(model);
                objReturn.Result = result;
                objReturn.Title = "Servicio";
                objReturn.Message = "Servicios encontrado";
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("List")]
        public async Task<ActionResult<dynamic>> List([FromBody] int Id)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _serviciosService.GetClientServices(Id);
                objReturn.Result = result;
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
        [HttpPut("cancel")]
        public async Task<ActionResult<dynamic>> CancelService([FromBody] CancelServiceReq model)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                await _serviciosService.CancelService(model);;

                objReturn.Title = "Cancelado";
                objReturn.Message = "Servicio cancelado";
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
