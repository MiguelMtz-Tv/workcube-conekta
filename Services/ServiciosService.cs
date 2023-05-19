using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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

        public async Task<Servicio> CancelService(int Id)
        {
            var serviceToCancel = await _context.Servicios.FindAsync(Id);
            if (serviceToCancel != null)
            {
                serviceToCancel.IdServicioEstatus = 3;  
                serviceToCancel.ServicioEstatusName = "Cancelado";
                _context.SaveChanges();
                return serviceToCancel;
            }
            return null;
        }

    }
}
