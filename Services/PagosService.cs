using System.Drawing.Text;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Services
{
    public class PagosService
    {
        private readonly DataContext _context;
        public PagosService(DataContext context) {
            _context = context;
        }   

        public async Task<object> CreateCharge(CreateChargeReq chargeObj)
        {
            var serviceToPay = await _context.Servicios.FindAsync(chargeObj.IdServicio);
            if(serviceToPay == null) { return null; }

            var client = await _context.Clientes.FindAsync(chargeObj.IdCliente);
            if(client == null) { return null; } 

            var amount = Convert.ToInt64(serviceToPay.ServicioTipoCosto);
            var customer = client.StripeCustomerID;

            var options = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "mxn",
                Source = chargeObj.IdCard,
                Customer = customer,
            };

            
            var service = new ChargeService();
            var result = service.Create(options);

            return result;
        }

    }
}
