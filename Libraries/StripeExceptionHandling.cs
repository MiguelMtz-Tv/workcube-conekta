using OfficeOpenXml.ConditionalFormatting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace workcube_pagos.Libraries
{
    public class StripeExceptionHandler
    {
        public static dynamic OnException(StripeException e)
        {
            switch(e.StripeError.Type)
            {
                case "card_error":
                    throw new ArgumentException($"A payment error occurred: {e.Message}");
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
