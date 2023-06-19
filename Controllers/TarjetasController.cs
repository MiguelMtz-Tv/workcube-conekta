using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workcube_pagos.Services;
using workcube_pagos.ViewModel.Req.Tarjeta;
using Workcube.Libraries;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class TarjetasController : ControllerBase
    {
        private readonly TarjetasService    _tarjetasService;
        private readonly string             NfMessage = "No se puedo encontrar al usuario en sesión";

        public TarjetasController(TarjetasService tarjetasService)
        {
            _tarjetasService = tarjetasService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> AddCard([FromBody] AddCardReq cardObj)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _tarjetasService.AddCard(cardObj);                  

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch(AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch(Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return Ok(objReturn.build());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("list")]
        public async Task<ActionResult> List([FromBody] int idClient)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _tarjetasService.List(idClient);

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch(AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch(Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return Ok(objReturn.build());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteCardReq cardObj)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _tarjetasService.Delete(cardObj);

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch(AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch(Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return Ok(objReturn.build());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("update")]
        public async Task<ActionResult> UpdateCard([FromBody] UpdateCardReq cardObj)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _tarjetasService.UpdateCard(cardObj);

                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch(AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch(Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return Ok(objReturn.build());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("get")]
        public async Task<ActionResult> GetCard([FromBody] UpdateCardReq cardObj)
        {
            JsonReturn objReturn = new JsonReturn();
            try
            {
                var result = await _tarjetasService.GetCard(cardObj);
                
                objReturn.Result = result;
                objReturn.Success(SuccessMessage.REQUEST);
            }
            catch(AppException ex)
            {
                objReturn.Exception(ex);
            }
            catch (Exception ex)
            {
                objReturn.Exception(ExceptionMessage.RawException(ex));
            }

            return Ok(objReturn.build());
        }


    }
}
