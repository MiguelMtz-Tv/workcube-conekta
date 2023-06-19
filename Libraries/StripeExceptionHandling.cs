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
               
                switch (e.StripeError.Code)
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
                    default: 
                        throw new ArgumentException("Algo salió mal con el metodo de pago");
                }
            }
            else
            {
                switch(e.StripeError.Type)
                {
                    case "invalid_request_error":
                        throw new ArgumentException("An invalid request occurred.");
                    case "api_connection_error":
                        throw new ArgumentException("There was a network problem between your server and Stripe.");
                    case "api_error":
                        throw new ArgumentException("Something went wrong on Stripe´s end");
                    case "rate_limit_error":
                        throw new ArgumentException("There are too many API calls in too short time");
                    default:
                        throw new ArgumentException("Another problem occurred, maybe unrelated to Stripe.");
                };
            }

        }
    }
}
