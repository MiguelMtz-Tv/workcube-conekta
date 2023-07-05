using Workcube.Libraries;
using Workcube.ViewModels;
using workcube_pagos.Templates.Emails;
using workcube_pagos.ViewModel.Req.Pago;
using workcube_pagos.ViewModel.Statics;
using workcube_pagos.Libraries;
using System.Security.Claims;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            if (serviceToPay == null) { throw new ArgumentException("Error en pago: P-01"); }
            if (serviceToPay.Vigencia > dateTime) { throw new ArgumentException("No puedes pagar este servicio porque aún se encuentra vigente"); }

            //obtener al cliente
            var client = await _context.Clientes.FindAsync(chargeObj.IdCliente) ?? throw new ArgumentException("Error en pago: P-02 - Cliente nulo");
            var customer = client.StripeCustomerID;
            decimal amount = serviceToPay.ServicioTipoCosto;
            decimal total;

            //obtener el cupon (si existe) y realizar el descuento
            decimal descuento =    0;
            var cupon =         new Cupon();

            if (chargeObj.AreCupon && chargeObj.IdCupon > 0)
            {
                cupon =         await _context.Cupones.FindAsync(chargeObj.IdCupon);
                descuento =     cupon.Monto;
                total =         (amount - descuento);
            }
            else { total = amount; }

            //Guardar pago en la base de datos
            var loginTransaction = _context.Database.BeginTransaction();
            
            //generamos un nuevo folio para el registro
            var folio = Globals.PIN("1234567890", 8);

            var newPayment = new Pago
            {
                Fecha =                 dateTime,
                IdServicio =            chargeObj.IdServicio,
                ServicioName =          serviceToPay.ServicioTipoName,
                IdCliente =             chargeObj.IdCliente,
                ClienteRazonSocial =    client.RazonSocial,
                ClienteDireccion =      client.Direccion,
                ClienteRFC =            client.RFC,
                Total =                 total,
                Monto =                 amount,
                Descuento =             descuento,
                Folio =                 folio,
            };
            await _context.Pagos.AddAsync(newPayment);
            await _context.SaveChangesAsync();
            //Console.WriteLine($"Se guardaron los primeros datos y se asignó el numero de folio: {newPayment.NroFolio}");

            //cambiamos el estado del cupón a vencido
            if (cupon.Status == CuponEstatus.Disponible)
            {
                cupon.Status = CuponEstatus.Usado;
            }
            
            //actualizamos la vigencia del servicio
            serviceToPay.Vigencia = dateTime.AddDays(30);


            //Creamos el cargo en la api de stripe
            var result = new Charge();
            var totalToLong = (long)(100*total);
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = totalToLong,
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

            try
            {
                //generacion de pdf
                byte[] pdfBytes = Recibopdf(newPayment.IdPago);

                //datos para envio de correo
                string claimEmail = Globals.GetClaim("Email", user);
                var reciboData = new ConfirmationEmailReq
                {
                    Email = claimEmail,
                    RazonSocial = client.RazonSocial,
                    ServicioName = serviceToPay.ServicioTipoName,
                    Last4 = result.PaymentMethodDetails.Card.Last4,
                    CardBrand = result.PaymentMethodDetails.Card.Brand,
                    CardFunding = result.PaymentMethodDetails.Card.Funding,
                    Fecha = dateTime,
                    Monto = amount,
                    Descuento = descuento,
                    Total = total,
                    Direccion = client.Direccion,
                    Folio = folio,
                };

                //Correo de confirmación
                Action b = () => ConfirmationEmail(reciboData, pdfBytes);
                //envio de correo en segundo plano
                var send = Task.Run((Action)b);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante el envio del correo: " + ex.Message);
            }

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

        public void ConfirmationEmail(ConfirmationEmailReq data, byte[] pdfBytes)
        {
            var body = ConfirmacionDePago.Html(data);
            try
            {
                dynamic pdf = new ModelAttachment
                {
                    type = "bytes",
                    bytes = pdfBytes,
                    fileName = "Recibo_de_pago",
                    extension = "pdf"
                };

                EmailManager objMailManager = new EmailManager(ConfigEmail.Data());
                objMailManager.html(data.Email, "Confirmación de pago. Folio: " + data.Folio, body, pdf);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public byte[] Recibopdf(int id)
        {
            var paymentObj = _context.Pagos.Find(id);

            // INSTANCIAS
            PdfPTable table = null;
            PdfPCell cell = null;


            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.Letter, 40f, 40f, 80f, 20f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                table = new PdfPTable(1);

                string imageFile = Path.Combine(_root.WebRootPath + "\\Images\\LOGOPIXL.png");
                Image image = Image.GetInstance(imageFile);
                image.ScaleToFit(30f, 30f);
                image.Alignment = Element.ALIGN_CENTER;

                cell = new PdfPCell();
                Paragraph Title = new Paragraph();
                Title.Add(new Chunk("Recibo de pago"));
                Title.Alignment = Element.ALIGN_CENTER;

                cell.AddElement(image);
                cell.AddElement(Title);
                cell.Border = 0;
                table.AddCell(cell);

                table.SpacingAfter = 10f;
                document.Add(table);

                /***********************/
                table = new PdfPTable(4);
                cell = new PdfPCell();

                //direccion y fecha
                Paragraph pDireccion = new Paragraph();
                pDireccion.Add(new Phrase("Dirección: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));
                pDireccion.Add(new Phrase(paymentObj.ClienteDireccion, PDFFont.FontStyle(true, 9, "#272727", "Arial")));

                Paragraph pFecha = new Paragraph();
                pFecha.Add(new Phrase("Fecha: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));
                pFecha.Add(new Phrase(paymentObj.Fecha.ToShortDateString(), PDFFont.FontStyle(true, 9, "#272727", "Arial")));

                cell.AddElement(pDireccion);
                cell.AddElement(pFecha);

                cell.Border = 1;
                cell.Padding = 4;
                cell.Colspan = 2;
                table.AddCell(cell);

                //folio
                cell = new PdfPCell();

                Paragraph pFolio = new Paragraph();
                pFolio.Add(new Phrase("Folio: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));
                pFolio.Add(new Phrase(paymentObj.Folio, PDFFont.FontStyle(true, 9, "#272727", "Arial")));
                
                Paragraph pCliente = new Paragraph();
                pCliente.Add(new Phrase("Cliente: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));
                pCliente.Add(new Phrase(paymentObj.ClienteRazonSocial, PDFFont.FontStyle(true, 9, "#272727", "Arial")));

                cell.AddElement(pFolio);

                cell.Border = 1;
                cell.Padding = 4;
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                //tabla informacion de compra
                table = new PdfPTable(4);
                cell = new PdfPCell();

                Paragraph pServicio = new Paragraph();
                pServicio.Add(new Phrase("Servicio: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));

                Paragraph pMonto = new Paragraph();
                pMonto.Add(new Phrase("Monto: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));

                Paragraph pDescuento = new Paragraph();
                pDescuento.Add(new Phrase("Descuento: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));

                Paragraph pTotal = new Paragraph();
                pTotal.Add(new Phrase("Total: ", PDFFont.FontStyle(false, 9, "#272727", "Arial")));

                cell.AddElement(pServicio);
                cell.AddElement(pMonto);
                cell.AddElement(pDescuento);
                cell.AddElement(pTotal);

                cell.Border = 1;
                cell.Padding = 4;
                cell.Colspan = 2;

                table.AddCell(cell);
                table.HorizontalAlignment = 20;
                document.Add(table);

                //datos de tabla informacion de compra
                cell = new PdfPCell();
                Paragraph pServicioName = new Paragraph();
                pServicioName.Add(new Phrase(paymentObj.ServicioName, PDFFont.FontStyle(false, 9, "#272727", "Arial")));

                Paragraph pMontoMXN = new Paragraph();
                pMontoMXN.Add(new Phrase(paymentObj.Monto.ToString() + "MXN", PDFFont.FontStyle(true, 9, "#272727", "Arial")));

                Paragraph pDescuentoMXN = new Paragraph();
                pDescuentoMXN.Add(new Phrase(paymentObj.Descuento.ToString() + "MXN", PDFFont.FontStyle(true, 9, "#272727", "Arial")));

                Paragraph pTotalMXN = new Paragraph();
                pTotalMXN.Add(new Phrase(paymentObj.Total.ToString() + "MXN", PDFFont.FontStyle(true, 9, "#272727", "Arial")));

                cell.AddElement(pServicioName);
                cell.AddElement(pMontoMXN);
                cell.AddElement(pDescuentoMXN);
                cell.AddElement(pTotalMXN);

                cell.Border = 1;
                cell.Padding = 4;
                cell.Colspan = 2;

                table.AddCell(cell);
                table.SpacingBefore = 10f;
                document.Add(table);

                //informacion de metodo de pago
                table = new PdfPTable(1);
                cell = new PdfPCell();

                Paragraph paymentMethodInfo = new Paragraph();
                paymentMethodInfo.Add(new Phrase("Pagado con " + paymentObj.TarjetaMarca + " " + paymentObj.TarjetaFinanciacion + " terminada en " + paymentObj.TarjetaTerminacion, PDFFont.FontStyle(true, 9, "#272727", "Arial")));
                paymentMethodInfo.Alignment = Element.ALIGN_CENTER;
                paymentMethodInfo.SpacingBefore = 20f;

                Paragraph direccion = new Paragraph();
                direccion.Add(new Phrase("DESARROLLOS DE INGENIERIA EN SISTEMAS TECNOLOGICOS Y CONTROL INTEGRAL S.A. DE C.V.Av. de Los Ríos 201, Los Rios, 86035 Villahermosa, Tab., México", PDFFont.FontStyle(true, 9, "#272727", "Arial")));
                direccion.Alignment = Element.ALIGN_CENTER;
                direccion.SpacingBefore = 10f;

                cell.AddElement(paymentMethodInfo);
                cell.AddElement(direccion);
                cell.Border = 1;
                table.SpacingBefore = 15f;
                cell.Padding = 4;
                table.AddCell(cell);  
                document.Add(table);    

                document.Close();
                return memoryStream.ToArray();
            }
        }
    }
}