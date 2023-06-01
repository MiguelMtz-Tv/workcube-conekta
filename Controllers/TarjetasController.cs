using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Text.Json.Nodes;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class TarjetasController : ControllerBase
    {
        private readonly TarjetasService _tarjetasService;
        public TarjetasController(TarjetasService tarjetasService)
        {
            _tarjetasService = tarjetasService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> AddCard([FromBody] AddCardReq cardObj)
        {
            var result = await _tarjetasService.AddCard(cardObj);

            if (result == null) { return BadRequest("algo salió mal durante la creación de la tarjeta"); }

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("list")]
        public async Task<ActionResult> List([FromBody] int idClient)
        {
            var result = await _tarjetasService.List(idClient);

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteCardReq cardObj)
        {
            var result = await _tarjetasService.Delete(cardObj);
            if (result == null) { return BadRequest(); }

            return Ok(result);
        }

    }
}
