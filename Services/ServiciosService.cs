using OfficeOpenXml.ConditionalFormatting;
using workcube_pagos.ViewModel.Req.Servicio;

namespace workcube_pagos.Services
{
    public class ServiciosService
    {
        private readonly DataContext _context;

        public ServiciosService(DataContext context)
        {
            _context = context;
        }


        //para obtener los servicios de un cliente
        public async Task<dynamic> GetClientServices(int UserId)
        {
            var res = await _context.Servicios.AsNoTracking()
                .Where(s => s.IdCliente == UserId)
                .Select(s => new
                {
                    IdServicio =                s.IdServicio,
                    ServicioTipoCosto =         s.ServicioTipoCosto,
                    ServicioTipoName =          s.ServicioTipoName,
                    ServicioTipoDescripcion =   s.ServicioTipoDescripcion,
                    PeriodoName =               s.PeriodoName,
                    Vigencia =                  s.Vigencia,
                    IsVigente =                 s.Vigencia.Date > DateTime.Now.Date,
                    ServicioEstatusName =       s.ServicioEstatusName,
                })
                .ToListAsync();

            if(res == null) {throw new ArgumentException("Al parecer el cliente no cuenta con ningún servicio");}
            return res;
        }

        //para obtener los detalles de un servicio
        public async Task<dynamic> GetService(GetServiceReq model)
        {
            var result = await _context.Servicios.AsNoTracking().Where(s => 
            s.IdServicio == model.IdServicio && 
            s.IdCliente ==  model.IdCliente)
            .FirstOrDefaultAsync();

            if (result == null) {throw new ArgumentException("No se pudo recuperar este servicio");}

            return result;
        }

        //para cancelar un servicio
        public async Task CancelService(CancelServiceReq model) 
        {
            var loginTransaction = _context.Database.BeginTransaction();

            var serviceToCancel = await _context.Servicios.Where(s => 
                s.IdCliente ==  model.IdCliente && 
                s.IdServicio == model.IdServicio)
                .FirstOrDefaultAsync();

            if (serviceToCancel == null) {throw new ArgumentException("No se pudo recuperar este servicio para su cancelación");}
            
            serviceToCancel.IdServicioEstatus =     3;  
            serviceToCancel.ServicioEstatusName =   "Cancelado";

            await _context.SaveChangesAsync();

            loginTransaction.Commit();
        }

        //Verificar la fecha de un servicio y asignar su estatus
        public async Task<List<Servicio>> UpdateStatusService()
        {
            DateTime FechaActual = DateTime.Now;

            var ServiciosVencidos = await _context.Servicios
                .Where(s => s.Vigencia.Date <= FechaActual.Date && s.IdServicioEstatus == 1)
                .ToListAsync();

            if(ServiciosVencidos.Any() )
            {
                foreach(var servicio in ServiciosVencidos)
                {
                    servicio.IdServicioEstatus = 2;
                    servicio.ServicioEstatusName = "Vencido";

                    await _context.SaveChangesAsync();
                }
                return ServiciosVencidos;
            }

            return null;
        }

    }
}
