using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; } 
        public DateTime Fecha { get; set; }
        public virtual Servicio Servicio { get; set; }  
        public int IdServicio { get; set; }
        public string IdStripeCard { get; set; }
        public long Monto { get; set; }
        public decimal descuento { get; set; }  


    }
}
