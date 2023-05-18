﻿using System.ComponentModel.DataAnnotations;
namespace workcube_pagos.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; } 
        public int IdServicioTipo { get; set; } 
        public string ServicioTipoName { get; set; } 
        public string ServicioTipoDescription { get; set; }
        public virtual ServicioTipo ServicioTipo { get; set;}
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdPeriodo { get; set; }
        public virtual Periodo Periodo { get; set; }
        public List<Cupon> Cupones { get; set; }
        public DateTime Vigencia { get; set; }
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
        public string KeyServicio { get; set; }
    }

    public enum Status
    {
        [Display(Name = "Vigente")] //0
        Vigente,
        [Display(Name = "Vencido")] //1
        Vencido,
        [Display(Name = "Cancelado")] //2
        Cancelado
    }
}
