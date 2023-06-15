using System.Security.Policy;

namespace workcube_pagos.Templates.Emails
{
    public class ConfirmacionDePago
    {
        public static string Html(string servicioName, string clienteName, long monto, long descuento, long total, string last4, DateTime fecha)
        {
            var html="<body style='padding:20px !important; margin:0 !important; display:block !important; min-width:100% !important; width:100% !important; background:#f5f5f5; -webkit-text-size-adjust:none; font-family: sans-serif;'>" +
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
                            "<td style='padding-bottom: 10px; color: #1e293b;'>"+servicioName+"</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'> Monto </td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b;' > "+monto+"MXN</td>" +
                        "</tr >" +
                       "<tr >" +
                            "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'> Descuento </td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b;' > "+descuento+ "MXN</td>" +
                        "</tr >" +
                    "</table >" +
                    "<hr style='width: 200px; color: #ffffff; border:1px solid rgb(209, 209, 209);'/>" +
                    "<table style='margin: auto;'>" +
                        "<tr style='border-top: 1px solid black;'>" +
                           "<td style='padding-right: 70px; padding-bottom: 10px; color: #1e293b;'> Total:</td>" +
                            "<td style='padding-bottom: 10px; color: #1e293b; font-weight: 600;'> "+total+"MXN</td>" +
                        "</tr>" +
                    "</table>" +

                   "<div style='margin: auto; width: fit-content; margin-top: 20px; color: #696969;'> Realizado el "+ fecha.ToString("MM/dd/yyyy h:mm tt") +"</div>" +
                    "<div style='margin: auto; width: fit-content; color: #696969;'> pagado con Visa terminada en "+last4+"</div>" +

                    "<table width='100%' border='0' cellspacing='0' cellpadding='0' >" +
                        "<tr>" +
                            "<td class='p30-15 bbrr' style='padding: 30px 30px; border-radius:0px 0px 26px 26px;' bgcolor='white'>" +
                               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                                    "<tr>" +
                                        "<td class='text-footer1 pb10' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:12px; text-align:center; font-weight: bolder;'><multiline>Oil Field Trainning and Certification S.A.de C.V.</multiline></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td class='text-footer2' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:12px; text-align:center;'>" +
                                            "<multiline>Carretera Federal Coatzacoalcos a Villahermosa Km. 165 Col.Anacleto Canabal 1a Sección. CP. 86220</multiline>" +
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

//"<body style='padding:0 !important; margin:0 !important; display:block !important; min-width:100% !important; width:100% !important; background:#f5f5f5; -webkit-text-size-adjust:none;'>" +
//                "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='background-color: #f5f5f5'>" +
//                    "<tr>" +
//                        "<td align='center' valign='top'>" +
//                            "<table width='650' border='0' cellspacing='0' cellpadding='0' class='mobile-shell'>" +
//                                "<tr>" +
//                                    "<td class='td container' style='width:650px; min-width:650px; margin:0; font-weight:normal; padding:55px 0px;'>" 
//                                        "<repeater>" +
//                                            "<layout label='Intro'>" +
//                                                "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
//                                                    "<tr>" +
//                                                        "<td>" +
//                                                            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
//                                                                "<tr>" +
//                                                                    "<td class='tbrr p30-15' style='padding: 30px 30px; border-radius:26px 26px 0px 0px;' bgcolor='white'>" +
//                                                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
//                                                                            "<tr>" +
//                                                                                "<td class='h1 pb25' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:24px; text-align:center; padding-bottom:16px;'><multiline>Hola, " + "</multiline></td>" +
//                                                                            "</tr>" +
//                                                                            "<tr>" +
//                                                                                "<td class='text-center pb25' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:16px; text-align:center; padding-bottom:25px;'><multiline>Recientemente solicitó un restablecimiento de contraseña para su cuenta. Para restablecer la contraseña, haga clic en el botón de abajo.</multiline></td>" +
//                                                                            "</tr>" +

//                                                                            "<tr>" +
//                                                                                "<td align='center' style='padding-bottom: 15px;'>" +
//                                                                                    "<table class='center' border='0' cellspacing='0' cellpadding='0' style='text-align:center;'>" +
//                                                                                        "<tr>" +
//                                                                                            "<td class='pink-button text-button' style='background:#37a58d; color:white; font-family: Arial,sans-serif; font-size:14px; line-height:18px; padding:12px 30px; text-align:center; border-radius:22px 22px 22px 22px; font-weight:bold;'><multiline><a href='" + url + "' target='_blank' class='link-white' style='color:#ffffff; text-decoration:none;'><span class='link-white' style='color:#ffffff; text-decoration:none;'>Restablecer contraseña</span></a></multiline></td>" +
//                                                                                        "</tr>" +
//                                                                                    "</table>" +
//                                                                                "</td>" +
//                                                                            "</tr>" +
//                                                                            "<tr>" +
//                                                                                "<td class='text-center pb25' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:16px; text-align:center;'><multiline>El enlace caducará en 30 minutos</multiline></td>" +
//                                                                            "</tr>" +

//                                                                        "</table>" +
//                                                                    "</td>" +
//                                                                "</tr>" +
//                                                            "</table>" +
//                                                        "</td>" +
//                                                    "</tr>" +
//                                                "</table>" +
//                                            "</layout>" +

//                                        "</repeater>" +


//                                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
//                                            "<tr>" +
//                                                "<td class='p30-15 bbrr' style='padding: 30px 30px; border-radius:0px 0px 26px 26px;' bgcolor='white'>" +
//                                                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
//                                                        "<tr>" +
//                                                            "<td class='text-footer1 pb10' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:12px; text-align:center; font-weight: bolder;'><multiline>Oil Field Trainning and Certification S.A. de C.V.</multiline></td>" +
//                                                        "</tr>" +
//                                                        "<tr>" +
//                                                            "<td class='text-footer2' style='color:#5a5a5a; font-family: Arial,sans-serif; font-size:12px; text-align:center;'>" +
//                                                                "<multiline>Carretera Federal Coatzacoalcos a Villahermosa Km. 165 Col. Anacleto Canabal 1a Sección. CP. 86220</multiline>" +
//                                                            "</td>" +
//                                                        "</tr>" +
//                                                    "</table>" +
//                                                "</td>" +
//                                            "</tr>" +
//                                        "</table>" +

//                                    "</td>" +
//                                "</tr>" +
//                            "</table>" +
//                        "</td>" +
//                    "</tr>" +
//                "</table>" +
//            "</body>";