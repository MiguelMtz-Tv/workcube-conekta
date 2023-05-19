using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace workcube_pagos.Models
{
    public class Periodo
    {
        [Key]
        public int IdPeriodo { get; set; }
        public string Name { get; set; }
        
        public virtual List<Servicio> Servicio { get; set; }
    }
}
