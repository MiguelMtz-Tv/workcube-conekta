using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace workcube_pagos.Models
{
    public class Cliente
    {
        [Key] 
        public int IdCliente                    { get; set; }
        public string StripeCustomerID          { get; set; }
        public virtual AspNetUser Usuario       { get; set; }
        [JsonIgnore]
        public List<Servicio> Servicios         { get; set; }
        public List<Cupon> Cupones              { get; set; }
        [JsonIgnore]
        public List<Tarjeta> Tarjetas           { get; set; }
        public string RFC                       { get; set; }  
        public string RazonSocial               { get; set; }  
        public string NombreComercial           { get; set; }  
        public string NumeroContrato            { get; set; }  
        public string Telefono                  { get; set; }  
        public string Correo                    { get; set; }  
        public string Direccion                 { get; set; }  
        public string CodigoPostal              { get; set; }  
        public string Code                      { get; set; }  
        public bool IsActive                    { get; set; }
    }
}
