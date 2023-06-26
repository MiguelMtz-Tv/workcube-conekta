using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workcube.Libraries;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Pago;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly PagosService _pagosService;
        public PagosController(PagosService pagosService) {
            _pagosService = pagosService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> CreateCharge([FromBody] CreateChargeReq chargeObj)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _pagosService.CreateCharge(chargeObj, User);

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            
            catch (AppException ex) {
                objReturn.Exception(ex);
            }
            catch (Exception ex){
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return objReturn.build();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("list")]
        public async Task<ActionResult<dynamic>> List([FromBody] int servicioId)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _pagosService.List(servicioId);
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

        [HttpGet("file")]
        public dynamic Recibo()
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                byte[] result = _pagosService.testpdf();

                Response.Headers["Content-Disposition"] = $"inline; filename=reciboTest.pdf";
                Console.WriteLine(result);
                return new FileContentResult(result, "Aplication/pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objReturn.build();
        }

        /*[HttpGet("file")]
        public dynamic Recibo()
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                byte[] result = _pagosService.Recibo();

                Response.Headers["Content-Disposition"] = $"inline; filename=reciboTest.pdf";
                Console.WriteLine(result);
                return new FileContentResult(result, "Aplication/pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objReturn.build();
        }*/
    }
}
