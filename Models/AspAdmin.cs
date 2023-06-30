using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workcube_pagos.Models
{
    public class AspAdmin : IdentityUser
    {
        public int IdCliente                                { get; set; }
        public string Nombre                                { get; set; }
        public string ApellidoPat                           { get; set;}
        public string ApellidoMat                           { get; set;}
        public string NombreCompleto =>                     string.Format("{0} {1} {2}", Nombre, ApellidoPat, ApellidoMat);
        public bool IsActive                                { get; set; }

        public virtual ICollection<Cliente> Clientes        { get; set; }
        public virtual ICollection<Cupon> Cupones           { get; set; }
        public virtual ICollection<Servicio> Servicios      { get; set; }
    }
}