using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
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
        public string ServicioName          { get; set; }    
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual Cliente Cliente      { get; set; }
        public int IdCliente                { get; set; }
        public string ClienteRazonSocial    { get; set; }
        public string ClienteRFC            { get; set; }
        public string ClienteDireccion      { get; set; }

        public string IdStripeCard          { get; set; }
        public string TarjetaMarca          { get; set; }
        public string TarjetaFinanciacion   { get; set; }
        public string TarjetaTerminacion    { get; set; } 
        public string TarjetaBanco          { get; set; }
        public string TarjetaTitular        { get; set; }  

        public string IdStripeCharge        { get; set; }
        public long Monto                   { get; set; }
        public long Descuento               { get; set; }  
        public long Total                   { get; set; } 
        public string CargoObj              { get; set; }   
        public string Folio              { get; set; }
    }
}
