using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Cliente;
using Workcube.Libraries;

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
        public async Task<ActionResult<dynamic>> Create([FromBody] CreateClientReq ClientObj)
        {
            JsonReturn objReturn = new JsonReturn();
            
            try
            {
                var result = await _clientesService.CreateClient(ClientObj);
                objReturn.Result = result;
                objReturn.Title = "Se ha creado un nuevo cliente";
                objReturn.Message = "Usuario creado en la base de datos";
            }
            catch (AppException ex) 
            {
                objReturn.Exception(ex);
            }
            catch(Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }
            
            return objReturn.build();
        }
    }
}
