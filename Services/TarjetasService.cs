using workcube_pagos.ViewModel.Req.Tarjeta;

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
            if (client == null) { return null; }

            var idCustomer = client.StripeCustomerID;

            var service = new CardService();
            var options = new CardListOptions();

            try
            {
                return service.List(idCustomer, options)
                    .Select(c => new
{
                        id = c.Id,
                        brand = c.Brand,
                        expYear = c.ExpYear,
                        expMonth = c.ExpMonth,
                        name = c.Name,
                        last4 = c.Last4,
                    });
            }catch(StripeException ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> Delete(DeleteCardReq cardObj)
        {
            var cliente = await _context.Clientes.FindAsync(cardObj.IdCliente);
            if (cliente == null) { return null; }

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

            // opciones de creación de la tarjeta
            var options = new CardCreateOptions {Source = cardObj.token,};
            var service = new CardService();

            var updatedCard = new Card();

            try
            {
                //Creamos la tarjeta en stripe
                var newcard = service.Create(IdCustomer, options);

                //actaulizar la tarjeta creada
                if (newcard == null) { return "No fue posible crear la tarjeta en la api de stripe"; }
                var updateCardOptions = new CardUpdateOptions
                {
                    Name = cardObj.name,
                };
                updatedCard = service.Update(IdCustomer, newcard.Id, updateCardOptions);

                return updatedCard;
            }
            catch (StripeException ex) { return ex; }
        }

        public async Task<object> UpdateCard(UpdateCardReq cardObj)
        {
            var client = await _context.Clientes.Where(c => c.IdCliente == cardObj.IdCliente).FirstOrDefaultAsync();
            if (client == null) { return null; }
            
            var IdCustomer = client.StripeCustomerID;

            var options = new CardUpdateOptions
            {
                Name = cardObj.Name,
                ExpMonth = cardObj.ExpMonth,
                ExpYear = cardObj.ExpYear,
            };

            var service = new CardService();

            try
            {
                var res = service.Update(IdCustomer, cardObj.CardStripeId, options);
                return res;
            }
            catch (StripeException ex)
            {
                return ex;
            }
        }

        public async Task<object> GetCard(UpdateCardReq cardObj)
        {
            var cliente = await _context.Clientes.FindAsync(cardObj.IdCliente);
            if (cliente == null) { return null; }

            var service = new CardService();
            try
            {
                var result = service.Get(cliente.StripeCustomerID, cardObj.CardStripeId);
                return result;
                
            }
            catch(StripeException ex)
            {
                return ex;
            }
        }
    }
}
