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
        public async Task<ActionResult> GetCupon([FromBody]GetCuponReq model)
        {
            var result = await _cuponesService.GetCupon(model);

            if (result == null)
            {
                return NotFound("No se encontró un cupón valido");
            }

            if(result.Status == CuponEstatus.Vencido)
            {
                return Ok("este cupón ya no es valido");
            }

            return Ok(result);
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
