using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workcube_pagos.Models
{
    public class ServicioTipo
    {
        [Key]
        public int IdServicioTipo               { get; set; }
        [JsonIgnore]
        public List<Servicio> Servicio          { get; set; }
        public string Nombre                    { get; set; }
        public string Description               { get; set;}
        public long Costo                       { get; set; }

        
    }
}
