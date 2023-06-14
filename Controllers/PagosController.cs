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
        public async Task<ActionResult> CreateCharge([FromBody] CreateChargeReq chargeObj)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _pagosService.CreateCharge(chargeObj);

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            
            catch (AppException ex) {
                objReturn.Exception(ex);
            }
            catch (StripeException ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }
            catch (Exception ex){
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return Ok(objReturn.build());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("list")]
        public async Task<ActionResult> List([FromBody] int servicioId)
        {   
            var result = await _pagosService.List(servicioId);
            if(result == null) { return NotFound("No se han encontrado pagos de este servicio"); }

            return Ok(result);
        }

    }
}
