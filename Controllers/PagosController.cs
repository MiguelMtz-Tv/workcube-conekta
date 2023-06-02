using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req;

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
            var result = await _pagosService.CreateCharge(chargeObj);

            return Ok(result);
        }
        
    }
}
