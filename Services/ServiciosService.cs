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

        public async Task<List<Servicio>> GetClientServices(int Id)
        {
            var services = await _context.Servicios.Where(services => services.IdCliente == Id).ToListAsync();
            if(services == null)
            {
                return null;
            }
            return services;

        }

        public async Task<List<Servicio>> GetView()
        {
            var query = from s in _context.Servicios
                        join st in _context.ServicioTipos on s.IdServicioTipo equals st.IdServicioTipo
                        join c in _context.Clientes on s.IdCliente equals c.IdCliente
                        join p in _context.Periodos on s.IdPeriodo equals p.IdPeriodo
                        select new Servicio
                        {
                            IdServicio = s.IdServicio,
                            IdServicioTipo = st.IdServicioTipo,
                            IdCliente = c.IdCliente,
                            IdPeriodo = p.IdPeriodo
                        };
            return query.ToList();
        }

    }
}
