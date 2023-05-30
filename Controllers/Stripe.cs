using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace workcube_pagos.Controllers
{
    public class Stripe : ControllerBase
    {
        [Route("api/[controller]")]

        [HttpGet]
        public async Task<ActionResult> Index() {
 
            //Create a customer with no params 
            var service = new CustomerService();
            //var customer = service.Create(null);
            //return Ok(customer);

            //Create a customer with params
            var options = new CustomerCreateOptions
            {
                //options right here
            };

            var customer = service.Get("cus_NzCXmfbsxfRGY8");
            return Ok(customer);
        }
    }
}
