using Workcube.Libraries;
using Workcube.ViewModels;
using workcube_pagos.Templates.Emails;
using workcube_pagos.ViewModel.Req.Pago;
using workcube_pagos.ViewModel.Res.Pago;
using workcube_pagos.ViewModel.Statics;

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
            if(payments ==  null) { return null; }
            return payments;
        }

        public async Task<ChargeRes> CreateCharge(CreateChargeReq chargeObj)
        {
            var serviceToPay = await _context.Servicios.FindAsync(chargeObj.IdServicio);
            if (serviceToPay == null
                || serviceToPay.IdServicioEstatus == 1
                || serviceToPay.ServicioEstatusName == "Vigente")
                { throw new ArgumentException("Error en pago: P-01"); }

            //obtener al cliente
            var client =        await _context.Clientes.FindAsync(chargeObj.IdCliente) ?? throw new ArgumentException("Error en pago: P-02 - Cliente nulo");
            var customer =      client.StripeCustomerID;
            long amount =       serviceToPay.ServicioTipoCosto;
            long total;

            //obtener el cupon (si existe) y realizar el descuento
            long descuento = 0;
            var cupon = new Cupon();

            if (chargeObj.AreCupon && chargeObj.IdCupon > 0)
            {
                cupon =         await _context.Cupones.FindAsync(chargeObj.IdCupon);
                descuento =     cupon.Monto;
                total =         amount - descuento;
            }
            else {total = amount;}

            //guardar pago en la base de datos
            var loginTransaction = _context.Database.BeginTransaction(); //empezamos transacción

            DateTime dateTime = DateTime.Now;
            var newPayment = new Pago
            {
                Fecha =                 dateTime,
                IdServicio =            chargeObj.IdServicio,
                IdCliente =             chargeObj.IdCliente,
                ClienteName =           client.NombreComercial,
                ClienteRazonSocial =    client.RazonSocial,
                ClienteDireccion =      client.Direccion,
                ClienteRFC =            client.RFC,
                Total =                 total,
                Monto =                 amount,
                Descuento =             descuento,  
            };
            await _context.Pagos.AddAsync(newPayment);

            //cambiamos el estado del cupón a vencido
            if (cupon.IdCupon > 0 && cupon.Status != CuponEstatus.Vencido)
            {
                cupon.Status = CuponEstatus.Vencido;
            }

            //Cambiamos el estatus del servicio
            serviceToPay.ServicioEstatusName =  "Vigente";
            serviceToPay.IdServicioEstatus =    1;
            serviceToPay.Vigencia =             dateTime.AddDays(30);


            //Creamos el cargo en la api de stripe
            var result = new Charge();
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount =    total,
                    Currency =  "mxn",
                    Source =    chargeObj.IdCard,
                    Customer =  customer,
                };

                var service = new ChargeService();
                result = service.Create(options);
            }
            catch (StripeException ex)
            {
                throw new ArgumentException("Error en el pago: p-03" + ex);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Error en el pago: p-04" + ex);
            }


            //asignamos el cargo al registro correspondiente
            newPayment.IdStripeCard =       result.PaymentMethod;
            newPayment.IdStripeCharge =     result.Id;
            newPayment.TarjetaTipo =        result.PaymentMethodDetails.Card.Brand;
            newPayment.TarjetaTerminacion = result.PaymentMethodDetails.Card.Last4;
            newPayment.TarjetaBanco =       result.PaymentMethodDetails.Card.Issuer;
            newPayment.CargoObj =           result.ToString();

            //guardamos las consultas
            await _context.SaveChangesAsync();
            loginTransaction.Commit(); //confirmamos transacción

            //enviamos correo de confirmación
            Action e = async () => await ConfirmationEmail(new ConfirmationEmailReq
            {
                ServicioName =  serviceToPay.ServicioTipoName,
                IdAspNetUser =  chargeObj.IdAspNetUser,
                IdCliente =     chargeObj.IdCliente,
                Last4 =         result.PaymentMethodDetails.Card.Last4,
                Fecha =         dateTime,
                Monto =         amount,
                Descuento =     descuento,
                Total =         total,
             });
            await Task.Run(e);

            return new ChargeRes
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

        public async Task ConfirmationEmail(ConfirmationEmailReq data)
        {
            AspNetUser objAspNetUser = await _context.AspNetUsers.FindAsync(data.IdAspNetUser);

            Cliente objCliente = await _context.Clientes.FindAsync(data.IdCliente);

            if (objAspNetUser != null && objCliente != null)
            { 
                string email =  objAspNetUser?.Email;
                var body = ConfirmacionDePago.Html(
                                data.ServicioName,
                                objCliente.RazonSocial,
                                data.Monto, 
                                data.Descuento,
                                data.Total,
                                data.Last4,
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
                    objMailManager.html(email, "Confirmación de pago", body, file);
                }
                catch( Exception ex )
                {
                    throw new ArgumentException("Problemas en el envio de correos: " + ex);
                }
            }
            else
            {
                throw new ArgumentException("No se encontró al usuario en sesión" + data.IdAspNetUser);
            }
        }

    }
}