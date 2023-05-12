using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class AspNetUserController : ControllerBase
    {
        AspNetUsersService _usersService;
        public AspNetUserController(AspNetUsersService aspNetUsersService) { 
            this._usersService = aspNetUsersService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] SingUpReq user)
        {
            await _usersService.AddUser(user);
            return Ok("usuario añadido");
        }
    }
}
