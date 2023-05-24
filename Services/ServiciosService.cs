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


        //para obtener los servicios de un cliente
        public async Task<List<Servicio>> GetClientServices(int UserId)
        {
            return await _context.Servicios.Where(s => s.IdCliente == UserId).ToListAsync();
        }

        //para obtener los detalles de un servicio
        public async Task<Servicio> GetService(GetServiceReq model)
        {
            var result = await _context.Servicios.Where(s => s.IdServicio == model.IdServicio && s.IdCliente == model.IdCliente).FirstOrDefaultAsync();

            if (result == null)
            {
                return null;
            }
            return result;
        }

        //para cancelar un servicio
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
