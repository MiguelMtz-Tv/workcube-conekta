using Azure.Core;
using Microsoft.Identity.Client;
using Stripe;
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

            //guardar la tarjeta en la base de datos
            var toStoreCard = new Tarjeta
            {
                IdCliente = cardObj.idCliente,
                StripeCardID = updatedCard.Id,

            };
 
            var storedCard = await _context.Tarjetas.AddAsync(toStoreCard);
            await _context.SaveChangesAsync();

            if (updatedCard == null) { return "Algo salío mal mientras se asignaba un nombre a la tarjeta"; }
            return storedCard;
        }
    }
}
