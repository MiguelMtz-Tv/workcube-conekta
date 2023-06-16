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
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _cuponesService.List(model);

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

            return Ok(objReturn.build());
        }
    }
}
