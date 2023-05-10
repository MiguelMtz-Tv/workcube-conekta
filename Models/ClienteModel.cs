using System.ComponentModel.DataAnnotations;

namespace workcube_pagos.Models
{
    public class ClienteModel
    {
        [Key] public int IdCliente { get; set; }
        public virtual UsuarioModel? Usuario { get; set; }
        public string RFC { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string NombreComercial { get; set; } = string.Empty;
        public string NumeroContrato { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set;}
    }
}
