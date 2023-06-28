using OfficeOpenXml.ConditionalFormatting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace workcube_pagos.Libraries
{
    public class StripeExceptionHandler
    {
        public static dynamic OnException(StripeException e)
        {
            if (e.StripeError.Type == "card_error")
            {
               
                switch (e.StripeError.DeclineCode)
                {
                    case "incorrect_cvc":
                        throw new ArgumentException("CVC incorrecto");
                    case "expired_card":
                        throw new ArgumentException("Metodo de pago expirado");
                    case "invalid_amount":
                        throw new ArgumentException("El monto excede el saldo disponible");
                    case "testmode_decline":
                        throw new ArgumentException("No se permite usar tarjetas de prueba");
                    case "restricted_card":
                        throw new ArgumentException("Esta tarjeta se encuentra restringida");
                    case "card_declined":
                        throw new ArgumentException("Metodo de pago rechazado por el proveedor");
                    case "fraudulent":                            
                        throw new ArgumentException("El pago fue rechazado por sospecha de fraude");
                    case "withdrawal_count_limit_exceeded":
                        throw new ArgumentException("Al parecer se ha excedido el limite de credito del metodo de pago");
                    case "not_permited":
                        throw new ArgumentException("El pago no fue permitido, contacta a tu banco para obtener mas información");
                    case "try_again_later":
                        throw new ArgumentException("El metodo de pago fue rechazado por razones desconocidas. Por favor, intenta mas tarde");
                    case "card_not_supported":
                        throw new ArgumentException("De momento no contamos con soporte para esta tarjeta");
                    case "insufficient_funds":
                        throw new ArgumentException("De momento no contamos con soporte para esta tarjeta");
                    default: 
                        throw new ArgumentException("Algo salió mal con el metodo de pago");
                }
            }
            else
            {
                switch(e.StripeError.Type)
                {
                    case "invalid_request_error":
                        throw new ArgumentException("Se realizó una solicitud invalida.");
                    case "api_connection_error":
                        throw new ArgumentException("There was a network problem between your server and Stripe.");
                    case "api_error":
                        throw new ArgumentException("Algo salio mal al intentar conectar con Stripe");
                    case "rate_limit_error":
                        throw new ArgumentException("Se hicieron demasiadas peticiones en muy poco tiempo");
                    default:
                        throw new ArgumentException("Error desconocido.");
                };
            }

        }
    }
}
