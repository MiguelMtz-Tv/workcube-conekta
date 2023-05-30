using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesService _clientesService;
        public ClientesController(ClientesService clientesService) { 
            _clientesService = clientesService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateClientReq ClientObj)
        {
            if(ClientObj == null) { return BadRequest("El cliente es nulo"); }

            var result = await _clientesService.CreateClient(ClientObj);
            
            if (result == null) { return Forbid("algo salió mal"); }
            
            return Ok(result);
        }
    }
}
