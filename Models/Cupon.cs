using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workcube_pagos.Models
{
    public class Cupon
    {
        [Key]
        public int IdCupon { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdServicio { get; set; }
        public virtual Servicio Servicio { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Monto { get; set; }
        [EnumDataType(typeof(CuponEstatus))]
        public CuponEstatus Status { get; set; }
        public DateTime Vigencia { get; set; }
    }

    public enum CuponEstatus
    {
        [Display(Name = "Vigente")]
        Vigente,
        [Display(Name = "Vencido")]
        Vencido,
    }
}
