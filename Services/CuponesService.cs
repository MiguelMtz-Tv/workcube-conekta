using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using workcube_pagos.Models;
using workcube_pagos.ViewModel.Req;

namespace workcube_pagos.Services
{
    public class CuponesService
    {
        private readonly DataContext _context;

        public CuponesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Cupon> GetCupon(GetCuponReq model)
        {
            var result = await _context.Cupones.Where(c => c.Codigo == model.Codigo && c.IdCliente == model.IdCliente).FirstOrDefaultAsync();
   
            if (result == null) {
                return null;
            }
            return result;
        }
    }
}


