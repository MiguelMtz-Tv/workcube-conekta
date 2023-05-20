using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class ServicioEstatus
    {
        [Key]
        public int IdServicioEstatus {  get; set; }
        public string Name { get; set; }
        public virtual List<Servicio> Servicios { get; set; }
    }
}
