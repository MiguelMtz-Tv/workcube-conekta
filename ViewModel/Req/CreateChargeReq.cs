using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace workcube_pagos.ViewModel.Req
{
    public class CreateChargeReq
    {
        public int IdCliente    { get; set; }
        public int IdServicio   { get; set; }  
        public int IdCupon      { get; set; }
        public string IdCard    { get; set; } //token o id de la tarjeta
    }
}
