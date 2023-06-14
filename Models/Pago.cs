using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace workcube_pagos.Models
{
    public class Pago
    {
        [Key]
        public int IdPago                   { get; set; } 
        public DateTime Fecha               { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Servicio Servicio    { get; set; }  
        public int IdServicio               { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Cliente Cliente      { get; set; }
        public int IdCliente                { get; set; }
        public string IdStripeCard          { get; set; }
        public string IdStripeCharge        { get; set; }
        public long Monto                   { get; set; }
        public long Descuento               { get; set; }  
        public long Total                   { get; set; } 
    }
}
