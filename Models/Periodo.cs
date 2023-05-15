using System.ComponentModel.DataAnnotations;
namespace workcube_pagos.Models
{
    public class Periodo
    {
        [Key]
        public int IdPeriodo { get; set; }
        public string PeriodoName { get; set; }
        public List<Servicio> Servicio { get; set; }
    }
}
