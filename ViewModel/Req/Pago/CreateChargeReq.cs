using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace workcube_pagos.ViewModel.Req.Pago
{
    public class CreateChargeReq
    {
        public int IdCliente        { get; set; }
        public string IdAspNetUser  { get; set; } //Campo utilizado para el envio de correos
        public int IdServicio       { get; set; }
        public int IdCupon          { get; set; }
        public string IdCard        { get; set; } //token o id de la tarjeta
        public bool AreCupon        { get; set; }

    }
}
