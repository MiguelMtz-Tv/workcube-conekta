using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class ClienteModel
    {
        [Key] public int IdCliente { get; set; }
        public UsuarioModel Usuario { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set;}
        public string NombreComercial { get; set; }
        public string NumeroContrato { get; set;}
        public string Telefono { get; set;}
        public string Correo { get; set;}
        public string Direccion { get; set;}
        public string CodigoPostal { get; set;}
        public string Code { get; set;}
        public bool IsActive { get; set;}
    }
}
