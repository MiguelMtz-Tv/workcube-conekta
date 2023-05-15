﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workcube_pagos.Models
{
    public class Cupon
    {
        [Key]
        public int IdCupon { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public int IdServicio { get; set; }
        public Servicio Servicio { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Monto { get; set; }
        [EnumDataType(typeof(CuponStatus))]
        public DateTime vigencia { get; set; }
    }

    public enum CuponStatus
    {
        [Display(Name = "Vigente")]
        Vigente,
        [Display(Name = "Vencido")]
        Vencido,
        [Display(Name = "Cancelado")]
        Cancelado
    }
}
