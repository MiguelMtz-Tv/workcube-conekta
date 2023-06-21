using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using workcube_pagos.Models;
using workcube_pagos.ViewModel.Req.Cupon;

namespace workcube_pagos.Services
{
    public class CuponesService
    {
        private readonly DataContext _context;

        public CuponesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Cupon> GetCupon(GetCuponReq model) //para obtener un cupón
        {
            var result = await _context.Cupones.Where(c => 
                c.Codigo ==     model.Codigo && 
                c.IdCliente ==  model.IdCliente)
                .FirstOrDefaultAsync();

            if(result.IdServicio != model.IdServicio)   { throw new ArgumentException("No puedes usar este cupón con este servicio"); }
            if(result.Vigencia < DateTime.Now)          { throw new ArgumentException("El cupón ya no se encuentra vigente"); }
            if(result.Status == CuponEstatus.Usado)     { throw new ArgumentException("Este cupón ya fue utilizado"); }             

            return result;
        }

        public async Task<List<Cupon>> List(GetCuponReq model) //Para obtener los cupones de un usuario
        {
            var result = await _context.Cupones.AsNoTracking().Where(c => 
                c.IdCliente == model.IdCliente && 
                c.Status == CuponEstatus.Disponible)
                .ToListAsync();
                
            return result;
        }
    }
}


