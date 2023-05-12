﻿using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class AspNetUser : IdentityUser
    {
        //[Key] public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPat { get; set;} = string.Empty;
        public string ApellidoMat { get; set;} = string.Empty;
        public string NombreCompleto => string.Format("{0} {1} {2}", Nombre, ApellidoPat, ApellidoMat);
        /*public string TokenRecovery { get; set; } = string.Empty;
        public new DateTime TokenRecoveryExpiration { get; set; } */ 
    }
}