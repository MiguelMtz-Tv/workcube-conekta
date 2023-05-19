using System.ComponentModel.DataAnnotations;
namespace workcube_pagos.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio                       { get; set; } 
        
        public virtual ServicioTipo ServicioTipo    { get; set; }
        public int IdServicioTipo                   { get; set; } 
        public string ServicioTipoName              { get; set; } 
        public string ServicioTipoDescripcion       { get; set; } 
        
        public virtual Cliente Cliente              { get; set; }
        public int IdCliente                        { get; set; }
        public string ClienteRazonSocial            { get; set; }
        
        public virtual Periodo Periodo              { get; set; }
        public int IdPeriodo                        { get; set; }
        public string PeriodoName                   { get; set; }
        public virtual List<Cupon> Cupones          { get; set; }
        public DateTime Vigencia                    { get; set; }
        
        
        public virtual ServicioEstatus ServicioEstatus { get; set; }
        public int IdServicioEstatus                   { get; set; }
        public string ServicioEstatusName              { get; set; }

        public string KeyServicio                      { get; set; }
    }
}
