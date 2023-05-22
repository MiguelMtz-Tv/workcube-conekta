using Quartz;
using System.Threading.Tasks;
using workcube_pagos.Services;

namespace workcube_pagos.Jobs
{
    public class VerifyServicesExpiration : IJob
    {
        private readonly ServiciosService _serviciosService;

        public VerifyServicesExpiration(ServiciosService serviciosService)
        {
            _serviciosService = serviciosService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _serviciosService.UpdateStatusService();
            return Task.CompletedTask;
        }
    }
}
