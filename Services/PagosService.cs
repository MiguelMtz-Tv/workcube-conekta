using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Drawing.Text;
using workcube_pagos.ViewModel.Req;
using workcube_pagos.ViewModel.Res;

namespace workcube_pagos.Services
{
    public class PagosService
    {
        private readonly DataContext _context;
        public PagosService(DataContext context) {
            _context = context;
        }   

        public async Task<ChargeRes> CreateCharge(CreateChargeReq chargeObj)
        {
            try
            {
                var serviceToPay = await _context.Servicios.FindAsync(chargeObj.IdServicio);
                if (serviceToPay == null 
                    || serviceToPay.IdServicioEstatus == 1 
                    || serviceToPay.ServicioEstatusName == "Vigente") 
                    { return null; }

                //obtener al cliente
                var client = await _context.Clientes.FindAsync(chargeObj.IdCliente);
                if (client == null) { return null; }

                var amount = Convert.ToInt64(serviceToPay.ServicioTipoCosto);

                //obtener el cupon y realizar el descuento
                decimal descuento = 0;
                var cupon = new Cupon();

                if (chargeObj.IdCupon > 0)
                {
                    cupon = await _context.Cupones.FindAsync(chargeObj.IdCupon);
                    descuento = cupon.Monto;
                    amount -= Convert.ToInt64(descuento);
                }

                var customer = client.StripeCustomerID;

                //Creamos el cargo en la api de stripe
                var options = new ChargeCreateOptions
                {
                    Amount = amount,
                    Currency = "mxn",
                    Source = chargeObj.IdCard,
                    Customer = customer,
                };


                var service = new ChargeService();
                var result = service.Create(options);

                //guardar pago en la base de datos
                DateTime dateTime = DateTime.Now;
                var newPayment = new Pago
                {
                    Fecha =         dateTime,
                    IdServicio =    chargeObj.IdServicio,
                    IdCliente =     chargeObj.IdCliente,
                    Monto =         amount,
                    IdStripeCard =  result.PaymentMethod,
                    Descuento =     descuento,
                };

                await _context.Pagos.AddAsync(newPayment);

                //cambiamos el estado del cupón a vencido
                if (cupon.IdCupon > 0)
                {
                    cupon.Status = CuponEstatus.Vencido;
                }

                //Cambiamos el estatus del servicio
                serviceToPay.ServicioEstatusName =      "Vigente";
                serviceToPay.IdServicioEstatus =        1;
                serviceToPay.Vigencia =                 dateTime.AddDays(30);

                //guardamos las consultas
                await _context.SaveChangesAsync();

                return new ChargeRes
                {
                    Fecha = newPayment.Fecha,
                    IdServicio = newPayment.IdServicio,
                    IdCliente = newPayment.IdCliente,
                    IdStripeCard = newPayment.IdStripeCard,
                    Monto = newPayment.Monto,
                    Descuento = newPayment.Descuento,
                };
            }
            catch (StripeException) 
            {
                return null;
            }
        }

    }
}
