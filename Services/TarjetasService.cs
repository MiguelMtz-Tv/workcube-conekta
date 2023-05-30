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

        public async Task<AddCardRes> AddCard(AddCardReq cardObj)
        {
            //verificar al cliente
            var client = await _context.Clientes.Where(c => c.IdCliente == cardObj.IdCliente).FirstOrDefaultAsync();
            if (client == null) { return null; }
            var IdCustomer = client.StripeCustomerID;

            var options = new CardCreateOptions
            {
                Source = new TokenCardOptions 
                {
                    Number =        cardObj.number,
                    Name =          cardObj.name,
                    ExpMonth =      cardObj.exp_month,
                    ExpYear =       cardObj.exp_year,
                    Cvc =           cardObj.cvc_check,
                }
            };
            var service = new CardService();

            var newCard = service.Create(IdCustomer ,options);

            //verificar que se haya creado una tarjeta en la api de stripe
            if(newCard == null 
               || newCard.Id == "" 
               || newCard.Id == null 
               || newCard.Id == string.Empty) { return null; }

            //creación de la nueva tarjeta en la base de datos
            var newTarjeta = new Tarjeta
            {
                IdCliente = client.IdCliente,
                StripeCardID = newCard.Id,
            };

            await _context.Tarjetas.AddAsync(newTarjeta);
            await _context.SaveChangesAsync();

            return new AddCardRes
            {
                IdCliente = client.IdCliente,
                StripeCardID = newCard.Id,
            };
        }
    }
}
