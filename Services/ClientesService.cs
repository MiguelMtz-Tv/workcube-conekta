using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Stripe;
using System.Text.Json.Nodes;
using workcube_pagos.ViewModel.Req;
using workcube_pagos.ViewModel.Res;

namespace workcube_pagos.Services
{
   
    public class ClientesService
    {
        private readonly DataContext _context;
        public ClientesService(DataContext dataContext) {
            _context = dataContext;
        }

        public async Task<CreateClientRes> CreateClient(CreateClientReq ClientObj)
        {
            var service = new CustomerService();
            var options = new CustomerCreateOptions
            {
                Name =          ClientObj.NombreComercial,
                Email =         ClientObj.Correo,
                Phone =         ClientObj.Telefono,
            };
            string idStripe = "";
            try
            {
                var stripeClient = service.Create(options);

                if (string.IsNullOrEmpty(stripeClient.Id)) { throw new ArgumentException("Error al cliente: C-01"); }

                idStripe = stripeClient.Id;
            } catch (Exception ex)
            {
                throw new ArgumentException("Error al cliente: C-02");
            }

            var newCLiente = new Cliente{
                RFC =                   ClientObj.RFC,
                RazonSocial =           ClientObj.RazonSocial,
                NombreComercial =       ClientObj.NombreComercial,
                NumeroContrato =        ClientObj.NumeroContrato,
                Telefono =              ClientObj.Telefono,
                Correo =                ClientObj.Correo,
                Direccion =             ClientObj.Direccion,
                CodigoPostal =          ClientObj.CodigoPostal,
                Code =                  ClientObj.Code,
                IsActive =              true,
                StripeCustomerID =      idStripe
            };

            await _context.Clientes.AddAsync(newCLiente);
            await _context.SaveChangesAsync();

            // ELIMIANR ID CUSTOMER PREVIO
            // CODIGO PARA ELIMINAR CLIENTE ANTE

            return new CreateClientRes
            {
                RFC =                   newCLiente.RFC,
                RazonSocial =           newCLiente.RazonSocial,
                NombreComercial =       newCLiente.NombreComercial,
                NumeroContrato =        newCLiente.NumeroContrato,
                Telefono =              newCLiente.Telefono,
                Correo =                newCLiente.Correo,
                Direccion =             newCLiente.Direccion,
                CodigoPostal =          newCLiente.CodigoPostal,
                Code =                  newCLiente.Code,
                StripeCustomerID =      newCLiente.StripeCustomerID,
            };
        }
        
    }
}
