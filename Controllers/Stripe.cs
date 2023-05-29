using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace workcube_pagos.Controllers
{
    public class Stripe : ControllerBase
    {
        [Route("api/[controller]")]

        [HttpGet]
        public async Task<ActionResult> Index() {
            StripeConfiguration.ApiKey = "sk_test_51NDAKSHo0a7qOJb8fswzrmT31QLoRDB6Hp8HWXNvEEg8JXpE3xnxGMWDrGOFiW5lo5AZ8Cjshh4RT7MJIpBatsUt006xpsXGTR";
            //Create a customer with no params 
            var service = new CustomerService();
            //var customer = service.Create(null);
            //return Ok(customer);

            var customer = service.Get("cus_NzCXmfbsxfRGY8");
            return Ok(customer);
        }
    }
}
