using Microsoft.EntityFrameworkCore;

namespace workcube_pagos.Services
{
    public class ServiciosService
    {
        private readonly DataContext _context;

        public ServiciosService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio>> GetClientServices(int idCliente)
        {
            var services = await _context.Servicios.Where(services => services.IdCliente == idCliente).ToListAsync();
            if(services == null)
            {
                return null;
            }
            return services;

        }

    }
}
