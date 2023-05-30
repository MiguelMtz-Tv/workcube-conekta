namespace workcube_pagos.ViewModel.Res
{
    public class CreateClientRes
    {
        public string RFC               { get; set; }
        public string RazonSocial       { get; set; }
        public string NombreComercial   { get; set; }
        public string NumeroContrato    { get; set; }
        public string Telefono          { get; set; }
        public string Correo            { get; set; }
        public string Direccion         { get; set; }
        public string CodigoPostal      { get; set; }
        public string Code              { get; set; }
        public string StripeCustomerID  { get; set;}
    }
}
