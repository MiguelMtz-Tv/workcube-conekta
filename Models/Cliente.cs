using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Workcube.Generic;

namespace workcube_pagos.Models
{
    public class Cliente : UserCreated
    {
        [Key] 
        public int IdCliente                                { get; set; }
        public string StripeCustomerID                      { get; set; }
        public virtual AspNetUser Usuario                   { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Servicio> Servicios      { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Pago> Pagos              { get; set; }

        public virtual List<Cupon> Cupones                  { get; set; }
        public string RFC                                   { get; set; }  
        public string RazonSocial                           { get; set; }  
        public string NombreComercial                       { get; set; }  
        public string NumeroContrato                        { get; set; }  
        public string Telefono                              { get; set; }  
        public string Correo                                { get; set; }  
        public string Direccion                             { get; set; }  
        public string CodigoPostal                          { get; set; }  
        public string Code                                  { get; set; }  
        public bool IsActive                                { get; set; }

        public virtual AspAdmin AspAdmin             { get; set; }
    }
}
