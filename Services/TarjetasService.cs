using Azure.Core;
using Microsoft.Identity.Client;
using Stripe;
using System.Net.WebSockets;
using workcube_pagos.ViewModel.Req;
using workcube_pagos.ViewModel.Res;

namespace workcube_pagos.Services
{
    public class TarjetasService
    {
        private readonly DataContext _context;

        public TarjetasService(DataContext context)
        {
            _context = context;
        }

        public async Task<object> List(int idCLient)
        {
           var client = await _context.Clientes.FindAsync(idCLient);
            if(client == null) { return null; }

           var idCustomer = client.StripeCustomerID;

            var service = new CardService();
            var options = new CardListOptions();

            var cards = service.List(idCustomer, options);

            return cards;
        }

        public async Task<object> Delete(DeleteCardReq cardObj)
        {
            var cliente = await _context.Clientes.FindAsync(cardObj.IdCliente);
            if(cliente == null){ return null; }

            var customerId = cliente.StripeCustomerID.ToString();

            var service = new CardService();
            var result = service.Delete(
                customerId,
                cardObj.CardId
            );

            return result;
        }

        public async Task<object> AddCard(AddCardReq cardObj)
        {
            //verificar al cliente
            var client = await _context.Clientes.Where(c => c.IdCliente == cardObj.idCliente).FirstOrDefaultAsync();
            if (client == null) { return "No se encontró al cliente"; }

            //obtener el Id del cliente en stripe
            var IdCustomer = client.StripeCustomerID;

            // Crea una tarjeta asociada al cliente en Stripe
            var options = new CardCreateOptions
            {
                Source = cardObj.token,
            };
            var service = new CardService();
            var newcard = service.Create(IdCustomer, options);

            //actaulizar la tarjeta creada
            if (newcard == null) { return "No fue posible crear la tarjeta en la api de stripe"; }
            var updateCardOptions = new CardUpdateOptions
            {
                Name = cardObj.name,
            };
            var updatedCard = service.Update(IdCustomer, newcard.Id, updateCardOptions);

            return updatedCard;
        }
    }
}
