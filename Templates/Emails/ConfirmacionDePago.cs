using System.Security.Policy;
using workcube_pagos.ViewModel.Req.Pago;

namespace workcube_pagos.Templates.Emails
{
    public class ConfirmacionDePago
    {
        public static string Html(ConfirmationEmailReq data)
        {
            var html=
            "<body style='padding:20px !important; margin:0 !important; display:block !important; min-width:100% !important; width:100% !important; background:#f5f5f5; -webkit-text-size-adjust:none; font-family: sans-serif;'>" +
                   "<div style='width: 500px;background-color: #ffffff;border-radius: 15px;margin: auto;margin-top: 40px;'>" +
                        "<div style='background-color: #6366f1; border-top-left-radius: 15px;border-top-right-radius: 15px;height: 100px;text-align: center;font-weight: 500;color: #ffffff; text-align: center;'>" +
                        "<svg xmlns='http://www.w3.org/2000/svg' style='margin-top:10px' fill='none' viewBox='0 0 24 24' stroke-width='1.5' stroke='currentColor'>" +
                            "<path stroke-linecap='round' stroke-linejoin='round' d='M9 12.75L11.25 15 15 9.75M21 12a9 9 0 11-18 0 9 9 0 0118 0z'/>" +
                        "</svg>" +
                        "<p style='padding-top:30px; font-size: 24px;' > Su pago se realizó con exito</p>" +
                    "</div>" +
                    "<table style='margin: auto; margin-top: 40px;' >" +
                       "<tr>" +
                            "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'>Servicio</td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b;'>"+data.ServicioName+"</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'> Monto </td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b;' > "+data.Monto+"MXN</td>" +
                        "</tr >" +
                       "<tr >" +
                            "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'> Descuento </td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b;' > "+data.Descuento+ "MXN</td>" +
                        "</tr >" +
                    "</table >" +
                    "<hr style='width: 200px; color: #ffffff; border:1px solid rgb(209, 209, 209);'/>" +
                    "<table style='margin: auto;'>" +
                        "<tr style='border-top: 1px solid black;'>" +
                           "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'> Total:</td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b; font-weight: 600;'> "+data.Total+"MXN</td>" +
                        "</tr>" +
                    "</table>" +
                    
                    "<div style='margin: auto; width: fit-content; margin-top: 20px; color: #696969;'>"+data.RazonSocial+"</div>" +
                    
                    "<div style='margin: auto; width: fit-content; margin-top: 20px; color: #696969;'> Realizado el "+ data.Fecha.ToString("MM/dd/yyyy h:mm tt") +"</div>" +
                    "<div style='margin: auto; width: fit-content; color: #696969;'> pagado con "+data.CardBrand+" "+data.CardFunding+" terminada en "+data.Last4+"</div>" +
                    "<div style='margin: auto; width: fit-content; color: #696969;'>Dirección: "+data.Direccion+"</div>" +

                    "<table width='100%' border='0' cellspacing='0' cellpadding='0' >" +
                        "<tr>" +
                            "<td class='p30-15 bbrr' style='padding: 30px 30px; border-radius:0px 0px 26px 26px;' bgcolor='white'>" +
                               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                    "<tr>" +
                                        "<td class='text-footer2' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:12px; text-align:center;'>" +
                                            "<multiline>DESARROLLOS DE INGENIERIA EN SISTEMAS TECNOLOGICOS Y CONTROL INTEGRAL S.A. DE C.V.Av. de Los Ríos 201, Los Rios, 86035 Villahermosa, Tab., México</multiline>" +
                                        "</td>" +
                                    "</tr>" +
                                "</table>" +
                           "</td>" +
                        "</tr>" +
                    "</table>" +
                "</div>" +
            "</body>";

            return html;
        }
    }
}