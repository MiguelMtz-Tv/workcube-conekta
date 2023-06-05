using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workcube_pagos.Models
{
    public class Pago
    {
        [Key]
        public int IdPago                   { get; set; } 
        public DateTime Fecha               { get; set; }
        public virtual Servicio Servicio    { get; set; }  
        public int IdServicio               { get; set; }
        public virtual Cliente Cliente      { get; set; }
        public int IdCliente                { get; set; }
        public string IdStripeCard          { get; set; }
        public long Monto                   { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Descuento            { get; set; }  

    }
}
