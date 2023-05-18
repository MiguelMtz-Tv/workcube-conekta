using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using workcube_pagos.Models;
using workcube_pagos.Services;

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        public DataContext _context;

        public ServicioController( DataContext context)
        {
            _context = context;
        }

        [HttpGet("clientservices/{id}")]
        public async Task<List<Servicio>> GetClientServices(int Id)
        {
            var services = await _context.Servicios.Where(services => services.IdCliente == Id).ToListAsync();

            if (services == null)
            {
                return null;
            }
            return services;
        }
        
        [HttpGet]
        public async Task<List<Servicio>> GetAll()
        {
            var services = await _context.Servicios.ToListAsync();
            return services;
        }
    }
}
