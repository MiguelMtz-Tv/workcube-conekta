using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class Tarjeta
    {
        [Key]
        public int IdTarjeta { get; set; } 
        public string StripeCardID { get; set; } 
        public virtual Cliente Cliente { get; set; }
        public int IdCliente { get; set; }  
    }
}