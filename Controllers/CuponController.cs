using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workcube.Libraries;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Cupon;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class CuponController : ControllerBase
    {
        private readonly CuponesService _cuponesService;

        public CuponController(CuponesService cuponesService)
        {
            _cuponesService = cuponesService;
        }

        [Authorize (AuthenticationSchemes = "Bearer")]
        [HttpPost("get")]
        public async Task<ActionResult> GetCupon([FromBody]GetCuponReq model)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _cuponesService.GetCupon(model);

                if (result == null)
                {
                    return NotFound("No se encontró un cupón valido");
                }

                if (result.Status == CuponEstatus.Vencido)
                {
                    return BadRequest("El cupon " + result.Codigo + " ya no esta disponible");
                }

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch (AppException exception)
            {
                objReturn.Exception(exception);
            }
            catch (Exception exception)
            {
                objReturn.Exception(ExceptionMessage.RawException(exception));
            }

            //return Ok(result);

            return Ok(objReturn.build());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("list")]
        public async Task<ActionResult> List([FromBody]GetCuponReq model)
        {
            var result = await _cuponesService.List(model);

            if (result == null) { return NotFound("No hay cupones disponibles"); }
            return Ok(result);
        }
    }
}
