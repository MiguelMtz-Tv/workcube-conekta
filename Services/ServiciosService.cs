using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;


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

    }
}
