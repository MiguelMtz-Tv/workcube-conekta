using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using workcube_pagos.ViewModel.Req;

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
            return await _context.Servicios.Where(s => s.IdCliente == Id).ToListAsync();
        }

        public async Task<Servicio> CancelService(CancelServiceReq model) 
        {
            var serviceToCancel = await _context.Servicios.Where(s => s.IdCliente == model.IdCliente && s.IdServicio == model.IdServicio).FirstOrDefaultAsync();

            if (serviceToCancel == null)
            {
                return null;
            }
            
            serviceToCancel.IdServicioEstatus = 3;  
                serviceToCancel.ServicioEstatusName = "Cancelado";
                await _context.SaveChangesAsync();
                return serviceToCancel;
        }

    }
}
