using Workcube.Libraries;
using Workcube.ViewModels;
using workcube_pagos.Templates.Emails;
using workcube_pagos.ViewModel.Req.Pago;
using workcube_pagos.ViewModel.Statics;
using workcube_pagos.Libraries;
using System.Security.Claims;

namespace workcube_pagos.Services
{
    public class PagosService
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _root;
        public PagosService(DataContext context, IWebHostEnvironment root) {
            _context = context;
            _root = root;
        }   

        public async Task<List<Pago>> List(int idServicio)
        {
            var payments = await _context.Pagos.Where(p => p.IdServicio == idServicio).ToListAsync();
            if(payments ==  null) { throw new ArgumentException("No se encontraron pagos"); }
            return payments;
        }

        public async Task<dynamic> CreateCharge(CreateChargeReq chargeObj, ClaimsPrincipal user)
        {
            var serviceToPay = await _context.Servicios.FindAsync(chargeObj.IdServicio);
            DateTime dateTime = DateTime.Now;

            if (serviceToPay == null || serviceToPay.Vigencia > dateTime)
            { throw new ArgumentException("Error en pago: P-01"); }

            //obtener al cliente
            var client = await _context.Clientes.FindAsync(chargeObj.IdCliente) ?? throw new ArgumentException("Error en pago: P-02 - Cliente nulo");
            var customer = client.StripeCustomerID;
            long amount = serviceToPay.ServicioTipoCosto;
            long total;

            //obtener el cupon (si existe) y realizar el descuento
            long descuento = 0;
            var cupon = new Cupon();

            if (chargeObj.AreCupon && chargeObj.IdCupon > 0)
            {
                cupon = await _context.Cupones.FindAsync(chargeObj.IdCupon);
                descuento = cupon.Monto;
                total = amount - descuento;
            }
            else { total = amount; }

            //Guardar pago en la base de datos
            var loginTransaction = _context.Database.BeginTransaction();

            var newPayment = new Pago
            {
                Fecha = dateTime,
                IdServicio = chargeObj.IdServicio,
                ServicioName = serviceToPay.ServicioTipoName,
                IdCliente = chargeObj.IdCliente,
                ClienteRazonSocial = client.RazonSocial,
                ClienteDireccion = client.Direccion,
                ClienteRFC = client.RFC,
                Total = total,
                Monto = amount,
                Descuento = descuento,
                Folio = Globals.PIN("1234567890", 8)
            };
            await _context.Pagos.AddAsync(newPayment);
            await _context.SaveChangesAsync();
            //Console.WriteLine($"Se guardaron los primeros datos y se asignó el numero de folio: {newPayment.NroFolio}");

            //cambiamos el estado del cupón a vencido
            if (cupon.IdCupon > 0 && cupon.Status != CuponEstatus.Vencido)
            {
                cupon.Status = CuponEstatus.Vencido;
            }
            
            //actualizamos la vigencia del servicio
            serviceToPay.Vigencia = dateTime.AddDays(30);


            //Creamos el cargo en la api de stripe
            var result = new Charge();
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = total,
                    Currency = "mxn",
                    Source = chargeObj.IdCard,
                    Customer = customer,
                };

                var service = new ChargeService();
                result = service.Create(options);
            }
            catch (StripeException ex) { StripeExceptionHandler.OnException(ex); }
            catch (Exception ex) { throw new ArgumentException("Error en el pago: p-03" + ex); }
            //Console.WriteLine("Se guardó el cargo en la API de stripe y se almacenó la respuesta");

            //asignamos el cargo al registro correspondiente
            newPayment.IdStripeCard =           result.PaymentMethod;
            newPayment.IdStripeCharge =         result.Id;
            newPayment.TarjetaMarca =           result.PaymentMethodDetails.Card.Brand;
            newPayment.TarjetaFinanciacion =    result.PaymentMethodDetails.Card.Funding;
            newPayment.TarjetaTerminacion =     result.PaymentMethodDetails.Card.Last4;
            newPayment.TarjetaBanco =           result.PaymentMethodDetails.Card.Issuer;
            newPayment.TarjetaTitular =         result.BillingDetails.Name;
            newPayment.CargoObj =               result.RawJObject.ToString();
            //Console.WriteLine($"se guardo el objeto de la respuesta {newPayment.CargoObj}");
            //throw new ArgumentException($"Lanzando error para lectura {newPayment.TarjetaTitular}");

            await _context.SaveChangesAsync();
            loginTransaction.Commit(); 

            //Correo de confirmación
            string claimEmail = Globals.GetClaim("Email", user);
            Action b = () => ConfirmationEmail(new ConfirmationEmailReq
            {
                Email =             claimEmail,
                RazonSocial =       client.RazonSocial,
                ServicioName =      serviceToPay.ServicioTipoName,
                Last4 =             result.PaymentMethodDetails.Card.Last4,
                CardFunding =       result.PaymentMethodDetails.Card.Funding,
                Fecha =             dateTime,
                Monto =             amount,
                Descuento =         descuento,
                Total =             total,
            }); 
            
            var send = Task.Run((Action) b);

            return new 
            {
                Fecha =             newPayment.Fecha,
                IdServicio =        newPayment.IdServicio,
                IdCliente =         newPayment.IdCliente,
                IdStripeCharge =    newPayment.IdStripeCharge,
                IdStripeCard =      newPayment.IdStripeCard,
                Monto =             newPayment.Monto,
                Descuento =         newPayment.Descuento, 
            };
        }

        public void ConfirmationEmail(ConfirmationEmailReq data)
        {
            var body = ConfirmacionDePago.Html(
                            data.ServicioName,
                            data.RazonSocial,
                            data.Monto,
                            data.Descuento,
                            data.Total,
                            data.Last4,
                            data.CardFunding,
                            data.Fecha
                        );
            try
            {
                var path = _root.ContentRootPath + "\\Files\\prueba.txt";
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                dynamic file = new ModelAttachment
                {
                    type = "bytes",
                    bytes = bytes,
                    fileName = "Recibo-prueba",
                    extension = "txt"
                };

                EmailManager objMailManager = new EmailManager(ConfigEmail.Data());
                objMailManager.html(data.Email, "Confirmación de pago", body, file);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Problemas en el envio de correos: " + ex);
            }
        }

    }
}