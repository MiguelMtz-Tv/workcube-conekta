﻿using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class UsuarioModel : IdentityUser
    {
        //[Key] public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public virtual ClienteModel? Cliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPat { get; set;} = string.Empty;
        public string ApellidoMat { get; set;} = string.Empty;
        /*public string TokenRecovery { get; set; } = string.Empty;
        public new DateTime TokenRecoveryExpiration { get; set; } */ 
    }
}