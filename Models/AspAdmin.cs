using System.ComponentModel.DataAnnotations;


namespace workcube_pagos.Models
{
    public class AspAdmin
    {
        [Key]
        public string IdAspAdmin                            { get; set; }
        public string Nombre                                { get; set; }
        public string ApellidoPat                           { get; set;}
        public string ApellidoMat                           { get; set;}
        public string NombreCompleto =>                     string.Format("{0} {1} {2}", Nombre, ApellidoPat, ApellidoMat);
        public string Email                                 { get; set; }
        public string HashedPassword                        { get; set; }
        public string RFC                                   { get; set; }
        public string Telfono                               { get; set; }
        public string Dirección                             { get; set; }

        public bool IsActive                                { get; set; }
        public virtual ICollection<Cliente> Clientes        { get; set; }
        public virtual ICollection<Cupon> Cupones           { get; set; }
        public virtual ICollection<Servicio> Servicios      { get; set; }
    }
}