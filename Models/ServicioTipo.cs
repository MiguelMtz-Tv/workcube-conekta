using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workcube_pagos.Models
{
    public class ServicioTipo
    {
        [Key]
        public int IdServicioTipo { get; set; }
        public List<Servicio> Servicio { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set;}
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Costo { get; set; }

        
    }
}
