using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace workcube_pagos.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio                           { get; set; } 
        
        public virtual ServicioTipo ServicioTipo        { get; set; }
        public int IdServicioTipo                       { get; set; } 
        public string ServicioTipoName                  { get; set; } 
        public string ServicioTipoDescripcion           { get; set; }
        public long ServicioTipoCosto                   { get; set; }
      
        public virtual Cliente Cliente                  { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Pago> Pagos          { get; set; }

        public int IdCliente                            { get; set; }
        public string ClienteRazonSocial                { get; set; }
        
        public virtual Periodo Periodo                  { get; set; }
        public int IdPeriodo                            { get; set; }
        public string PeriodoName                       { get; set; }
        public virtual List<Cupon> Cupones              { get; set; }
        public DateTime Vigencia                        { get; set; }
        public bool IsCanceled                          { get; set; }
        public string KeyServicio                       { get; set; }
    }
}