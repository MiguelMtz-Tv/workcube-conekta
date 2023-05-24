using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req;

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
        [HttpPost]
        public async Task<ActionResult> GetCupon(GetCuponReq model)
        {
            var result = await _cuponesService.GetCupon(model);
            if (result == null)
            {
                return NotFound("No se encontró un cupón valido");
            }

            if(result.Status == CuponStatus.Vencido)
            {
                return Ok("este cuón ya no es valido");
            }

            return Ok(result);
        }
    }
}
