using workcube_pagos.ViewModel.Req.Pago;
using workcube_pagos.ViewModel.Res.Pago;

namespace workcube_pagos.Services
{
    public class PagosService
    {
        private readonly DataContext _context;
        private readonly ClientesService _clientesService;
        public PagosService(DataContext context, ClientesService clientesService) {
            _context = context;
            _clientesService = clientesService;
        }   

        public async Task<List<Pago>> List(int idServicio)
        {
            var services = await _context.Pagos.Where(p => p.IdServicio == idServicio).ToListAsync();
            if(services ==  null) { return null; }
            return services;
        }

        public async Task<ChargeRes> CreateCharge(CreateChargeReq chargeObj)
        {
            
            var serviceToPay = await _context.Servicios.FindAsync(chargeObj.IdServicio);
            if (serviceToPay == null 
                || serviceToPay.IdServicioEstatus == 1 
                || serviceToPay.ServicioEstatusName == "Vigente") 
                { return null; }

            //obtener al cliente
            var client = await _context.Clientes.FindAsync(chargeObj.IdCliente);
            if (client == null) { return null; }
            var customer = client.StripeCustomerID;

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

            //Creamos el cargo en la api de stripe
            var result = new Charge();
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = amount,
                    Currency = "mxn",
                    Source = chargeObj.IdCard,
                    Customer = customer,
                };

                var service = new ChargeService();
                result = service.Create(options);
            }
            catch (StripeException)
            {
                throw new ArgumentException("Error en el pago: p-01");
            }

            if(result == null)
            {
                throw new ArgumentException("Error en el pago: p-02");
            }

            //guardar pago en la base de datos
            var loginTransaction = _context.Database.BeginTransaction();
            DateTime dateTime = DateTime.Now;
            var newPayment = new Pago
            {
                Fecha =         dateTime,
                IdServicio =    chargeObj.IdServicio,
                IdCliente =     chargeObj.IdCliente,
                Monto =         amount,
                IdStripeCard =  result.PaymentMethod,
                IdStripeCharge= result.Id,
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
            serviceToPay.Vigencia =                    dateTime.AddDays(30);

            //guardamos las consultas
            await _context.SaveChangesAsync();
            loginTransaction.Commit();

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

    }
}
