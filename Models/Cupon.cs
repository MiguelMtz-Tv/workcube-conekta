using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Workcube.Generic;

namespace workcube_pagos.Models
{
    public class Cupon : UserCreated
    {
        [Key]
        public int IdCupon                      { get; set; }
        public string Codigo                    { get; set; }
        public string Descripcion               { get; set; }
        public int IdServicio                   { get; set; }
        public virtual Servicio Servicio        { get; set; }
        public int IdCliente                    { get; set; }
        public Cliente Cliente                  { get; set; }
        public long Monto                       { get; set; }
        [EnumDataType(typeof(CuponEstatus))]
        public CuponEstatus Status              { get; set; }
        public DateTime Vigencia                { get; set; }

        public virtual AspAdmin AspAdmin        { get; set; }
    }

    public enum CuponEstatus
    {
        [Display(Name = "Disponible")]
        Disponible,
        [Display(Name = "Usado")]
        Usado,
    }
}
